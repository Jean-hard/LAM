using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField]
    private AudioSource audioSourceSong;
    [SerializeField]
    private AudioSource audioSourceEffect;
    [SerializeField]
    private AudioSource audioSourcePassif;

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

    // SINGLETON ---------------------------------------------
    private static SoundManager _instance;

    public static SoundManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }//--------------------------------------------------------------------

    public void Start()
    {
        PlaySeaSound();
    }

    public void PlayOpeningDoor()
    {
        audioSourceEffect.clip = doorOpening;
        audioSourceEffect.Play();
    }

    public void PlayCatMeowSound()
    {
        audioSourceEffect.clip = catMeow;
        audioSourceEffect.Play();
    }

    public void PlayCatHissesSound()
    {
        audioSourceEffect.clip = catHisses;
        audioSourceEffect.Play();
    }

    public void PlaySeaSound()
    {
        audioSourceSong.clip = sea_Sound;
        audioSourceSong.Play();
    }

    public void PlayPassifAtticRun()
    {
        audioSourcePassif.clip = atticRun;
        audioSourcePassif.Play();
    }

    public void PlayPassifScratch()
    {
        audioSourcePassif.clip = scratch;
        audioSourcePassif.Play();
    }

    public void PlayPassifWind()
    {
        audioSourcePassif.clip = wind_Sound;
        audioSourcePassif.Play();
    }

    public void PlayPassifNightOwl()
    {
        audioSourcePassif.clip = nightOwl;
        audioSourcePassif.Play();
    }

    public void PlayOpenDrawer()
    {
        audioSourceEffect.clip = openDrawer;
        audioSourceEffect.Play();
    }

    public void PlayCloseDrawer()
    {
        audioSourceEffect.clip = closeDrawer;
        audioSourceEffect.Play();
    }

    public void PlayFlashBackOpening()
    {
        audioSourceEffect.clip = flashBackOpening;
        audioSourceEffect.Play();
    }

    public void PlayPaintingSound()
    {
        audioSourceEffect.clip = painting;
        audioSourceEffect.Play();
    }

    public void PlayRingRing()
    {
        audioSourceEffect.clip = ringingPhone;
        audioSourceEffect.Play();
    }

    public void PlaySearchingDrawer()
    {
        audioSourceEffect.clip = searchingDrawer;
        audioSourceEffect.Play();
    }

    public void PlayGruntingCall()
    {
        audioSourceEffect.clip = gruntingCall;
        audioSourceEffect.Play();
    }

    public void PlayVynilSpeed()
    {
        audioSourceEffect.clip = vynilSpeed;
        audioSourceEffect.Play();
    }

    public void PlayFallingMan()
    {
        audioSourceEffect.clip = fallingMan;
        audioSourceEffect.Play();
    }

    public void PlayStorm()
    {
        audioSourceEffect.clip = storm;
        audioSourceEffect.Play();
    }

    public void PlaySeaStorm()
    {
        audioSourceEffect.clip = seaStorm;
        audioSourceEffect.Play();
    }
}
