using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Dialogue : MonoBehaviour
{
    /**
     * On gére l'apparition des phrases
     * en fonction de qui parle on modifie l'actuelle zone de texte utilisé.
     */
    public TextMeshProUGUI currentTextDisplay;

    public string[] sentences;
    public SPEAKER[] speakers;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject blockScreen;
    public GameObject textBox;
    public GameObject skipTextButton;
    public Text speakerName;

    public enum SPEAKER
    {
        PLAYER,
        INNKEEPER,
        CAT
    }

    IEnumerator TypeSentence () 
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            currentTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
        if (skipTextButton != null)
            skipTextButton.SetActive(true);
    }

    public void StartDialogue()
    {
        ShowText();
        currentTextDisplay.gameObject.SetActive(true);
        blockScreen.gameObject.SetActive(true);
        textBox.SetActive(true);
        if (skipTextButton != null)
            skipTextButton.SetActive(true);
        speakerName.gameObject.SetActive(true);
    }

    /**
     * Reset le dialogue et desactive la UI de texte
     */
    public void StopDialogue()
    {
        index = 0;
        currentTextDisplay.text = "";
        currentTextDisplay.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        blockScreen.gameObject.SetActive(false);
        textBox.SetActive(false);
        if (skipTextButton != null)
            skipTextButton.SetActive(false);
        speakerName.gameObject.SetActive(false);
    }

    public void NextSentence()
    {
        index++;
        continueButton.SetActive(false);
        skipTextButton.SetActive(false);

        if (index < sentences.Length)
            ShowText();
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
        }
    }

    private void ShowText()
    {
        //on affiche le nom de celui qui parle
        if (speakers[index] == SPEAKER.PLAYER)
            speakerName.text = "Seiji";
        else if (speakers[index] == SPEAKER.INNKEEPER)
            speakerName.text = "Aubergiste";
        else
            speakerName.text = "Chat";

        currentTextDisplay.text = "";
            StartCoroutine(TypeSentence());

    }
}
