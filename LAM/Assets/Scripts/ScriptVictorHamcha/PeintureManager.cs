using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeintureManager : MonoBehaviour
{
    /**
     * Utile
     */
    [SerializeField]
    private SFadeScript fadeScript;
    private GameObject[] peintureTab = new GameObject[4];
    public GameObject firstStep;//sprite de la première bonne peinture
    public GameObject secondStep;//sprite de la deuxième bonne peinture
    public GameObject finalGoodStep;//sprite de la troidième bonne peinture
    public GameObject finalBadStep; //la peinture si on a pas entré les bonnes couleurs

    private bool isColorsGood = true;
    private int indexStep = 0;
    
    public bool goodAnswer1;//quand on mets la bonne peinture 1 
    public bool goodAnswer2;//quand on mets la bonne peinture 2
    public bool goodAnswer3;//quand on mets la bonne peinture 3
    public bool activefondue;

    private Color opacity1;//couleur ou on change l'opacité de la peinture 1
    private Color opacity2;//couleur ou change l'opacité de la peinture 2
    private Color opacity3;// couleur ou on change l'opacité de la peintue 3
    private Color fondueopacity;//couleur de la fondue

    public GameObject palettedecouleur; //les bouton palette de couleur

    private void Start()
    {
        peintureTab[0] = firstStep;
        peintureTab[1] = secondStep;
        peintureTab[2] = finalGoodStep;
        peintureTab[3] = finalBadStep;
    }

    /**
     * tant que les bonnes couleurs sont entré, on affiche les différentes peintures.
     * Si une couleurs entré était une mauvaise, la dernière peinture sera la BadPeinture
     * Si toutes les bonnes couleurs on été entré, la good peinture sera révélé.
     */
    public void GoodColor()
    {
        if (indexStep == 2 && isColorsGood == false)
        {
            indexStep = 3;
            StartCoroutine(ShowPeinture());
        }
        else
        {
            StartCoroutine(ShowPeinture());
        }
    }

    //TODO : Finir les commentaires
    public void BadColor()
    {
        isColorsGood = false;
        if (indexStep == 2 && isColorsGood == false)
        {
            indexStep = 3;
            StartCoroutine(ShowPeinture());
        }
        else
        {
            StartCoroutine(ShowPeinture());
        }
    }

    public IEnumerator ShowPeinture()
    {
        fadeScript.FadeIn();
        yield return new WaitForSeconds(1f);
        peintureTab[indexStep].SetActive(true);
        fadeScript.FadeOut();
        //Win
        if (indexStep == 2)
        {
            Debug.Log("WIN");
            palettedecouleur.SetActive(false);
        }
        //Loose
        if (indexStep == 3)
        {
            Debug.Log("Loose");
            StartCoroutine(ResetPeinture());
        }
        indexStep++;
    }

    public IEnumerator ResetPeinture()
    {
        yield return new WaitForSeconds(3f);
        for (int i = 0; i < peintureTab.Length; i++)
            peintureTab[i].SetActive(false);
        indexStep = 0;
        isColorsGood = true;
    }
}
