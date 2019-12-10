using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Dialogue : MonoBehaviour
{
    /**
     * On gére l'apparition des phrases
     * en fonction de qui parle on modifie l'actuelle zone de texte utilisé.
     */
    public TextMeshProUGUI playerTextDisplay;
    public TextMeshProUGUI otherTextDisplay;
    private TextMeshProUGUI currentTextDisplay;

    public string[] sentences;
    public SPEAKER[] speakers;
    private int index;
    public float typingSpeed;
    public GameObject continueButton;
    public GameObject blockScreen;
    public GameObject textBox;

    public enum SPEAKER
    {
        PLAYER,
        INNKEEPER,
        CAT
    }

    void Start()
    {
        //StartCoroutine(Type());
        currentTextDisplay = playerTextDisplay;
    }

    IEnumerator TypeSentence () 
    {
        foreach (char letter in sentences[index].ToCharArray())
        {
            currentTextDisplay.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
        continueButton.SetActive(true);
    }

    public void StartDialogue()
    {
        ShowText();
        playerTextDisplay.gameObject.SetActive(true);
        otherTextDisplay.gameObject.SetActive(true);
        blockScreen.gameObject.SetActive(true);
        textBox.SetActive(true);
    }

    /**
     * Reset le dialogue et desactive la UI de texte
     */
    public void StopDialogue()
    {
        index = 0;
        playerTextDisplay.text = "";
        playerTextDisplay.gameObject.SetActive(false);
        otherTextDisplay.text = "";
        otherTextDisplay.gameObject.SetActive(false);
        continueButton.gameObject.SetActive(false);
        blockScreen.gameObject.SetActive(false);
        textBox.SetActive(false);
    }

    public void NextSentence()
    {
        index++;
        continueButton.SetActive(false);

        if (index < sentences.Length)
            ShowText();
        else
        {
            currentTextDisplay.text = "";
            continueButton.SetActive(false);
            playerTextDisplay.gameObject.SetActive(false);
            otherTextDisplay.gameObject.SetActive(false);
            blockScreen.SetActive(false);
            textBox.SetActive(false);
            index = 0;
        }
    }

    private void ShowText()
    {
        //si cette phrase est dite par le player, la zone de texte est celle du bas sinon c'est celle du haut 
        if (speakers[index] == SPEAKER.PLAYER)
            currentTextDisplay = playerTextDisplay;
        else
            currentTextDisplay = otherTextDisplay;

            currentTextDisplay.text = "";
            StartCoroutine(TypeSentence());

    }
}
