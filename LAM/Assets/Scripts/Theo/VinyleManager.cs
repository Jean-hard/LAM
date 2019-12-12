using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VinyleManager : MonoBehaviour
{
    // Audiosources
    public AudioSource musique;
    public AudioSource bruitBlanc;
    public AudioSource craquement;

    // valeurs des différentes vitesses
    public float slowSpeed = 0.5f;
    public float normalSpeed = 1f;
    public float fastSpeed = 1.5f;
    public float vitesseBaseVinyle = 120f;
    public int volumePosition = 0;
    public float volumeBruitBlancInitial = 0.3f;
    public Vector2 zoneVictoireBras = new Vector2(276f, 286f);
    public Vector2 limitesSlider = new Vector2(0.6f, 0.9f); // les 2 valeurs du slider entre lesquelles le slider est en position de victoire

    public float bonneValeurBruitBlanc = 0.7f;
    public GameObject boutonVolume;

    public GameObject bras;
    public GameObject vinyle;
    private bool vinyleTourne = false;
    public Slider slider;

    public float[] tabVolumeValeurs = { 0.1f, 0.3f, 0.5f }; // valeurs de volume
    private float[] tabVolumePositions = { 90, 0, 270 }; // angles des 3 crans
    private float angleVinyleMin; // les valeurs d'angles (z) du bras de la platine entre lesquelles il est placé sur le vinyle
    private float angleVinyleMax;

    // conditions de victoire : bras bien placé, volume activé, fréquence bien placée, vitesse slow
    private bool conditionRemplie = false;

    // Dialogue
    [SerializeField]
    private Dialogue vinyleWinDialogue;
    private bool dialogueDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        angleVinyleMin = bras.GetComponent<BrasController>().angleVinyleMin;
        angleVinyleMax = bras.GetComponent<BrasController>().angleVinyleMax;

        // animator.enabled = false; // le vinyle tourne disque est éteint à l'arrivée sur le plan
        musique.volume = 0f; // set le volume au début du jeu
        bruitBlanc.Pause();
        bruitBlanc.volume = volumeBruitBlancInitial;
    }

    enum Vitesses { SLOW, NORMAL, FAST };
    private Vitesses vitesseVinyle;

    // Update is called once per frame
    void Update()
    {
        bool brasBienPlace = zoneVictoireBras.x < bras.transform.eulerAngles.z && bras.transform.eulerAngles.z < zoneVictoireBras.y;
        bool freqBienPlace = limitesSlider.x < slider.value && slider.value < limitesSlider.y;
        bool volumeAuMax = musique.volume == tabVolumeValeurs[tabVolumeValeurs.Length - 1];
        conditionRemplie = brasBienPlace && freqBienPlace && volumeAuMax && vinyleTourne && vitesseVinyle == Vitesses.SLOW;
        // conditionRemplie = brasBienPlace && freqBienPlace && volumeAuMax && animator.speed == slowSpeed && animator.enabled;

        if (conditionRemplie && !dialogueDisplayed)
        {
            GameManager.Instance.InitDialogue(vinyleWinDialogue);
            dialogueDisplayed = true;
            Debug.Log("VICTOIRE");
        }

        if (!vinyleTourne)
        {
            musique.Pause();
            bruitBlanc.Pause();
            craquement.Pause();
        }
        else
        {
            DonneVitesse();
        }
    }

    //-----------------SLIDER------------------

    public void AjusteBruitBlanc(Slider slider)
    {
        bruitBlanc.volume = Mathf.Abs(bonneValeurBruitBlanc - slider.value) * volumeBruitBlancInitial; // plus le slider est proche de la bonne valeur, moins il y a de bruit blanc
    }

    //---------------VOLUME--------------------

    public void ChangeVolume()
    {
        // le bouton change de cran
        if (volumePosition < 2)
        {
            volumePosition++;
        }
        else
        {
            volumePosition = 0;
        }
        boutonVolume.transform.rotation = Quaternion.Euler(0, 0, tabVolumePositions[volumePosition]);

        // le volume est changé seulement si le bras de la platine est sur le vinyle
        float zAngleBras = bras.transform.eulerAngles.z;
        if (angleVinyleMin < zAngleBras && zAngleBras < angleVinyleMax)
        {
            musique.volume = tabVolumeValeurs[volumePosition];
        }
    }

    //----------------START/STOP---------------

    public void StartStop()
    {
        if (vinyleTourne)
        {
            vinyleTourne = false;
            // animator.enabled = false;
            musique.Pause();
            bruitBlanc.Pause();
            craquement.Pause();
        }
        else
        {
            // animator.enabled = true;
            vinyleTourne = true;
            musique.Play();
            bruitBlanc.Play();
            craquement.Play();
        }
    }

    //-------------SPEED---------------

    public void SpeedSlow()
    {
        vitesseVinyle = Vitesses.SLOW;
        // animator.speed = slowSpeed;
        musique.pitch = slowSpeed;
    }

    public void SpeedNormal()
    {
        vitesseVinyle = Vitesses.NORMAL;
        // animator.speed = normalSpeed;
        musique.pitch = normalSpeed;
    }

    public void SpeedFast()
    {
        vitesseVinyle = Vitesses.FAST;
        // animator.speed = fastSpeed;
        musique.pitch = fastSpeed;
    }

    private void DonneVitesse()
    {
        switch (vitesseVinyle)
        {
            case Vitesses.SLOW:
                vinyle.transform.Rotate(0, 0, -slowSpeed * vitesseBaseVinyle * Time.deltaTime);
                break;
            case Vitesses.NORMAL:
                vinyle.transform.Rotate(0, 0, -normalSpeed * vitesseBaseVinyle * Time.deltaTime);
                break;
            case Vitesses.FAST:
                vinyle.transform.Rotate(0, 0, -fastSpeed * vitesseBaseVinyle * Time.deltaTime);
                break;
        }
    }
}
