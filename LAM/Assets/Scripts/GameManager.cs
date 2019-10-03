using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private GameObject firstMask;
    [SerializeField]
    private PlaneScript currentPlan;
    [SerializeField]
    private FadeScript fadeScript;

    private bool isMovingToDoor = false;
    private PlaneScript nextPlan;
    private Vector3 currentMaskPosition;

    //use to know the position to reach, to do action
    private Vector3 currentDestination;

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (player.transform.position == currentMaskPosition)
        {
            //placer le mask dans l'inventaire
            firstMask.SetActive(false); //pour l'instant on le désactive juste
        }

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
    }

    //ce délai sert à limiter les interactions juste après un changement de plan et à faire un changement stylé aussi (je le fais en anglais la prochaine fois)
    public IEnumerator ChangeSceneDelay()
    {
        fadeScript.FadeIn();
        yield return new WaitForSeconds(2.0f);
        fadeScript.FadeOut();

        ////petit problème, on passe plusieur fois ici et no lo se WHY 
        nextPlan.OnActive();//active new font
        currentPlan.OnDesactive();//desactive last font
        currentPlan = nextPlan;
        player.targetPosition = currentPlan.GetInitPlayerPos();
        player.gameObject.transform.position = currentPlan.GetInitPlayerPos();//position the player to the position initial in the current plan
        nextPlan = null;
    }

    //quand on click sur un mask
    public void GetMask()
    {
        currentMaskPosition = EventSystem.current.currentSelectedGameObject.GetComponent<Mask>().maskPosition;
        player.targetPosition = currentMaskPosition;
    }
}
