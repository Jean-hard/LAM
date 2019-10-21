using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private SpriteRenderer _sprite;
    private bool selected;
    public List<GameObject> pos = new List<GameObject>();//tous les emplacements de pièce
    public List<GameObject> piece = new List<GameObject>(); //toute les pièces du puzzle
    private bool[] bon;//bool pour savoir si la pièce est oau bonne emplacement 
    public bool gameWin = false;
    public GameObject puzzlegame; // jeux de puzzle entier

    private void Start()
    {
        bon = new bool[16];

        _sprite = gameObject.GetComponent<SpriteRenderer>();
    }
    void Update()
    {
        if (selected==true)// si la pièce est séléctioné
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);//la pèce prend la position de la souris
            //_sprite.sortingOrder = 5;
            
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))// si je lache le clic gauche de la souris la pièce est déléctioné
        {
            selected = false;
           
        }
        
        for (int i =0; i<17; i++)
        {
            float distance = Vector2.Distance(pos[i].transform.position, gameObject.transform.position); // distance entre un amplacement et la pièce séléctioné 
            if (distance<1)// si la distance entre la pièce et et l'amplacement est inferieur à 1 alors la pièce prend la position de l'amplacement comme si magnétisée et on vérifie si c'est la bonne pièce avec fonction win
            {
                transform.position = pos[i].transform.position;
                //_sprite.sortingOrder = 1;
                Win();
                    
            }
            
            
            
        }
    }

    

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))//si ma souris est sur la pièce et que j'appui sur clic droit alors la pièce est séléctioné
        {
            selected = true;

        }
    }

    private IEnumerator SkipGameDelay()
    {
        yield return new WaitForSeconds(3.0f);
        puzzlegame.SetActive(false);
    }

    public void Win ()
    {
        for (int i = 0; i < 16; i++)// vérifie si la pièce est au bonne endoirt 
        {
            

            float dis = Vector2.Distance(pos[i].transform.position, piece[i].transform.position);

            if (dis == 0)//si elle est au bon endroit l'emplacement est validé 
            {

                bon[i] = true;
            }
            else//si non elle est invalidé
            {
                bon[i] = false;
            }
               

        }
        if (bon[0]&&bon[1]&&bon[2]&& bon[3] && bon[4] && bon[5]&& bon[6] && bon[7] && bon[8]&&bon[9] && bon[10] && bon[11]&& bon[12] && bon[13] && bon[14]&& bon[15])//si toutes les pièces sont au bons endroit
        {
            Debug.Log("we win");
            StartCoroutine(SkipGameDelay());
        }
    }
    private void OnTriggerStay(Collider other)
    {
        


        

    }
    
}
