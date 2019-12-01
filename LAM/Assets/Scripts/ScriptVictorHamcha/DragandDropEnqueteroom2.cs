﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDropEnqueteroom2 : MonoBehaviour
{


    private bool selected;// es-ce que la pièce est séléctioné 
    [SerializeField]
    private float distance;//distance entre la pièce séléctioné et les slots 
    private SpriteRenderer _sprite;//spriterenderer de l'image 
    private Vector3 initialposition;//valeur de la position initial de la pièce 
    private bool droped;//vérifie si la pièce a été drop dans un des slots 
    
    private EnqueteManager enqueteManager;
    

    public static bool enigmeFinished;


    // Start is called before the first frame update 
    void Start()
    {
        enqueteManager = GetComponentInParent<EnqueteManager>();//enquetemanager parent de toutes les images a draganddrop
        _sprite = GetComponent<SpriteRenderer>();//récupère le sprite de la pièce 
        StartCoroutine(waitforinitialisation());//laisse le temps au pièce de se placer  
    }

    // Update is called once per frame 
    void Update()
    {
        if (selected)// si la pièce est séléctionée 
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);//prend la valeur de la positon de la souris par rapport a la caméra 
            transform.position = new Vector2(cursorPos.x, cursorPos.y);//la pèce prend la position de la souris 
            _sprite.sortingOrder = 2;//mets la pièce au premier pland  

        }

        for (int i = 0; i <= 3; i++)// vérifie pour toutes les pièce de la liste  
        {
            float distgoodnumber = Vector2.Distance(enqueteManager.goodpiece[i].transform.position, enqueteManager.slots[i].transform.position);//distance entre une bonne pièce et et le slots de la même index 

            if (distgoodnumber ==0)//si la pièce est dans le bon slot alors  
            {
                enqueteManager.piececorrectpos[i] = true;// le booléen le vérifiant devient true 
            }
            else
            {
                enqueteManager.piececorrectpos[i] = false;//le bouleen est faux  
            }

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))//si on lève le click de la souris 
        {
            for (int i = 0; i <= 3; i++)//vérifie pour chacun des slots 
            {
                if (selected)
                {
                    distance = Vector2.Distance(transform.position, enqueteManager.slots[i].position);// la distance entre la pièce que l'on a dans la main et les slots 

                    if (distance <= 1)//si la pièce est prets de l'un des slots alors 
                    {
                        transform.position = enqueteManager.slots[i].position;//la pièce prend la position du slot en question 
                        droped = true;//dropped devient true 

                    }
                }
            }

            if (!droped && selected)// si la pièce séléctionée n'a pas été drop dans un slot alors... 
            {
                transform.position = initialposition;//la pièce reprend sa position initiale 
            }

            _sprite.sortingOrder = 0;//la pièce revient au plan de base 
            selected = false;// pièce desélctioné  
            droped = false;//droped devient faux  
        }

       

    }

    IEnumerator waitforinitialisation()// coroutine servent a attendre que les pièce se soit placer a leur bonnes places  
    {
        yield return new WaitForSeconds(0.1f);//attend que les pièce soit a leur bonne place 
        initialposition = transform.position;//initial positon prend la valeur de la position initial des pièce  

    }

    private void OnMouseOver()//si on a la souris sur une pièce 
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))// si j'appuye sur click gauche alors... 
        {
            selected = true;//la pièce est séléctionné 
        }
    }
}
