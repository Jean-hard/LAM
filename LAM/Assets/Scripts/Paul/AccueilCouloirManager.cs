using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilCouloirManager : MonoBehaviour
{
    public static bool aubergisteTalkDone;

    //dans le couloirs
    public static bool soloTalk01Ready = true; //pour SOLO DIA 01
    public static bool soloTalk02Ready = false; //pour SOLO DIA 02

    //dans l'accueil
    public static bool introTalkReady = true; //SOLO_SEIJI_HALL_01
    public static bool accueilTalk1Ready = false; //SOLO_SEIJI_HALL_02
    public static bool accueilTalk2Ready = false; //DIA_AFTER_CINE_01

    //pour les cinématique
    public static bool cinematique1Done = false;


    [SerializeField]
    private Dialogue aubergisteTalkDialogue;

    [SerializeField]
    private Dialogue[] aubergisteDiaTab;
    private int indexAubergisteDia = 0;

    [SerializeField]
    private Dialogue[] accueilDiaTab;
    private int indexAccueilDia = 0;

    [SerializeField]
    private Dialogue[] couloirDiaTab;
    private int indexCouloirDia = 0;

    [SerializeField]
    private Dialogue afterCinematiqueDia01;

    private Dialogue currentAubergisteDia;
    private Dialogue currentCouloirDia;
    private Dialogue currentAccueilDia;


    // SINGLETON ---------------------------------------------
    private static AccueilCouloirManager _instance;

    public static AccueilCouloirManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }//--------------------------------------------------------------------

    public void Start()
    {
        currentAubergisteDia = aubergisteDiaTab[0];
        currentCouloirDia = couloirDiaTab[0];
        currentAccueilDia = accueilDiaTab[0];

        ////////////si on peut faire un systeme plus propre ce serait mieux
        Debug.Log("lancement du dialogue d'intro de seiji");
        ShowAccueilDia();
    }

    //button aubergiste
    public void LaunchAubergisteDia()
    {
        GameManager.Instance.InitDialogue(currentAubergisteDia);
        aubergisteTalkDone = true;

        //apres DIA_AUBER_01 on passera dans l'accueil SOLO_SEIJI_HALL_02
        if (indexAubergisteDia == 0)
            accueilTalk1Ready = true;


    }

    //button escalier dans l'accueil
    public void CheckAubergisteTalkDone(DoorScript stairDoor)
    {
        Debug.Log("aubergiste talk : " + aubergisteTalkDone);
        if (aubergisteTalkDone)
        {
            GameManager.Instance.MoveToDoor(stairDoor);
        }
        else
        {
            GameManager.Instance.InitDialogue(aubergisteTalkDialogue);
        }
    }

    public void UpdateAubergisteDia()
    {
        indexAubergisteDia++;
        currentAubergisteDia = aubergisteDiaTab[indexAubergisteDia];
        //une fois passer le dialogue DIA_AUBER_03
        if (indexAubergisteDia == 2)
            soloTalk02Ready = true;
    }


    public void ShowAccueilDia()
    {
        if (introTalkReady == true)
        {
            GameManager.Instance.InitDialogue(currentAccueilDia);
            introTalkReady = false;
        }

        if (accueilTalk1Ready == true)
        {
            indexAccueilDia++;
            currentAccueilDia = accueilDiaTab[indexAccueilDia];
            GameManager.Instance.InitDialogue(currentAccueilDia);
            accueilTalk1Ready = false;
        }
    }

    public void ShowCouloirDia()
    {
        //si le premier dialogue solo n'a pas été fait lorsque qu'on arrive à l'étage - DIA_SOLO_01
        if (soloTalk01Ready == true)
        {
            GameManager.Instance.InitDialogue(currentCouloirDia);
            soloTalk01Ready = false;
        }
        //si éléments nécessaire sont prêt pour le second dialogue à l'étage - DIA_SOLO_02
        if (soloTalk02Ready == true)
        {
            indexCouloirDia++;
            currentCouloirDia = couloirDiaTab[indexCouloirDia];
            GameManager.Instance.InitDialogue(currentCouloirDia);
            soloTalk02Ready = false;
        }
    }

    //appelé a la fin de la cinématique normalement
    public void ShowAfterCinematiqueDia()
    {
        GameManager.Instance.InitDialogue(afterCinematiqueDia01);
        //pour passer en AUBER_DIA_03 et plus.
        UpdateAubergisteDia();
    }
}