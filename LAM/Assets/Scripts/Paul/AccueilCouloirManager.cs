using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilCouloirManager : MonoBehaviour
{
    public static bool aubergisteTalkDone;

    [SerializeField]
    private Dialogue aubergisteTalkDialogue;

    [SerializeField]
    private Dialogue[] AubergisteDiaTab;
    private int indexAubergisteDia = 0;

    private Dialogue currentAubergisteDia;

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
        currentAubergisteDia = AubergisteDiaTab[0];
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
        currentAubergisteDia = AubergisteDiaTab[indexAubergisteDia];
    }

}
