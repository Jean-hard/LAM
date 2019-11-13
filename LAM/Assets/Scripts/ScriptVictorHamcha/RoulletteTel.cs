using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulletteTel : MonoBehaviour
{
    // Start is called before the first frame update
    private bool selected; // boolen servant a savoir si l'aiguille est séléctioné
    public float offset;//offset
    public float rotZ;
    public GameObject roulette;
  
    

    private void Start()
    {
       
    }

    void Update()
    {
        

        if (selected == true)// si l'aiguille est séléctioné
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - roulette.transform.position; // *position de la souris
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;// trouve l'angle de rotation que doit prendre l'aiguille par rapport à la souris 
            roulette.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))// si je lache le clic gauche de la souris la pièce est déléctioné
        {
            selected = false;
        }

      
    }


   



    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//si ma souris est sur la pièce et que j'appui sur clic droit alors la pièce est séléctioné
        {
            selected = true;
        }
    }
}
