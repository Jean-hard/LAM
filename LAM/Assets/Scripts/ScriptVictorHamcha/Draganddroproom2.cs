using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draganddroproom2 : MonoBehaviour
{
    [SerializeField]
    private float distance; // distance entre la pièce séléctionnée et les slots 

    private SpriteRenderer sprite; // spriterenderer de l'image 
    private Vector3 initialPosition;    // valeur de la position initial de la pièce 

    private bool imageSelected;  // est-ce que la pièce est séléctioné 
    private bool imageIsDropped;    // vérifie si la pièce a été drop dans un des slots 
    
    private EnqueteManager enqueteManager;
    public static bool enigmeFinished;

    // Start is called before the first frame update 
    void Start()
    {
        enqueteManager = GetComponentInParent<EnqueteManager>();    // enqueteManager parent de toutes les images a drag and drop
        sprite = GetComponent<SpriteRenderer>();   // récupère le sprite de la pièce 
        StartCoroutine(waitForInitialisation());    // laisse le temps au pièce de se placer  
    }

    // Update is called once per frame 
    void Update()
    {
        if (imageSelected)  // si la pièce est séléctionée 
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // prend la valeur de la position de la souris par rapport a la caméra 
            transform.position = new Vector2(cursorPos.x, cursorPos.y);     // la pièce prend la position de la souris 
            sprite.sortingOrder = 2;    // mets la pièce au premier plan 
        }

        for (int i = 0; i <= 3; i++)    // vérifie pour toutes les pièces de la liste  
        {
            float distgoodnumber = Vector2.Distance(enqueteManager.goodImagesList[i].transform.position, enqueteManager.slots[i].transform.position);   // distance entre une bonne pièce et le slot de la même index 

            if (distgoodnumber == 0) // si la pièce est dans le bon slot alors  
            {
                enqueteManager.pieceCorrectPos[i] = true;   // le booléen le vérifiant devient true 
            }
            else
            {
                enqueteManager.pieceCorrectPos[i] = false;    // le booleen est faux  
            }
        }

        if (Input.GetKeyUp(KeyCode.Mouse0)) // si on lève le click de la souris 
        {
            for (int i = 0; i <= 3; i++)    // vérifie pour chacun des slots 
            {
                if (imageSelected)
                {
                    distance = Vector2.Distance(transform.position, enqueteManager.slots[i].position);      // la distance entre la pièce que l'on a dans la main et les slots 

                    if (distance <= 1)  //si la pièce est près de l'un des slots alors 
                    {
                        transform.position = enqueteManager.slots[i].position;  //la pièce prend la position du slot en question 
                        imageIsDropped = true;  // dropped devient true 
                    }
                }
            }

            if (!imageIsDropped && imageSelected)   // si la pièce séléctionnée n'a pas été drop dans un slot alors... 
            {
                transform.position = initialPosition;   // la pièce reprend sa position initiale 
            }

            sprite.sortingOrder = 0;    // la pièce revient au plan de base 
            imageSelected = false;  // pièce desélectionnée  
            imageIsDropped = false; // dropped devient faux  
        }
    }

    IEnumerator waitForInitialisation() // coroutine servent a attendre que les pièces se soient placer a leur bonnes places  
    {
        yield return new WaitForSeconds(0.1f);  // attend que les pièce soit a leur bonne place 
        initialPosition = transform.position;   // initial position prend la valeur de la position initial des pièces
    }

    private void OnMouseOver()  // si on a la souris sur une pièce 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))   // si j'appuie sur click gauche alors... 
        {
            imageSelected = true;   // la pièce est sélectionnée 
        }
    }
}
