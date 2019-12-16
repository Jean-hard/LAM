using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialoguePreCinematique : Dialogue
{
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
            speakerName.gameObject.SetActive(false);
            index = 0;

            //lance la cinematique
            GameManager.Instance.LaunchCinematique();
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

        //lance la cinematique
        GameManager.Instance.LaunchCinematique();

    }
}
