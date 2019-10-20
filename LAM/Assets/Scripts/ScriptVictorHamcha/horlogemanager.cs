using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horlogemanager : MonoBehaviour
{

    //position de l'aiguille minute + position des minutes sur l'horloge
    public GameObject minute;//aiguille 
    /**
     * attention min signifie minimum la plupart du temps
     * pour minute mieux vaux écrire minutePos
     */
    public Transform minpos; 
    public Transform posm;
    private GameObject vv;// les minutes sur l'horloge


    // position de l'aiguille heure sur l'horloge + des heures sur l'horloge
    public GameObject heure;// aiguille
    public Transform heurepos; 
    public Transform posh;
    private GameObject hh;// les heures sur l'orloge

    /**
     * distance pour les minute et distanceh pour les h ?!
     * c'est pas logique XD
     * faut que ce soit logique :p
     */
    private float distance;// distance entre l'aiguille minute et les minutes sur l'horloge
    private float distanceh;//distance entre l'aiguille heure et les heures sur l'horloge


    public List<GameObject> pointminute = new List<GameObject>();// list des points minutes sur l'horloge 
    public List<GameObject> pointheure = new List<GameObject>();// list des points heures sur l'horloge

    /**
     * Même si c'est commenté à la déclaration de variable, c'est trop dure de comprendre la suite juste avec un nom pareil
     * il faut mieux nommer les variable, quitte a avoir des nom plus long, tant pis
     * la priorité c'est la comprehension du code et pas l'aeration du code
     * mint, minh, M... c'est pas trop possible
     */
    private bool m;// bool pour verifier si on est à la bonnes minutes 
    private float mint;// valeur minutes temps réel 
    private float minh;//  valeur minutes temps horloge

    private bool h;// bool pour verifier si on est à la bonnes heures 
    private float heuret;// valeur heure temps réel
    private float heureh;// valeur heure temps horloge 

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<60;i++)// place les 60 points minutes sur l'horloge
        {
            
            minpos.rotation= Quaternion.Euler(0f, 0f, -i*6+180);// un point tout les 6° de l'angle
            /**
             * pipi.... PIPI ?! IMPOSSIBLE !!!
             * les noms doivent signifier la fonction de la variable, même si elle est temporaire comme ici
             */
            Vector2 pipi = new Vector2(posm.position.x, posm.position.y);// position du point sur l'horloge 
            /**
             * pareil pour vv
             */
            vv = Instantiate(minute, pipi, Quaternion.identity);// creer les petits points minutes 
            pointminute.Add(vv);//ajoutes les points a la liste pointsminute
        }


        for (int i = 0; i < 12; i++)// la même chose qu'au dessus mais pour les heures
        {

            heurepos.rotation = Quaternion.Euler(0f, 0f, -i * 30 + 180);
            /**
             * HIHI ?! Pareil faut le changer
             */
            Vector2 hihi = new Vector2(posh.position.x, posh.position.y);
            /**
             * pareil vv
             */
             hh = Instantiate(heure, hihi, Quaternion.identity);
            pointheure.Add(hh);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        mint = System.DateTime.Now.Minute;// valeur minute réel
        heuret = System.DateTime.Now.Hour;// valeur heure réel
       
        for (int i = 0; i < 60; i++)// vérifie sur tout les éléments de la liste points minutes si l'aiguille est sur une minute et si c'est sur la bonne minute
        {
            distance = Vector2.Distance(pointminute[i].transform.position, minute.transform.position);// distance entre un points de l'horloge minute et l'aiguille des minutes 
            if (distance <= 0.1)// si distance entre l'aiguille et un points minute de l'horloge est inferieur à 0.1
            {
                minh = i;
                if (minh==mint)//si aiguille sur la bonne minute 
                {
                    m = true;
                }
                else
                {
                    m = false;
                }
                
            }
        }
        
        for (int i = 0; i < 12; i++)//même chose qu'au dessus mais pour les heures
        {
            distanceh = Vector2.Distance(pointheure[i].transform.position, heure.transform.position);
            if (distanceh <= 0.1)
            {
                heureh = i;
                if (heureh == heuret|| heureh+12==heuret)
                {
                    h = true;
                }

                else
                {
                    h = false;
                }

            }
        }

        if (h&&m)// si même heure et minute sur l'horloge et le temps réel :
        {
            Debug.Log("youwin");
        }


    }
}
