using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script pour le dialogue : DIA_CINE_01
 */
public class DialogueCinematique : Dialogue
{
    [SerializeField]
    private CinematiqueScript cinematiqueScript;

    public enum CINEMATIQUE_ID
    {
        CINE_01,
        CINE_02
    }
    public CINEMATIQUE_ID currenCinematqueID;

    public override void NextSentence()
    {
        index++;
        continueButton.SetActive(false);
        skipTextButton.SetActive(false);

        if (index < sentences.Length)
            ShowText();
        //fin du dialogue
        else
        {
            currentTextDisplay.text = "";
            continueButton.SetActive(false);
            if (skipTextButton != null)
                skipTextButton.SetActive(false);
            currentTextDisplay.gameObject.SetActive(false);
            blockScreen.SetActive(false);
            textBox.SetActive(false);
            if(speakerName != null)
                speakerName.gameObject.SetActive(false);
            index = 0;

            //pour mettre fin à la cinematique
            
            if (currenCinematqueID == CINEMATIQUE_ID.CINE_01)
            {
                AccueilCouloirManager.cinematique1Done = true;
                GameManager.Instance.EndCinematique(1);
            }
            if (currenCinematqueID == CINEMATIQUE_ID.CINE_02)
            {
                AccueilCouloirManager.cinematique2Done = true;
                GameManager.Instance.EndCinematique(2);
            }

            GameManager.Instance.BackToAccueil();
            AccueilCouloirManager.isStairLock = true;
            AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();
        }
    }

    public override void StopDialogue()
    {
        StopAllCoroutines();
        index = 0;
        currentTextDisplay.text = "";
        currentTextDisplay.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        blockScreen.gameObject.SetActive(false);
        textBox.SetActive(false);
        if (skipTextButton != null)
            skipTextButton.SetActive(false);
        if (speakerName != null)
            speakerName.gameObject.SetActive(false);

        if (currenCinematqueID == CINEMATIQUE_ID.CINE_01)
        {
            AccueilCouloirManager.cinematique1Done = true;
            GameManager.Instance.EndCinematique(1);
        }
        if (currenCinematqueID == CINEMATIQUE_ID.CINE_02)
        {
            AccueilCouloirManager.cinematique2Done = true;
            GameManager.Instance.EndCinematique(2);
        }

        GameManager.Instance.BackToAccueil();
        AccueilCouloirManager.isStairLock = true;
        AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();
    }
}