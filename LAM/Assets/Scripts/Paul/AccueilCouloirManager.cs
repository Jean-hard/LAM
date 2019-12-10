using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilCouloirManager : MonoBehaviour
{
    [SerializeField]
    private Dialogue aubergisteTalkDialogue;

    public static bool aubergisteTalkDone;
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
}
