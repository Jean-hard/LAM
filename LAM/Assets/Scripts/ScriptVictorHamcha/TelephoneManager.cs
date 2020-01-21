using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TelephoneManager : MonoBehaviour
{
    public List<int> numtel = new List<int>();  //numéro que le mec appel  
    public List<int> goodnumtel = new List<int>();  //numéro que le mec doit appelé 
    public bool end;    // action du jeu fini + une nouvelle action faite par le joueur alors ...(demandé par les GD)
    public bool goodnumber;     //quand tous les numéros mis par le joueur sont bon alors ... 
    public int numteltaille;
    public AudioSource telSound;    // son apres le jeux du tel
    public GameObject telephoneButton;
    public GameObject originalBackButton;
    private float timerTime1=2;
   
    [SerializeField]
    private Dialogue telephoneDialogue;

    private bool firstTextDone;
    private bool hasRinged;

    public Text papierText;
    public int numberToWriteIndex = 0;

    // Start is called before the first frame update 
    void Start()
    {
        numteltaille = goodnumtel.Count;
    }

    // Update is called once per frame 
    void Update()
    {
        if (numtel.Count == numteltaille) // si tous les numéros de l'appel sont bons alors  
        {
            if (numtel[0] == goodnumtel[0] && numtel[1] == goodnumtel[1] && numtel[2] == goodnumtel[2] && numtel[3] == goodnumtel[3] && numtel[4] == goodnumtel[4] && numtel[5] == goodnumtel[5] && numtel[6] == goodnumtel[6] && numtel[7] == goodnumtel[7] && numtel[8] == goodnumtel[8] && numtel[9] == goodnumtel[9])
            {
                goodnumber = true;
                
                if (!hasRinged)
                {
                    SoundManager.Instance.PlayRingRing();
                    //Vibration.Vibrate(2000);    //vibrations de 2 secondes codes trouvé sur internet nommé vibration reutilisable pour faire des vibrations sur android
                    hasRinged = true;
                    originalBackButton.SetActive(false);
                    telephoneButton.SetActive(true);
                }
            }
            else
            {
                numtel.Clear();
            }
        }
    }

    // appelée quand le joueur clicke sur le téléphone quand il sonne
    public void DecrocheTel()    
    {
        SoundManager.Instance.StopRingRing();
        SoundManager.Instance.PlayGruntingCall();
        telephoneButton.SetActive(false);
        StartCoroutine(WaitForSpeaking());
        goodnumber = false;
        numtel = new List<int>();
        Room2Manager.telephoneDone = true;
        StartCoroutine(WaitForQuit());
    }

    public IEnumerator WaitForSpeaking()
    {
        yield return new WaitForSeconds(4f);
        GameManager.Instance.InitDialogue(telephoneDialogue);
    }

    public IEnumerator WaitForQuit()
    {
        yield return new WaitForSeconds(15f);
        originalBackButton.SetActive(true);
    }

    public void ClearNumTel()
    {
        numtel.Clear();
        papierText.text = "";
        numberToWriteIndex = 0;
        Debug.Log("Clear Numtel");
    }
}
