using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
    private bool soundHasBeenPlayed;

    void Update()
    {
        if (indexCinematique == 0 && indexParagraphe == 1 && !soundHasBeenPlayed)
        {
            SoundManager.Instance.PlayThunderP1();
            soundHasBeenPlayed = true;
        }

        if (indexCinematique == 0 && indexParagraphe == 2 && !soundHasBeenPlayed)
        {
            SoundManager.Instance.PlayThunderP2();
            soundHasBeenPlayed = true;
        }

        if (indexCinematique == 0 && indexParagraphe == 5 && !soundHasBeenPlayed)
        {
            SoundManager.Instance.PlayBoatCrash();
            soundHasBeenPlayed = true;
        }
    }

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

        PlayCine1LoopSounds();
        SoundManager.Instance.StopBO();

        if (indexCinematique == 0)
            cine1Dialogues[indexParagraphe].FadeIn();
        else if (indexCinematique == 1)
            cine2Dialogues[indexParagraphe].FadeIn();
        else
            Debug.Log("Error : il n'y a plus de cinematique à lancer");

        continueButton.SetActive(true);
        skipTextButton.SetActive(true);
    }

    //appelé quand on appuie sur le bouton continuer
    public void NextSentence()
    {
        soundHasBeenPlayed = false;
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

        ResetDialogues();

        //on eteind les sons de la cinematique
        SoundManager.Instance.StopCineSounds();
        SoundManager.Instance.PlayBO();
        GameManager.Instance.BackToAccueil();
        AccueilCouloirManager.isStairLock = true;
        AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();

        //on eteind les sons de la cinematique
        SoundManager.Instance.StopCineSounds();
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

        ResetDialogues();
        GameManager.Instance.BackToAccueil();
        AccueilCouloirManager.isStairLock = true;
        AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();

        //on eteind les sons de la cinematique
        SoundManager.Instance.StopCineSounds();
        SoundManager.Instance.PlayBO();
    }

    public void ResetDialogues()
    {
        if (indexCinematique == 0)
        {
            for (int i = 0; i < cine1Dialogues.Length; i++)
            {
                cine1Dialogues[i].gameObject.SetActive(false);
                // on set tous les textes des dialogues à transparent complet
                cine1Dialogues[i].gameObject.GetComponent<Text>().color = new Color(
                    cine1Dialogues[i].gameObject.GetComponent<Text>().color.r,
                    cine1Dialogues[i].gameObject.GetComponent<Text>().color.g,
                    cine1Dialogues[i].gameObject.GetComponent<Text>().color.b,
                    0);
            }
        }
        if (indexCinematique == 1)
        { 
            for (int i = 0; i < cine2Dialogues.Length; i++)
            {
                cine2Dialogues[i].gameObject.SetActive(false);
                // on set tous les textes des dialogues à transparent complet
                cine2Dialogues[i].gameObject.GetComponent<Text>().color = new Color(
                    cine2Dialogues[i].gameObject.GetComponent<Text>().color.r,
                    cine2Dialogues[i].gameObject.GetComponent<Text>().color.g,
                    cine2Dialogues[i].gameObject.GetComponent<Text>().color.b,
                    0);
            }
        }
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

    public void PlayCine1LoopSounds()
    {
        SoundManager.Instance.PlayOceanLoop();
        SoundManager.Instance.PlayStormLoop();
    }
}