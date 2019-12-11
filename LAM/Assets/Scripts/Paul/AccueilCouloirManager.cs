using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilCouloirManager : MonoBehaviour
{
    public static bool aubergisteTalkDone;
    public static bool soloTalk01Ready = true;
    public static bool soloTalk02Ready = false;



    [SerializeField]
    private Dialogue aubergisteTalkDialogue;

    [SerializeField]
    private Dialogue[] aubergisteDiaTab;
    private int indexAubergisteDia = 0;

    [SerializeField]
    private Dialogue[] couloirDiaTab;
    private int indexCouloirDia = 0;

    private Dialogue currentAubergisteDia;
    private Dialogue currentCouloirDia;


    // SINGLETON ---------------------------------------------
    private static AccueilCouloirManager _instance;

    public static AccueilCouloirManager Instance { get { return _instance; } }

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
        currentAubergisteDia = aubergisteDiaTab[0];
        currentCouloirDia = couloirDiaTab[0];
    }

    public void LaunchAubergisteDia()
    {
        GameManager.Instance.InitDialogue(currentAubergisteDia);
        aubergisteTalkDone = true;
    }

    public void CheckAubergisteTalkDone(DoorScript stairDoor)
    {
        Debug.Log("aubergiste talk : " + aubergisteTalkDone);
        if (aubergisteTalkDone)
        {
            GameManager.Instance.MoveToDoor(stairDoor);
        }
        else
        {
            GameManager.Instance.InitDialogue(aubergisteTalkDialogue);
        }
    }

    public void UpdateAubergisteDia()
    {
        indexAubergisteDia++;
        currentAubergisteDia = aubergisteDiaTab[indexAubergisteDia];
    }

    public void ShowCouloirDia()
    {
        //si le premier dialogue solo n'a pas été fait lorsque qu'on arrive à l'étage
        if (soloTalk01Ready == true)
        {
            GameManager.Instance.InitDialogue(currentCouloirDia);
            soloTalk01Ready = false;
        }
        //si éléments nécessaire sont prêt pour le second dialogue à l'étage
        if (soloTalk02Ready == true)
        {
            indexCouloirDia++;
            currentCouloirDia = couloirDiaTab[indexCouloirDia];
            GameManager.Instance.InitDialogue(currentCouloirDia);
            soloTalk02Ready = false;
        }
    }
}
