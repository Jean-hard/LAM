using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CinematiqueScript : MonoBehaviour
{
    [SerializeField]
    private DialogueCinematique[] cinematiqueDiaTab;
    private int indexCinematiqueDia;

    [SerializeField]
    private int timeBeforeStartDialogue;

    public void LaunchCinematique()
    {
        StartCoroutine(CinematiqueLaunchDialogueTimer());
    }

    public void DisplayDialogueNextSentence()
    {
        cinematiqueDiaTab[indexCinematiqueDia].NextSentence();
    }

    //bouton "skip dialogue"
    public void StopDialogue()
    {
        cinematiqueDiaTab[indexCinematiqueDia].StopDialogue();
        EndCinematique();
    }

    private IEnumerator CinematiqueLaunchDialogueTimer()
    {
        yield return new WaitForSeconds(timeBeforeStartDialogue);
        cinematiqueDiaTab[indexCinematiqueDia].StartDialogue();
    }

    public void EndCinematique()
    {
        GameManager.Instance.EndCinematique();
    }
}
