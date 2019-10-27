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
    public Transform minutePos; 
    public Transform posm;
    private GameObject pointsMinute;// les minutes sur l'horloge


    // position de l'aiguille heure sur l'horloge + des heures sur l'horloge
    public GameObject heure;// aiguille
    public Transform heurePos; 
    public Transform posh;
    private GameObject pointsHeure;// les heures sur l'orloge

    /**
     * distance pour les minute et distanceh pour les h ?!
     * c'est pas logique XD
     * faut que ce soit logique :p
     */
    private float distanceminute;// distance entre l'aiguille minute et les minutes sur l'horloge
    private float distanceheure;//distance entre l'aiguille heure et les heures sur l'horloge


    public List<GameObject> pointminute = new List<GameObject>();// list des points minutes sur l'horloge 
    public List<GameObject> pointheure = new List<GameObject>();// list des points heures sur l'horloge

    /**
     * Même si c'est commenté à la déclaration de variable, c'est trop dure de comprendre la suite juste avec un nom pareil
     * il faut mieux nommer les variable, quitte a avoir des nom plus long, tant pis
     * la priorité c'est la comprehension du code et pas l'aeration du code
     * mint, minh, M... c'est pas trop possible
     */
    private bool bonneMinute;// bool pour verifier si on est à la bonnes minutes 
    private float minuteReel;// valeur minutes temps réel 
    private float minuteHorloge;//  valeur minutes temps horloge

    private bool bonneHeure;// bool pour verifier si on est à la bonnes heures 
    private float heureReel;// valeur heure temps réel
    private float heureHorloge;// valeur heure temps horloge 

    public GameObject horlogePlan;
    public Sprite fontHorlogeSucceed;
    private SpriteRenderer fontHorloge;

    // Start is called before the first frame update
    void Start()
    {
        fontHorloge = horlogePlan.GetComponent<SpriteRenderer>();

        for (int i=0; i<60;i++)// place les 60 points minutes sur l'horloge
        {
            
            minutePos.rotation= Quaternion.Euler(0f, 0f, -i*6+180);// un point tout les 6° de l'angle
            /**
             * pipi.... PIPI ?! IMPOSSIBLE !!!
             * les noms doivent signifier la fonction de la variable, même si elle est temporaire comme ici
             */
            Vector2 positionpointminute = new Vector2(posm.position.x, posm.position.y);// position du point sur l'horloge 
            /**
             * pareil pour vv
             */
            pointsMinute = Instantiate(minute, positionpointminute, Quaternion.identity);// creer les petits points minutes 
            pointminute.Add(pointsMinute);//ajoutes les points a la liste pointsminute
        }


        for (int i = 0; i < 12; i++)// la même chose qu'au dessus mais pour les heures
        {

            heurePos.rotation = Quaternion.Euler(0f, 0f, -i * 30 + 180);
            /**
             * HIHI ?! Pareil faut le changer
             */
            Vector2 positionpointHeure = new Vector2(posh.position.x, posh.position.y);
            /**
             * pareil vv
             */
             pointsHeure = Instantiate(heure, positionpointHeure, Quaternion.identity);
            pointheure.Add(pointsHeure);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        minuteReel = System.DateTime.Now.Minute;// valeur minute réel
        heureReel = System.DateTime.Now.Hour;// valeur heure réel
       
        for (int i = 0; i < 60; i++)// vérifie sur tout les éléments de la liste points minutes si l'aiguille est sur une minute et si c'est sur la bonne minute
        {
            distanceminute = Vector2.Distance(pointminute[i].transform.position, minute.transform.position);// distance entre un points de l'horloge minute et l'aiguille des minutes 
            if (distanceminute <= 0.1)// si distance entre l'aiguille et un points minute de l'horloge est inferieur à 0.1
            {
                minuteHorloge = i;
                if (minuteHorloge==minuteReel)//si aiguille sur la bonne minute 
                {
                    bonneMinute = true;
                }
                else
                {
                    bonneMinute = false;
                }
                
            }
        }
        
        for (int i = 0; i < 12; i++)//même chose qu'au dessus mais pour les heures
        {
            distanceheure = Vector2.Distance(pointheure[i].transform.position, heure.transform.position);
            if (distanceheure <= 0.1)
            {
                heureHorloge = i;
                if (heureHorloge == heureReel || heureHorloge+12==heureReel)
                {
                    bonneHeure = true;
                }

                else
                {
                    bonneHeure = false;
                }

            }
           
        }

        if (bonneHeure&&bonneMinute)// si même heure et minute sur l'horloge et le temps réel :
        {
            Debug.Log("youwin");
            horlogeaiguille.jeufini = true;
            
        }
        
        if(horlogeaiguille.canChangeFont)
        {
            fontHorloge.sprite = fontHorlogeSucceed; //on applique le sprite avec le tiroir ouvert de l'horloge
            horlogeaiguille.canChangeFont = false;
        }

    }


}
