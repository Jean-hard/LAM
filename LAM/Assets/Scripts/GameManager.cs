using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject firstChamber;
    [SerializeField]
    private GameObject firstMask;

    private string clickedBtnName;
    private Vector3 currentMaskPosition;

    private Vector3 currentDestination;
    [SerializeField]
    private PlaneScript currentPlan;
    private PlaneScript nextPlan;

    private Vector3 positionOnRightDoor = new Vector3(6f, 2.5f, 0);
    private Vector3 positionOnLeftDoor = new Vector3(-6f, 2.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        currentDestination = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //si le personnage atteint la position de la porte de droite
        //if (player.transform.position == positionOnRightDoor)
        //{
        //    ChangeScene();
        //}
        if (player.transform.position == currentMaskPosition)
        {
            //placer le mask dans l'inventaire
            firstMask.SetActive(false); //pour l'instant on le désactive juste
        }

        if (player.transform.position == currentDestination)
        {
            ChangeScene();
        }
    }

    //on gère un seul personnage donc autant gérer ça dans le gameManager
    //public void MoveToRightDoor()
    //{
    //    player.targetPosition = positionOnRightDoor;
    //}

    //public void MoveToLeftDoor()
    //{
    //    player.targetPosition = positionOnLeftDoor;
    //}

    public void MoveToDoor(DoorScript targetDoor)
    {
        currentDestination = targetDoor.doorPosition;
        player.targetPosition = targetDoor.doorPosition;
        nextPlan = targetDoor.planeNextDoor;
    }

    //a chaque changement de plan on placera le personnage à une position de base et on préparera un fade dans une coroutine
    public void ChangeScene()
    {
        player.targetPosition = player.PLAYER_BASE_POS;

        //if(hallway.activeSelf)
        //{
        //    hallway.SetActive(false);
        //    firstChamber.SetActive(true);
        //    firstMask.SetActive(true);
        //}
        //else
        //{
        //    hallway.SetActive(true);
        //    firstChamber.SetActive(false);
        //    firstMask.SetActive(false);
        //}

        Debug.Log("on change scene");

        nextPlan.OnActive();
        currentPlan.OnDesactive();
        currentPlan = nextPlan;
    }

    //ce délai servira à limiter les interactions juste après un changement de plan (et à éviter des bugs... surtout)
    public IEnumerator ChangeSceneDelay()
    {
        yield return new WaitForSeconds(3.0f);
    }

    //quand on click sur un mask
    public void GetMask()
    {
        currentMaskPosition = EventSystem.current.currentSelectedGameObject.GetComponent<Mask>().maskPosition;
        player.targetPosition = currentMaskPosition;
    }
}
