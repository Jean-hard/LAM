using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : MonoBehaviour
{
    /**
     * bool to check if all the enigma in the room has been done succesfully
     */
    public static bool room1EnigmaDone;

    public static bool puzzleDone;
    public static bool horlogeDone;
    public static bool oeufVisited;

    [SerializeField]
    private Dialogue peintureDialogue;

    // SINGLETON ---------------------------------------------
    private static Room1Manager _instance;

    public static Room1Manager Instance { get { return _instance; } }

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

    public void OeufIsVisited()
    {
        oeufVisited = true;
    }

    public void CheckGamesDone(PlaneScript peinturePlan)
    {
        Debug.Log("Oeuf: " + oeufVisited + ", horloge: " + horlogeDone + ", puzzle: " + puzzleDone);
        if (horlogeDone && puzzleDone && oeufVisited)
        {
            GameManager.Instance.ChangePlan(peinturePlan);
        }
        else
        {
            GameManager.Instance.InitDialogue(peintureDialogue);
        }
    }
}
