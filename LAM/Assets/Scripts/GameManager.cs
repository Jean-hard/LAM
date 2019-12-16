using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private PlaneScript startPlan;
    [SerializeField]
    private PlaneScript currentPlan;
    [SerializeField]
    private FadeScript fadeScript;
    [SerializeField]
    private FadeScript cinematiqueFade;

    //Dialogue
    [SerializeField]
    private GameObject dialogueGUI;
    private Dialogue currentDialogue;

    [SerializeField]
    private GameObject CinematiqueGUI;

    [SerializeField]
    private TwistManager twistManager;

    private bool isMovingToDoor = false;
    private PlaneScript nextPlan;

    private Vector3 currentDestination;

    private ScalePlayer scalePlayer;

    private bool isPlayerOnNextPlan = true;

    private bool isDoorSound = false;

    // SINGLETON ---------------------------------------------
    private static GameManager _instance;

    public static GameManager Instance { get { return _instance; } }

    // Animation --------------------
    private AnimScript animManager;

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

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = new Vector3(0f, 0f, 0f);
        scalePlayer = player.gameObject.GetComponent<ScalePlayer>();
        animManager = FindObjectOfType<AnimScript>();
        scalePlayer.sm = currentPlan.minscale;
        scalePlayer.sp = currentPlan.propscale;
        scalePlayer.sx = currentPlan.maxscale;

        StartCoroutine(StartSceneDelay());
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == currentDestination && isMovingToDoor)
        {
            ChangeScene();
            isMovingToDoor = false;
            currentDestination = new Vector3(0f, 0f, 0f);
        }
    }

    //function for Button, player is set for his move and his action destination
    public void MoveToDoor(DoorScript targetDoor)
    {
        nextPlan = targetDoor.planeNextDoor;

        //si le player est déjà présent sur la scène actuelle on le fait se déplacer
        if (isPlayerOnNextPlan == true)
        {
            currentDestination = targetDoor.doorPosition;
            player.targetPosition = targetDoor.doorPosition;
            isMovingToDoor = true;
            animManager.lancerAnim = true;//on lance l'anim
        }
        else
        {
            /**
            * ça va changer avec le remaniement de quand j'aurais le time
            */
            animManager.lancerAnim = false;//on ne lance pas d'anim
            isPlayerOnNextPlan = true;
            ChangeScene();
        }
    }

    /**
     * pour pouvoir lancer le son de porte
     */
    public void MoveToDoorSounded(DoorScript targetDoor)
    {
        isDoorSound = true;
        MoveToDoor(targetDoor);
    }

    public void SetPlayerVisible(bool isVisible)
    {
        isPlayerOnNextPlan = isVisible;
    }

    public void ChangePlan(PlaneScript theNextPlan)
    {
        nextPlan = theNextPlan;
        //isPlayerOnNextPlan = true;
        ChangeScene();
    }

    public void ChangePlanSounded(PlaneScript theNextPlan)
    {
        isDoorSound = true;
        ChangePlan(theNextPlan);
    }

    /**
     * On change de plan et seiji ne sera pas visible dans celui ci
     */
    public void ChangePlanWhithoutPlayer(PlaneScript theNextPlan)
    {
        nextPlan = theNextPlan;
        //isPlayerOnNextPlan = false;
        ChangeScene();
    }

    /**
     * a chaque changement de plan on placera le personnage à une position de base et on préparera un fade dans une coroutine
     * TODO : et on stop le dialogue
     */
    public void ChangeScene()
    {
        Debug.Log("on change scene");
        StartCoroutine(ChangeSceneDelay());

        if (currentDialogue)
        {
            currentDialogue.StopDialogue();
            dialogueGUI.SetActive(false);
        }
        player.targetPosition = player.playerBasePose;
    }

    //le lancement du jeu se fera prendant un fade qui enchainera sur le lancement du dialogue du plan de départ
    public IEnumerator StartSceneDelay()
    {
        //fadeScript.FadeIn();
        yield return new WaitForSeconds(1.5f);
        fadeScript.FadeOut();

        ////on récupère le dialogue initiale de la scène si il y en a un ET si il n'a jamais été lancé
        //currentDialogue = currentPlan.GetInitialDialogue();
        //if (currentDialogue)
        //    DisplayDialogue();
    }

    //ce délai sert à limiter les interactions juste après un changement de plan et à faire un changement stylé aussi (je le fais en anglais la prochaine fois)
    public IEnumerator ChangeSceneDelay()
    {
        fadeScript.FadeIn();
        yield return new WaitForSeconds(2.0f);
        InitNewScene();
        fadeScript.FadeOut();
        if (isDoorSound)
        {
            SoundManager.Instance.PlayOpeningDoor();
            isDoorSound = false;
        }
    }

    public void InitNewScene()
    {
        //change en fonction du type de bouton(de fonction*) utilisé pour le changement de plan
        player.gameObject.SetActive(isPlayerOnNextPlan);

        nextPlan.OnActive();//active new font
        currentPlan.OnDesactive();//desactive last font
        currentPlan = nextPlan;

        //Set player in the new scene
        scalePlayer.sm=currentPlan.minscale;
        scalePlayer.sp = currentPlan.propscale;
        scalePlayer.sx = currentPlan.maxscale;
        player.targetPosition = currentPlan.GetInitPlayerPos();

        player.gameObject.transform.position = currentPlan.GetInitPlayerPos();//position the player to the position initial in the current plan
        nextPlan = null;
    }

    /**
     * On stock le dialogue visé et on le lance
     */
    public void InitDialogue(Dialogue targetDialogue)
    {
        currentDialogue = targetDialogue;
        DisplayDialogue();
    }
    /**
     * On active le canvas de dialogue.
     * On pourra de ce fait accéder à la phrase suivante en passant par le gameManager.
     */
    public void DisplayDialogue()
    {
        if (currentDialogue == null)
            Debug.Log("il n'y a pas de dialogue stocké");
        else
        {
            Debug.Log("display le dialogue");
            dialogueGUI.SetActive(true);

            currentDialogue.StartDialogue();
        }
    }

    //bouton "skip dialogue"
    public void StopDialogue()
    {
        currentDialogue.StopDialogue();
    }

    //correspond au bouton "continuer" dans la scène
    public void DisplayDialogueNextSentence()
    {
        currentDialogue.NextSentence();
    }

    //retour à l'accueil après la cinématique
    public void BackToAccueil()
    {
        scalePlayer.sm = startPlan.minscale;
        scalePlayer.sp = startPlan.propscale;
        scalePlayer.sx = startPlan.maxscale;

        startPlan.OnActive();//active new font
        currentPlan.OnDesactive();//desactive last font
        currentPlan = startPlan;

        player.targetPosition = currentPlan.GetInitPlayerPos();

        player.gameObject.transform.position = currentPlan.GetInitPlayerPos();//position the player to the position initial in the current plan
        nextPlan = null;
    }


    public void LaunchCinematique()
    {
        cinematiqueFade.FadeIn();
        SoundManager.Instance.PlayFlashBackOpening();
        CinematiqueGUI.SetActive(true);
        CinematiqueGUI.GetComponent<CinematiqueScript>().LaunchCinematique();
    }

    public void EndCinematique(int idCinematique)
    {
        if (idCinematique == 1)
        {
            Debug.Log("Twist 1 launch");
            twistManager.TwistEnvironnement1();
        }
        if (idCinematique == 2)
        {
            Debug.Log("Twist 2 launch");
            twistManager.TwistEnvironnement2();
        }
        else
            Debug.Log("erreur en fin de cinématique");
        cinematiqueFade.FadeOut();
    }
}

