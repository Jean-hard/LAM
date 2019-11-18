using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulletteTel : MonoBehaviour
{
    // Start is called before the first frame update
    private bool selected; // boolen servant a savoir si l'aiguille est séléctioné
    public float offset;//offset
    public float rotZ;
    public float roatmax;
    public float roatmin;
    public GameObject roulette;
    public int numéro;
    private TelephoneManager telephonemanager;
    public GameObject ancre;
    public float distance=10;

    private void Start()
    {
        telephonemanager = roulette.GetComponent<TelephoneManager>();
    }

    void Update()
    {
        
        

        if (selected == true)// si l'aiguille est séléctioné
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - roulette.transform.position; // *position de la souris
            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg+360;// trouve l'angle de rotation que doit prendre l'aiguille par rapport à la souris 
            distance = Vector2.Distance(transform.position, ancre.transform.position);
           
        if(roatmin < rotZ && rotZ < roatmax)
             roulette.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset

            

        }
        
            
               
            
           
            

        

        if (Input.GetKeyUp(KeyCode.Mouse0))// si je lache le clic gauche de la souris la pièce est déléctioné
        {

            //if (rotZ<roatmax+0.3f)
            //{
            //    rotZ += 0.1f;
            //    roulette.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset
            //}
            //else if (rotZ > roatmax-0.3f)
            //{
            //    rotZ -= 0.1f;
            //    roulette.transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset
            //}
            //else
            //{
            //    roulette.transform.rotation = Quaternion.Euler(0f, 0f, roatmax + offset);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset
            //}
            if (selected)
            {
                roulette.transform.rotation = Quaternion.Euler(0f, 0f, roatmax + offset);
                if (telephonemanager.numtel.Count <= 5&& distance<=0.1)
                {
                    telephonemanager.numtel.Add(numéro);
                    
                }

                if (telephonemanager.end == false && telephonemanager.goodnumber == true&& distance <= 0.1)
                    telephonemanager.end = true;

            }

            Debug.Log(telephonemanager.numtel.Count);
           
           
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
