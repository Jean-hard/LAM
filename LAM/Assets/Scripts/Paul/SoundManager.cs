using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSource;

    [SerializeField]
    private AudioClip doorOpening;
    [SerializeField]
    private AudioClip catMeow;
    [SerializeField]
    private AudioClip catHisses;
    [SerializeField]
    private AudioClip sea_Sound;
    [SerializeField]
    private AudioClip atticRun;
    [SerializeField]
    private AudioClip scratch;
    [SerializeField]
    private AudioClip wind_Sound;
    [SerializeField]
    private AudioClip nightOwl;
    [SerializeField]
    private AudioClip openDrawer;
    [SerializeField]
    private AudioClip closeDrawer;
    [SerializeField]
    private AudioClip flashBackOpening;
    [SerializeField]
    private AudioClip painting;
    [SerializeField]
    private AudioClip ringingPhone;
    [SerializeField]
    private AudioClip searchingDrawer;
    [SerializeField]
    private AudioClip gruntingCall;
    [SerializeField]
    private AudioClip vynilSpeed;
    [SerializeField]
    private AudioClip fallingMan;
    [SerializeField]
    private AudioClip storm;
    [SerializeField]
    private AudioClip seaStorm;

    public void Start()
    {
        PlaySound();
    }

    public void PlaySound()
    {
        audioSource.Play();
    }

    public void PlaySoundOnce()
    {
        audioSource.Play();
    }
}
