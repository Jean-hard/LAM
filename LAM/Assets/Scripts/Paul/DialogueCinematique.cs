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
            cinematiqueScript.EndCinematique();
            GameManager.Instance.BackToAccueil();
            AccueilCouloirManager.Instance.ShowAfterCinematiqueDia();
            AccueilCouloirManager.cinematique1Done = true;
        }
    }
}
