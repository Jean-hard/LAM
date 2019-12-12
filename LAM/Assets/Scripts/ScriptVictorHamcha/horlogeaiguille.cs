using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horlogeaiguille : MonoBehaviour
{
    public static bool canChangeFont;

    private bool selected; // boolen servant a savoir si l'aiguille est séléctioné
    public float offset;//offset OUAI BA MERCI GENIE !!!
    public float rotZ;
    private float rotationsave;
    private Collider2D collideraiguille;
    private float timerTime = 3;
    public bool turnLeft;
    private void Start()
    {
        collideraiguille = GetComponent<Collider2D>();
    }

    void Update()
    {
        
        if (selected == true)// si l'aiguille est séléctioné
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; // *position de la souris

            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;// trouve l'angle de rotation que doit prendre l'aiguille par rapport à la souris 
            transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))// si je lache le clic gauche de la souris la pièce est déléctioné
        {
            selected = false;
        }

        if (horlogemanager.horlogeGameFinished)
        {
            collideraiguille.enabled = false;
            selected = false;
            if (timerTime>0)
            {
                timerTime -= Time.deltaTime;
                if (turnLeft)
                    rotationsave += 3;
                else
                    rotationsave -= 5;
                transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset + rotationsave);// rotation commence a la dernière position de l'aiguille et augmente en continue

            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 180);// aiguille positioné sur miniuit 
                canChangeFont = true;
                horlogemanager.horlogeGameFinished = false;
                timerTime = 0;
            }

            
            //rotationsave+=15;
            //transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset+ rotationsave);// rotation de l'aiguille égal a l'angle trouvé au dessus + offset

           
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
