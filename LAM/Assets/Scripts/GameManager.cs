using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private PlayerManager player;
    [SerializeField]
    private GameObject hallway;
    [SerializeField]
    private GameObject firstChamber;

    private Vector3 positionOnRightDoor = new Vector3(6f, 2.5f, 0);
    private Vector3 positionOnLeftDoor = new Vector3(-6f, 2.5f, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //si le personnage atteint la position de la porte de droite
        if (player.transform.position == positionOnRightDoor)
        {
            ChangeScene();
        }
    }

    //on gère un seul personnage donc autant gérer ça dans le gameManager
    public void MoveToRightDoor()
    {
        player.targetPosition = positionOnRightDoor;
    }

    public void MoveToLeftDoor()
    {
        player.targetPosition = positionOnLeftDoor;
    }

    //a chaque changement de plan on placera le personnage à une position de base et on préparera un fade dans une coroutine
    public void ChangeScene()
    {
        player.targetPosition = player.PLAYER_BASE_POS;

        if(hallway.activeSelf)
        {
            hallway.SetActive(false);
            firstChamber.SetActive(true);
        }
        else
        {
            hallway.SetActive(true);
            firstChamber.SetActive(false);
        }
    }

    //ce délai servira à limiter les interactions juste après un changement de plan (et à éviter des bugs... surtout)
    public IEnumerator ChangeSceneDelay()
    {
        yield return new WaitForSeconds(3.0f);
    }
}
