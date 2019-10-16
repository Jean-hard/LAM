using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horlogeaiguille : MonoBehaviour
{
    private bool selected;
    public float offset;
    public float distance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (selected == true)// si l'aiguille est séléctioné
        {
            
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
           

            float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);

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
