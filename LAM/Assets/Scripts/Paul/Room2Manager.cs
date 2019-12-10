using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manage events between enigmas
 */

public class Room2Manager : MonoBehaviour
{
    [SerializeField]
    private GameObject mirrorButton;

    [SerializeField]
    private GameObject boardButton;

    [SerializeField]
    private Dialogue boardDialogue;
    /**
     * bool to check if all the enigma in the room has been done succesfully
     */
    public static bool vinyleDone = false;
    public static bool telephoneDone = false;
    public static bool deadBodyEnigmaDone = false;// à modifier

    /**
     * check if the right drawer has been open
     */
    public static bool tiroirOpened = false;


    // SINGLETON ---------------------------------------------
    private static Room2Manager _instance;

    public static Room2Manager Instance { get { return _instance; } }

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
        mirrorButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // TODO: pour plus tard il faudra que ce soit quand on clique sur la photo dans le tirroir que l'on activera le mirrorButton.
    public void EnigmaDrawerOpened()
    {
        tiroirOpened = true;
        mirrorButton.SetActive(true);
    }

    //TEMPORAIRE
    public void ClickOnMirror()
    {
        Debug.Log("ON ATTEND L'IMAGE POUR LE MIROIR !");
        //TEMPORAIRE, devra être appelé à la vraie résolution de l'énigme
        deadBodyEnigmaDone = true;
    }

    public void CheckGamesDone(PlaneScript BoardPlan)
    {
        Debug.Log("vinyle: " + vinyleDone + ", telephone: " + telephoneDone + ", deadEnigma: " + deadBodyEnigmaDone);
        if (vinyleDone && telephoneDone && deadBodyEnigmaDone)
        {
            GameManager.Instance.ChangePlan(BoardPlan);
        }
        else
        {
            GameManager.Instance.InitDialogue(boardDialogue);
        }
    }
}
