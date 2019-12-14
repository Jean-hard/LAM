using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room1Manager : MonoBehaviour
{
    public static bool soloTalkRoom11Ready = true;
    public static bool soloTalkRoom12Ready = false;

    /**
     * bool to check if all the enigma in the room has been done succesfully
     */
    public static bool puzzleDone;
    public static bool horlogeDone;
    public static bool oeufVisited;

    [SerializeField]
    private Dialogue[] room1DiaTab;
    private int indexRoomDia = 0;

    [SerializeField]
    private Dialogue peintureDialogue;

    private Dialogue currentRoom1Dia;

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

    public void Start()
    {
        currentRoom1Dia = room1DiaTab[indexRoomDia];
    }

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

    public void ShowRoom1Dia()
    {
        if (soloTalkRoom11Ready == true)
        {
            GameManager.Instance.InitDialogue(currentRoom1Dia);
            soloTalkRoom11Ready = false;
            SoundManager.Instance.PlayPassifWind(1);
        }

        ///passe a true à la résolution de l'énigme de la chambre 1
        if (soloTalkRoom12Ready == true)
        {
            indexRoomDia++;
            currentRoom1Dia = room1DiaTab[indexRoomDia];
            GameManager.Instance.InitDialogue(currentRoom1Dia);
            soloTalkRoom12Ready = false;
            //débloque le dialogue DIA_AUBER_02
            AccueilCouloirManager.Instance.UpdateAubergisteDia();
        }
    }
}
