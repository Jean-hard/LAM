using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class GameManager : MonoBehaviour
{
    //player in the scene
    [SerializeField]
    private PlayerManager player;

    //the first plan showed at the beginning
    [SerializeField]
    private PlaneScript startPlan;

    //the actual active plan in game
    [SerializeField]
    private PlaneScript currentPlan;

    //Fade screen use between each change of plan
    [SerializeField]
    private FadeScript fadeScript;

    //Fade screen use for each cinematique
    [SerializeField]
    private FadeScript cinematiqueFade;

    //Dialogue
    [SerializeField]
    private GameObject dialogueGUI;

    //actual dialogue active in game
    private Dialogue currentDialogue;

    //gameobject Canvas for dialogue
    [SerializeField]
    private GameObject CinematiqueGUI;

    //to manage the change of sprite after the cinematique.
    [SerializeField]
    private TwistManager twistManager;

    //to know if the player is still moving
    private bool isMovingToDoor = false;

    //the next plan to activate when the player reach his destination
    private PlaneScript nextPlan;

    //current destination for the player in the scene
    private Vector3 currentDestination;

    //actual scale for the Player, different in each room 
    private ScalePlayer scalePlayer;

    //check if the player is visible on the next plan
    private bool isPlayerOnNextPlan = true;

    //check if the door will make a sound when the player reach it
    private bool isDoorSound = false;

    /**
     * for final Scene
     */
    private bool isMovingToFinalScene = false;
    [SerializeField]
    private Vector3 finalDoorPosition;
    [SerializeField]
    private VideoPlayer videoLauncher;

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
    }

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = new Vector3(0f, 0f, 0f);
        scalePlayer = player.gameObject.GetComponent<ScalePlayer>();
        animManager = FindObjectOfType<AnimScript>();
        scalePlayer.speedScale = currentPlan.speedScale;
        scalePlayer.needScale = currentPlan.needScale;
        

        StartCoroutine(StartSceneDelay());

        currentPlan.OnActive();
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

        //final scene
        if (player.transform.position == currentDestination && isMovingToFinalScene)
        {
            ShowVideo();
            isMovingToFinalScene = false;
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
            scalePlayer.canScale = true;
            animManager.AnimationSeiji("animSeijiDeDos");
        }
        else
        {
            animManager.lancerAnim = false;//on ne lance pas d'anim
            scalePlayer.canScale = false;
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

    //set player visible for the next plan
    public void SetPlayerVisible(bool isVisible)
    {
        isPlayerOnNextPlan = isVisible;
    }

    //to change plan without the player going at a location
    public void ChangePlan(PlaneScript theNextPlan)
    {
        nextPlan = theNextPlan;
        ChangeScene();
    }

    //like "ChangePlan" with sound
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
        scalePlayer.speedScale = currentPlan.speedScale;
        scalePlayer.needScale = currentPlan.needScale;
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
        scalePlayer.speedScale = currentPlan.speedScale;
        scalePlayer.needScale = currentPlan.needScale;

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
        CinematiqueGUI.GetComponent<DialogueCinematique>().LaunchCinematique();
    }

    public void EndCinematique(int idCinematique)
    {
        if (idCinematique == 1)
        {
            Debug.Log("Twist 1 launch");
            twistManager.TwistEnvironnement1();
        }
        else if (idCinematique == 2)
        {
            Debug.Log("Twist 2 launch");
            twistManager.TwistEnvironnement2();
        }
        else
            Debug.Log("erreur en fin de cinématique");
        cinematiqueFade.FadeOut();
        CinematiqueGUI.SetActive(false);
    }

    //move the player to a special location and indiquate that the next plan is the final one
    public void MoveToFinalScene()
    {
        currentDestination = finalDoorPosition;
        player.targetPosition = finalDoorPosition;
        isMovingToFinalScene = true;
        animManager.lancerAnim = true;//on lance l'anim
        animManager.AnimationSeiji("animSeijiDeDos");
    }

    public void ShowVideo()
    {
        videoLauncher.Play();
    }

    public IEnumerator DesactiveCineGUI()
    {
        yield return new WaitForSeconds(4f);
        CinematiqueGUI.SetActive(false);
    }
}

