using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;
    //[SerializeField]
    //private GameObject firstMask;
    [SerializeField]
    private GameObject firstMask;
    [SerializeField]
    private PlaneScript currentPlan;
    [SerializeField]
    private FadeScript fadeScript;

    [SerializeField]
    private GameObject dialogueGUI;
    [SerializeField]
    private GameObject dialogueButton;
    //[SerializeField]
    //private GameObject secondMask;
    //[SerializeField]
    //private GameObject maskCollection;

    private bool isMovingToDoor = false;
    private PlaneScript nextPlan;
    //private Vector3 currentMaskPosition;

    //Component[] components;
    //use to know the position to reach, to do action
    private Vector3 currentDestination;

    private ScalePlayer scalePlayer;
    
  

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = new Vector3(0f, 0f, 0f);
        scalePlayer = player.gameObject.GetComponent<ScalePlayer>();
        scalePlayer.sm = currentPlan.minscale;
        scalePlayer.sp = currentPlan.propscale;
        scalePlayer.sx = currentPlan.maxscale;
        //components = maskCollection.GetComponentsInChildren(typeof(Image), true);

        //for (int i = 0; i < components.Length; i++)
        //{
        //    GameObject newMask = Instantiate(components[i].gameObject, new Vector2(80f * i + 50f, 700f), Quaternion.identity);
        //    newMask.transform.localScale = 0.9f * newMask.transform.localScale;
        //    newMask.SetActive(false);
        //    Destroy(newMask.GetComponent<Button>()); // enlève le clic sur le masque
        //    foreach (Transform child in newMask.transform) // enlève le texte "Button"
        //    {
        //        Destroy(child.gameObject);
        //    }
        //newMask.transform.SetParent(maskCollection.transform.parent.transform); // met le masque dans le canvas
        //}
    }

    // Update is called once per frame
    void Update()
    {
        //if (player.transform.position == currentMaskPosition)
        //{
        //    //placer le mask dans l'inventaire
        //    firstMask.SetActive(false); //pour l'instant on le désactive juste
        //}
        //if (player.transform.position == currentMaskPosition)
        //{
        //    //placer le mask dans l'inventaire
        //    EventSystem.current.currentSelectedGameObject.GetComponent<Mask>().SetIsFound(true);
        //    EventSystem.current.currentSelectedGameObject.SetActive(false);
        //    foreach (Component comp in maskCollection.transform.parent.GetComponentsInChildren(typeof(Image), true))
        //    {
        //        if (comp.gameObject.name == EventSystem.current.currentSelectedGameObject.name + "(Clone)")
        //        {
        //            comp.gameObject.SetActive(true);
        //        }
        //    }
        //    currentMaskPosition = new Vector3();
        //}

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
        currentDestination = targetDoor.doorPosition;
        player.targetPosition = targetDoor.doorPosition;
        nextPlan = targetDoor.planeNextDoor;
        isMovingToDoor = true;
    }

    //a chaque changement de plan on placera le personnage à une position de base et on préparera un fade dans une coroutine
    public void ChangeScene()
    {
        Debug.Log("on change scene");
        StartCoroutine(ChangeSceneDelay());
        player.targetPosition = player.playerBasePose;

        //if (hallway.activeSelf)
        //{
        //    firstMask.SetActive(true);
        //    secondMask.SetActive(false);
        //}
        //else
        //{
        //    firstMask.SetActive(false);
        //    secondMask.SetActive(true);
        //}
    }

    //ce délai sert à limiter les interactions juste après un changement de plan et à faire un changement stylé aussi (je le fais en anglais la prochaine fois)
    public IEnumerator ChangeSceneDelay()
    {
        fadeScript.FadeIn();
        yield return new WaitForSeconds(2.0f);
        fadeScript.FadeOut();

        nextPlan.OnActive();//active new font
        currentPlan.OnDesactive();//desactive last font
        currentPlan = nextPlan;
        scalePlayer.sm=currentPlan.minscale;
        scalePlayer.sp = currentPlan.propscale;
        scalePlayer.sx = currentPlan.maxscale;
        player.targetPosition = currentPlan.GetInitPlayerPos();

        player.gameObject.transform.position = currentPlan.GetInitPlayerPos();//position the player to the position initial in the current plan
        nextPlan = null;
    }

    //quand on click sur un mask
    //public void GetMask()
    //{
    //    currentMaskPosition = EventSystem.current.currentSelectedGameObject.GetComponent<Mask>().maskPosition;
    //    player.targetPosition = currentMaskPosition;
    //}

    //lancer le dialogue
    public void DisplayDialogue()
    {
        dialogueGUI.SetActive(true);
        dialogueButton.SetActive(false);
        Dialogue.Instance.StartDialogue();
    }
}
