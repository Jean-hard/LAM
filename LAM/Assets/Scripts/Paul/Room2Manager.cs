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

    /**
     * bool to check if all the enigma in the room has been done succesfully
     */
    public static bool vinyleDone = false;
    public static bool telephoneDone = false;
    public static bool tiroirDone = false;// à modifier

    /**
     * check if the right drawer has been open
     */
    public static bool tiroirOpened = false;


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
        Debug.Log("ON ATTEND LE L'IMAGE POUR LE MIROIR !");
    }
}
