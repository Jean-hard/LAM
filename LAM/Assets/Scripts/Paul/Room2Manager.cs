﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Manage events between enigmas
 */

public class Room2Manager : MonoBehaviour
{
    public static bool soloTalkRoom21Ready = false;

    [SerializeField]
    private Dialogue enigmaDoneDia;

    [SerializeField]
    private GameObject mirrorButton;

    [SerializeField]
    private GameObject boardButton;

    [SerializeField]
    private Dialogue boardDialogue;

    [SerializeField]
    private Dialogue mirrorDialogue;
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

    // Quand on clique sur la photo dans le tiroir, on active le mirrorButton.
    public void EnigmaDrawerOpened()
    {
        tiroirOpened = true;
        mirrorButton.SetActive(true);
    }

    // Quand on clique sur le miroir, on lance le dialogue
    public void ClickOnMirror()
    {
        GameManager.Instance.InitDialogue(mirrorDialogue);
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

    public void LaunchDiaEnigmaDone()
    {
        GameManager.Instance.InitDialogue(enigmaDoneDia);
        AccueilCouloirManager.Instance.UpdateAubergisteDia();
    }

    // EN ATTENDANT QUE THEO NE SOIS PLUS DEFONCE
    public void ValidVinyleGame()
    {
        vinyleDone = true;
    }

    // EN ATTENDANT QUE THEO NE SOIS PLUS DEFONCE
    public void ValidAllGame()
    {
        telephoneDone = true;
        vinyleDone = true;
        deadBodyEnigmaDone = true;
    }
}
