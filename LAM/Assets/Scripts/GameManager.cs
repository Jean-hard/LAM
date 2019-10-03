using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private GameObject secondMask;
    [SerializeField]
    private GameObject maskCollection;

    private string clickedBtnName;
    private Vector3 currentMaskPosition;

    private Vector3 positionOnRightDoor = new Vector3(6f, 2.5f, 0);
    private Vector3 positionOnLeftDoor = new Vector3(-6f, 2.5f, 0);
    Component[] components;

    // Start is called before the first frame update
    void Start()
    {
        components = maskCollection.GetComponentsInChildren(typeof(Image), true);

        for (int i = 0; i < components.Length; i++)
        {
            GameObject newMask = Instantiate(components[i].gameObject, new Vector2(80f * i + 50f, 700f), Quaternion.identity);
            newMask.transform.localScale = 0.9f * newMask.transform.localScale;
            newMask.SetActive(false);
            Destroy(newMask.GetComponent<Button>()); // enlève le clic sur le masque
            foreach (Transform child in newMask.transform) // enlève le texte "Button"
            {
                Destroy(child.gameObject);
            }
            newMask.transform.SetParent(maskCollection.transform.parent.transform); // met le masque dans le canvas
        }
    }

    // Update is called once per frame
    void Update()
    {
        //si le personnage atteint la position de la porte de droite
        if (player.transform.position == positionOnRightDoor)
        {
            ChangeScene();
        }
        if (player.transform.position == currentMaskPosition)
        {
            //placer le mask dans l'inventaire
            EventSystem.current.currentSelectedGameObject.GetComponent<Mask>().SetIsFound(true);
            EventSystem.current.currentSelectedGameObject.SetActive(false);
            foreach (Component comp in maskCollection.transform.parent.GetComponentsInChildren(typeof(Image), true))
            {
                if (comp.gameObject.name == EventSystem.current.currentSelectedGameObject.name + "(Clone)")
                {
                    comp.gameObject.SetActive(true);
                }
            }
            currentMaskPosition = new Vector3();
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

        if (hallway.activeSelf)
        {
            hallway.SetActive(false);
            firstChamber.SetActive(true);
            firstMask.SetActive(true);
            secondMask.SetActive(false);
        }
        else
        {
            hallway.SetActive(true);
            firstChamber.SetActive(false);
            firstMask.SetActive(false);
            secondMask.SetActive(true);
        }
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