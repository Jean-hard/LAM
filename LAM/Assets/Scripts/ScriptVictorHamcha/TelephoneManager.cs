using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneManager : MonoBehaviour
{
    public List<int> numtel = new List<int>();//nuéro que le mec appel  
    public List<int> goodnumtel = new List<int>();//nuéro que le mec doit appelé 
    public bool end;// action du jeu fini + une nouvelle action faite par le joueur alors ...(demandé par les GD 
    public bool goodnumber; //quand tous les numéros mis par le joeur sont bon alors ... 
    public int numteltaille;
    public AudioSource telSound;// son apres le jeux du tel
    public GameObject falseBackButton;
    public GameObject trueBackButton;
    private float timerTime1=2;
   
    [SerializeField]
    private Dialogue telephoneDialogue;

    private bool firstTextDone;
    

    // Start is called before the first frame update 
    void Start()
    {
        numteltaille = goodnumtel.Count;
    }

    // Update is called once per frame 
    void Update()
    {
        if (numtel.Count == numteltaille) // si tout les numéro de l'appel son bon alors  
        {
            if (numtel[0] == goodnumtel[0] && numtel[1] == goodnumtel[1] && numtel[2] == goodnumtel[2] && numtel[3] == goodnumtel[3] && numtel[4] == goodnumtel[4] && numtel[5] == goodnumtel[5] && numtel[6] == goodnumtel[6] && numtel[7] == goodnumtel[7] && numtel[8] == goodnumtel[8] && numtel[9] == goodnumtel[9])
            {
                goodnumber = true;
                falseBackButton.SetActive(true);
                trueBackButton.SetActive(false);
            }
            else
            {
                numtel.Clear();
            }



        }
        if (goodnumber&& end)// si le jeu est finie et que le joueur a essayé une autre action alors  
        {

            trueBackButton.SetActive(true);
            falseBackButton.SetActive(false);
            if (timerTime1>0)
            {
                timerTime1 -= Time.deltaTime;
                Vibration.Vibrate(2000);//vibrations de 2 secondes codes trouvé sur internet nommé vibration reutilisable pour faire des vibrations sur android
                telSound.Play();//lance le son du tel
            }
            else 
            {
                timerTime1 = 0;
                GameManager.Instance.InitDialogue(telephoneDialogue);
                goodnumber = false;
                numtel = new List<int>();
                Room2Manager.telephoneDone = true;
                
                Debug.Log("youwin");

            }

           
            
        }
    }

    public void lanceappel()// si le joueur quitte le mini jeu apres avoir mis le bon num
    {
        if (goodnumber && !end)
            end = true;
        
    }
}
