using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script pour le dialogue : DIA_CINE_01
 */
public class DialogueCinematique : MonoBehaviour
{
    public FadeScript[] cine1Dialogues;
    public FadeScript[] cine2Dialogues;
    public GameObject continueButton;
    public GameObject skipTextButton;
    
    public int indexParagraphe;
    private int indexCinematique;

    [SerializeField]
    private int timeBeforeStartDialogue;
    [SerializeField]
    private int timeBeforeNextDialogue;

    private bool isFirstCinematiquePassed = false;

    public void LaunchCinematique()
    {
        //si on a déjà lancé la 1ere cinématique, on lancera la 2eme
        if (isFirstCinematiquePassed == true)
            indexCinematique++;
        else
            isFirstCinematiquePassed = true;
        StartCoroutine(CinematiqueLaunchDialogueTimer());
    }

    //lance le premier paragraphe
    public void StartDialogue()
    {
        indexParagraphe = 0;

        if (indexCinematique == 0)
            cine1Dialogues[indexParagraphe].FadeIn();
        else if (indexCinematique == 0)
            cine2Dialogues[indexParagraphe].FadeIn();
        else
            Debug.Log("Error : il n'y a plus de cinematique à lancer");

        continueButton.SetActive(true);
        skipTextButton.SetActive(true);
    }

    //appelé quand on appuie sur le bouton continuer
    public void NextSentence()
    {
        continueButton.SetActive(false);
        skipTextButton.SetActive(false);

        if (indexCinematique == 0)
        {
            cine1Dialogues[indexParagraphe].FadeOut();
            indexParagraphe++;
            if (indexParagraphe < cine1Dialogues.Length)    //tant qu'il reste un paragraphe à afficher
                StartCoroutine(DisplayNextDialogueTimer());
        }
        else
        { 
            cine2Dialogues[indexParagraphe].FadeOut();
            indexParagraphe++;
            if (indexParagraphe < cine2Dialogues.Length)    //tant qu'il reste un paragraphe à afficher
                StartCoroutine(DisplayNextDialogueTimer());
        }

        if (indexCinematique == 0)
        {
            if (indexParagraphe >= cine1Dialogues.Length)
            {
                Debug.Log("in end cinematique");
                AccueilCouloirManager.cinematique1Done = true;
                GameManager.Instance.EndCinematique(1);
            }
            else
                return;
        }
        else if (indexCinematique == 1)
        {
            if (indexParagraphe >= cine2Dialogues.Length)
            {
                AccueilCouloirManager.cinematique2Done = true;
                GameManager.Instance.EndCinematique(2);
            }
            else
                return;
        }

        GameManager.Instance.BackToAccueil();
        AccueilCouloirManager.isStairLock = true;
        AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();
    }

    //appelé par le bouton skip dialogue
    public void StopDialogue()
    {
        indexParagraphe = 0;

        if (indexCinematique == 0)
        {
            AccueilCouloirManager.cinematique1Done = true;
            GameManager.Instance.EndCinematique(1);
        }
        if (indexCinematique == 1)
        {
            AccueilCouloirManager.cinematique2Done = true;
            GameManager.Instance.EndCinematique(2);
        }

        GameManager.Instance.BackToAccueil();
        AccueilCouloirManager.isStairLock = true;
        AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();
    }

    //pour lancer le dialogue quand le fade du lancement de la cinematique est finie
    private IEnumerator CinematiqueLaunchDialogueTimer()
    {
        yield return new WaitForSeconds(timeBeforeStartDialogue);
        StartDialogue();
    }

    //pour lancer le dialogue quand le fade du précédent dialogue est finie
    private IEnumerator DisplayNextDialogueTimer()
    {
        yield return new WaitForSeconds(timeBeforeNextDialogue);
        if (indexCinematique == 0)
        {
            cine1Dialogues[indexParagraphe].FadeIn();
        }
        else if (indexCinematique == 1)
        {
            cine2Dialogues[indexParagraphe].FadeIn();
        }
        yield return new WaitForSeconds(.5f);
        continueButton.SetActive(true);
        skipTextButton.SetActive(true);
    }
}