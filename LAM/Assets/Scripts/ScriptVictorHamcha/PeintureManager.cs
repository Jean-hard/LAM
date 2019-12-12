using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Linq;

public class PeintureManager : MonoBehaviour
{
    /**
     * Utile
     */
    [SerializeField]
    private SFadeScript fadeScript;

    public Button[] colorButtons = new Button[6];   // tableau des boutons couleur de la palette
    public GameObject[] colorNameText = new GameObject[6];    // tableau contenant tous les gameobjects de noms de couleurs qui vont s'afficher

    private string[] badColorTagsList = {"Coquelicot","Taupe","Ocean"}; // tableau de tags correspondant aux mauvaises couleurs
    private GameObject[] peintureTab = new GameObject[4];
    public GameObject firstStep;    // sprite de la première bonne peinture
    public GameObject secondStep;   // sprite de la deuxième bonne peinture
    public GameObject finalGoodStep;    // sprite de la troisième bonne peinture
    public GameObject finalBadStep;     // sprite de la mauvaise peinture si on a pas entré les bonnes couleurs
    public GameObject paletteCouleur;     // gameObject contenant les boutons couleurs
    public GameObject toileButton;      // toile sur laquelle on va clicker pour appliquer une "couleur"
    private GameObject currentButtonClicked;

    private GameObject colorChoosed;    // couleur choisie par le joueur

    private bool colorSelected;
 
    private bool choosedBadColor;
    private int indexStep = 0;

    private void Start()
    {
        peintureTab[0] = firstStep;
        peintureTab[1] = secondStep;
        peintureTab[2] = finalGoodStep;
        peintureTab[3] = finalBadStep;

        for (int i = 0; i < colorNameText.Length; i++)
        {
            colorNameText[i].SetActive(false);
        }
    }

    // appelée quand on clicke sur une couleur pour afficher son nom
    public void DisplayColorName()
    {
        for (int i = 0; i < colorNameText.Length; i++)      // on passe tous les textes à false pour pas qu'ils se chevauchent
        {
            colorNameText[i].SetActive(false);
        }

        currentButtonClicked = EventSystem.current.currentSelectedGameObject;   // on récup le tag du bouton clické
        for (int i = 0; i < colorNameText.Length; ++i)
        {
            // si le tag du bouton clické est le même qu'un des noms de couleur
            if (currentButtonClicked.CompareTag(colorNameText[i].tag))      
            {
                colorChoosed = colorNameText[i];
                colorNameText[i].SetActive(true);   // on active le nom de la couleur clickée
                colorSelected = true;
            }
        }
    }

    public void CheckColorChoosed()
    {
        if (colorSelected)   // si on a bien choisi une couleur
        {
            for (int i = 0; i < colorNameText.Length; i++)
            {
                colorNameText[i].SetActive(false);
            }

            if (indexStep == 2 && choosedBadColor)      // si on a choisi une mauvaise couleur parmi les 3, indexstep passe à 3 --> défaite
            {
                indexStep = 3;
                DisplayPaintLayer();
            }
            else
            {
                DisplayPaintLayer();
            }

            indexStep++;
            colorSelected = false;
        }
    }

    public void DisplayPaintLayer()
    {
        if (badColorTagsList.Contains(colorChoosed.tag))    // si la peinture qu'on applique fait partie de la liste des mauvaises couleurs
            choosedBadColor = true;     // on sait maintenant que sur les 3 couleurs choisies, le joueur en a au moins une de fausse

        fadeScript.FadeIn();
        StartCoroutine(WaitForDisplayLayer());
        peintureTab[indexStep].SetActive(true);
        fadeScript.FadeOut();

        // Win
        if (indexStep == 2)
        {
            paletteCouleur.SetActive(false);
        }

        // Loose
        if (indexStep == 3)
        {
            StartCoroutine(ResetPeinture());
        }
    }

    // affiche la peinture
    public IEnumerator WaitForDisplayLayer()
    {
        yield return new WaitForSeconds(1f);
    }

    public IEnumerator ResetPeinture()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < peintureTab.Length; i++)
            peintureTab[i].SetActive(false);
        indexStep = 0;
        choosedBadColor = false;
    }
}
