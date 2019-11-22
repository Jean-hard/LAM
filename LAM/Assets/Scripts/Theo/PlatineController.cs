using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlatineController : MonoBehaviour
{
    public GameObject vinyle;
    public AudioSource musique;
    public AudioSource bruitBlanc;
    public AudioSource crackle;

    public Transform brasTransform;
    public Slider slider;

    public Vector2 limitesRotationZBras; // les 2 valeurs d'angles entre lesquelles le bras de la platine est en position de victoire
    public Vector2 limitesSlider; // les 2 valeurs du slider entre lesquelles le slider est en position de victoire

    // valeurs des différentes vitesses
    public float slowSpeed = 0.5f;
    public float normalSpeed = 1f;
    public float fastSpeed = 1.5f;

    private float[] volumes; // tableau des valeurs de volume disponibles 
    private Animator animator;

    // conditions de victoire : bras bien placé, volume activé, fréquence bien placée, vitesse slow
    private bool conditionRemplie = false;

    // Dialogue
    [SerializeField]
    private Dialogue myDialogue;
    private bool dialogueDisplayed;

    // Start is called before the first frame update
    void Start()
    {
        animator = vinyle.GetComponent<Animator>();
        // animator.enabled = false;
        volumes = GameObject.Find("VolumeKnob").GetComponent<VolumeController>().tabVolumes;

        // le vinyle tourne disque est éteint à l'arrivée sur le plan
        animator.enabled = false;
        musique.Pause();
        crackle.Pause();
        bruitBlanc.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        bool brasBienPlace = limitesRotationZBras.x < brasTransform.eulerAngles.z && brasTransform.eulerAngles.z < limitesRotationZBras.y;
        bool freqBienPlace = limitesSlider.x < slider.value && slider.value < limitesSlider.y;
        bool volumeAuMax = musique.volume == volumes[volumes.Length - 1];
        // Debug.Log(brasBienPlace);

        conditionRemplie = brasBienPlace && freqBienPlace && volumeAuMax && animator.speed == slowSpeed && animator.enabled;
        

        if (conditionRemplie && !dialogueDisplayed)
        {
            GameManager.Instance.InitDialogue(myDialogue);
            dialogueDisplayed = true;
            //Debug.Log("VICTOIRE");
        }

        if(animator.enabled == false)
        {
            musique.Pause();
            crackle.Pause();
            bruitBlanc.Pause();
        }
    }

    public void StartStop()
    {
        if (animator.enabled == true)
        {
            animator.enabled = false;
            musique.Pause();
            bruitBlanc.Pause();
            crackle.Pause();
        }
        else
        {
            animator.enabled = true;
            musique.Play();
            bruitBlanc.Play();
            crackle.Play();
        }
    }

    public void SpeedSlow()
    {
        animator.speed = slowSpeed;
        musique.pitch = slowSpeed;
    }

    public void SpeedNormal()
    {
        animator.speed = normalSpeed;
        musique.pitch = normalSpeed;
    }

    public void SpeedFast()
    {
        animator.speed = fastSpeed;
        musique.pitch = fastSpeed;
    }
}


