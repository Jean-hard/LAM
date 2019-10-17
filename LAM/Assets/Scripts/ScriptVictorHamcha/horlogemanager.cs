using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horlogemanager : MonoBehaviour
{

    //position de l'aiguille minute + position des minutes sur l'horloge
    public GameObject minute;//aiguille 
    public Transform minpos;
    public Transform posm;
    private GameObject vv;// les minutes sur l'horloge


    // possition de l'aiguille heure sur l'horloge + des heures sur l'horloge
    public GameObject heure;// aiguille
    public Transform heurepos; 
    public Transform posh;
    private GameObject hh;// les heures sur l'orloge


    private float distance;// distance entre aiguille minute et les minutes sur l'horloge
    private float distanceh;//distance entre l'aiguille heure et les heures sur l'horloge


    public List<GameObject> pointminute = new List<GameObject>();// list des poins minutes sur l'horloge 
    public List<GameObject> pointheure = new List<GameObject>();// list des points heures sur l'horloge


    private bool m;// bool pour verifier si on est à la bonnes minutes 
    private float mint;// valeur minutes temps réel 
    private float minh;//  valeur minutes temps horloge

    private bool h;// bool pour verifier si on est à la bonnes heures 
    private float heuret;// valeur heure temps réel
    private float heureh;// valeur heure temps horloge 

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<60;i++)// place les 60 poins minutes sur l'horloge
        {
            
            minpos.rotation= Quaternion.Euler(0f, 0f, -i*6+180);// un point tout les 6° de l'angle
            Vector2 pipi = new Vector2(posm.position.x, posm.position.y);// position du point sur l'horloge 
            vv = Instantiate(minute, pipi, Quaternion.identity);// creer les petits points minutes 
            pointminute.Add(vv);//ajoutes les points a la liste pointsminute
        }


        for (int i = 0; i < 12; i++)// la même chose qu'au dessus mais pour les heures
        {

            heurepos.rotation = Quaternion.Euler(0f, 0f, -i * 30 + 180);
            Vector2 hihi = new Vector2(posh.position.x, posh.position.y);
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
            distance = Vector2.Distance(pointminute[i].transform.position, minute.transform.position);// distanceentre un points de l'horloge minute et l'aiguille des minutes 
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
