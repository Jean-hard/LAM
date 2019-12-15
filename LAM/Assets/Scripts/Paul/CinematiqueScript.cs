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

    private bool isFirstCinematiquePassed = false;

    public void LaunchCinematique()
    {
        //si on a déjà lancé la 1er cinématique, on lancera la 2eme
        if (isFirstCinematiquePassed == true)
            indexCinematiqueDia++;
        else
            isFirstCinematiquePassed = true;
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

    //pour lancer le dialogue que le fade est finie
    private IEnumerator CinematiqueLaunchDialogueTimer()
    {
        yield return new WaitForSeconds(timeBeforeStartDialogue);
        cinematiqueDiaTab[indexCinematiqueDia].StartDialogue();
    }

    public void EndCinematique()
    {
        GameManager.Instance.EndCinematique(0);
    }
}
