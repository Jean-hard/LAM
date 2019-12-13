﻿using System.Collections;
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
}