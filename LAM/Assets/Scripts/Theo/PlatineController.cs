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

    public Vector2 limitesRotationZBras;
    public Vector2 limitesSlider;

    // valeurs des différentes vitesses
    public float slowSpeed = 0.5f;
    public float normalSpeed = 1f;
    public float fastSpeed = 1.5f;

    private float[] volumes;
    private Animator animator;

    // conditions de victoire : bras bien placé, volume activé, fréquence bien placée, vitesse slow
    private bool conditionRemplie = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = vinyle.GetComponent<Animator>();
        // animator.enabled = false;
        volumes = GameObject.Find("VolumeKnob").GetComponent<VolumeController>().tabVolumes;
    }

    // Update is called once per frame
    void Update()
    {
        bool brasBienPlace = limitesRotationZBras.x < brasTransform.eulerAngles.z && brasTransform.eulerAngles.z < limitesRotationZBras.y;
        bool freqBienPlace = limitesSlider.x < slider.value && slider.value < limitesSlider.y;
        bool volumeAuMax = musique.volume == volumes[volumes.Length - 1];
        // Debug.Log(brasBienPlace);

        conditionRemplie = brasBienPlace && freqBienPlace && volumeAuMax && animator.speed == slowSpeed && animator.enabled;

        if (conditionRemplie)
        {
            Debug.Log("VICTOIRE");
        }
    }

    public void StartStop()
    {
        if (animator.enabled == true)
        {
            animator.enabled = false;
            musique.Pause();
        }
        else
        {
            animator.enabled = true;
            musique.Play();
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


