using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;

    [SerializeField]
    private PlaneScript currentPlan;
    [SerializeField]
    private FadeScript fadeScript;

    //Dialogue
    [SerializeField]
    private GameObject dialogueGUI;
    [SerializeField]
    private GameObject dialogueButton;
    private Dialogue currentDialogue;

    private bool isMovingToDoor = false;
    private PlaneScript nextPlan;

    private Vector3 currentDestination;

    private ScalePlayer scalePlayer;

    private bool isPlayerOnNextPlan = true;
    
  

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = new Vector3(0f, 0f, 0f);
        scalePlayer = player.gameObject.GetComponent<ScalePlayer>();
        scalePlayer.sm = currentPlan.minscale;
        scalePlayer.sp = currentPlan.propscale;
        scalePlayer.sx = currentPlan.maxscale;
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
        }
        else
        {
            /**
            * ça va changer avec le remaniement de quand j'aurais le time
            */
            isPlayerOnNextPlan = true;
            ChangeScene();
        }
    }

    /**
     * CETTE FONCTION EST TEMPORAIRE !!
     * Il faudra changer le système de changement de scène pour garder de la logique dans nos action.
     * Quand on change de plan on passe pas une porte,
     * donc il ne devra plus y avoir de doorScript en paramètre.
     */
    public void ChangePlan(DoorScript targetDoor)
    {
        nextPlan = targetDoor.planeNextDoor;
        /**
         * ça va changer avec le remaniement de quand j'aurais le time
         */
        isPlayerOnNextPlan = false;

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
        if(currentDialogue)
            currentDialogue.StopDialogue();
        player.targetPosition = player.playerBasePose;
    }

    //ce délai sert à limiter les interactions juste après un changement de plan et à faire un changement stylé aussi (je le fais en anglais la prochaine fois)
    public IEnumerator ChangeSceneDelay()
    {
        fadeScript.FadeIn();
        yield return new WaitForSeconds(2.0f);
        fadeScript.FadeOut();

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

        //on récupère le dialogue initiale de la scène si il y en a un ET si il n'a jamais été lancé
        currentDialogue = currentPlan.GetInitialDialogue();
        if (currentDialogue)
            DisplayDialogue();            

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
     * On active le canvas de dialogue et on stock le dialogue actuel.
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

    //correspond au bouton "continuer" dans la scène
    public void DisplayDialogueNextSentence()
    {
        currentDialogue.NextSentence();
    }
}

/**
 * TODO : corriger bug de dialogueGUI qui se set active quand elle veut c'est relou
 *  - faire en sorte que le lancement du jeu sur la scène principale se fasse bien par un changeScene
 *  - préparer un système pour alterner entre seiji et interlocuteur.
 */
