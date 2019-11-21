using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoulletteTel : MonoBehaviour
{

    private bool selected; // boolen servant a savoir si l'aiguille est séléctioné 
    public float offset;//offset 
    public float rotZ;//angle de rotation de la toulette 
    public float roatmax;// float servant a limité l'angle de rotation de la roulette 
    public GameObject roulette;//roulette avec tous les numéros 
    public int numéro;//numéro associé a la touche du téléphone 
    private TelephoneManager telephonemanager;// code téléphone manager 
    public GameObject ancre;// position de l'enroit ou il faut aller pour valider un numéro 
    public float distance = 10;//distance entre le bouton du numéro et l'ancre 
    public float smooth;//vitesse de retour a point d'origine 
    public float rotationZ;// valeur rotation en Z 
    public bool returntopos;// est ce que la roulette retourn a sa position de départ 
    private void Start()
    {
        telephonemanager = roulette.GetComponent<TelephoneManager>();


    }

    void Update()
    {



        if (selected == true)// si le numéro  est séléctioné 
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - roulette.transform.position; // *position de la souris 

            rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;// trouve l'angle de rotation que doit prendre la roulette par rapport à la souris  
            if (rotZ <= 0)
                rotZ += 360;//pour avoir un cercle trigonométrique full positif 
            distance = Vector2.Distance(transform.position, ancre.transform.position);

            if (roatmax >= rotZ && !returntopos)// si l'angle de rotation est inferieur au max qu'a le droit de faire l'angle alors et que la roulette n'est pas entrain de revenir a sa posion d'origine alors 
            {
                rotationZ = rotZ + offset;// rotation en Z est égal a la position de la souris rotZ + un offset 

            }


            else if (returntopos)// quand on commence l'action de retourné a la postion d'origine 
            {
                if (rotationZ < 0)// si rotation est inferieur a zéro car on tourne la roullette dans le sans négatif de rotation 
                {

                    rotationZ += smooth;// l'élément retourn a sa position d'origine a la vitesse smooth 

                }
                else
                {
                    rotationZ = 0;// l'élément est retourné a sa posision d'origine 

                    selected = false;// l'elemnet n'est plus séléctioné 
                    returntopos = false;// si l'lement n'est pas séléctioné alors ne vas pas a position d'origine 
                }


            }

            roulette.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);// rotation de la roulette égal a la valeur de rotationZ 

        }







        if (Input.GetKeyUp(KeyCode.Mouse0))// si je lache le clic gauche de la souris la pièce est déléctioné 
        {


            if (selected)// si c'etait le numéro selectioné 
            {

                if (telephonemanager.numtel.Count < telephonemanager.numteltaille && distance <= 0.3)// si le nombre de numéro fait est inferieur au nombre de numéro demandé et que la distance entre l'ancre et le numéro séléctioné est inferieur à 0.2 alor 
                {
                    telephonemanager.numtel.Add(numéro);// le numéro est ajouté a la liste du numéro à appelé 

                }

                if (telephonemanager.end == false && telephonemanager.goodnumber == true && distance <= 0.3)// si le joueur a bien tous les bon numéro et essaye de refaire un num alors  
                    telephonemanager.end = true;// le jeu est fini  


                //.transform.rotation = Quaternion.Euler(0f, 0f, rotationZ);// rotation de la roulette égal a l'angle trouvé au dessus + offset 

                returntopos = true;//quand lève la souris commence l'acion de retour au point de départ 

            }

            Debug.Log(telephonemanager.numtel.Count);//debug pour vérifié le nombre de numéro rentrré par le joueur 




        }


    }





    private void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))//si ma souris est sur la pièce et que j'appui sur clic gauche alors la pièce est séléctioné 
        {
            selected = true;
        }
    }
}
