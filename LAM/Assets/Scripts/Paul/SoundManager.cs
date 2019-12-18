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
    private AudioSource audioSourceBO;
    [SerializeField]
    private AudioSource audioSourceCineEffects;
    [SerializeField]
    private AudioSource audioSourceCineLoop1;
    [SerializeField]
    private AudioSource audioSourceCineLoop2;

    [SerializeField]
    private AudioClip doorClosing;
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
    [SerializeField]
    private AudioClip BO;
    [SerializeField]
    private AudioClip numberGood;
    [SerializeField]
    private AudioClip oceanLoop;
    [SerializeField]
    private AudioClip stormLoop;
    [SerializeField]
    private AudioClip thunderP1;
    [SerializeField]
    private AudioClip thunderP2;
    [SerializeField]
    private AudioClip boatCrash;

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
        PlayBO();
        //pour jouer un son de quelquun marchant dans le couloir à l'étage pendant la discusion avec l'aubergiste
        PlayPassifAtticRun(5);
    }

    public void PlayOpeningDoor()
    {
        audioSourceEffect.clip = doorClosing;
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

    public void PlayPassifAtticRun(int secondWait)
    {
        audioSourcePassif.clip = atticRun;
        StartCoroutine(PlayPassif(secondWait));
    }

    public void PlayPassifScratch(int secondWait)
    {
        audioSourcePassif.clip = scratch;
        StartCoroutine(PlayPassifScratchReduced(secondWait));
    }

    public void PlayPassifWind(int secondWait)
    {
        audioSourcePassif.clip = wind_Sound;
        StartCoroutine(PlayPassif(secondWait));
    }

    public void PlayPassifNightOwl(int secondWait)
    {
        audioSourcePassif.clip = nightOwl;
        StartCoroutine(PlayPassif(secondWait));
    }

    private IEnumerator PlayPassif(int secondWait)
    {
        yield return new WaitForSeconds(secondWait);
        audioSourcePassif.Play();
    }

    //Scratch est trop long sinon
    private IEnumerator PlayPassifScratchReduced(int secondWait)
    {
        yield return new WaitForSeconds(secondWait);
        audioSourcePassif.Play();
        yield return new WaitForSeconds(5);
        audioSourcePassif.Stop();
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

    public void StopRingRing()
    {
        audioSourceEffect.Stop();
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

    public void StopGruntingCall()
    {
        audioSourceEffect.Stop();
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

    public void PlayBO()
    {
        audioSourceBO.clip = BO;
        audioSourceBO.Play();
    }

    public void PlayNumberGood()
    {
        audioSourceEffect.clip = numberGood;
        audioSourceEffect.Play();
    }

    public void PlayOceanLoop()
    {
        audioSourceCineLoop1.clip = oceanLoop;
        audioSourceCineLoop1.Play();
    }
    public void PlayStormLoop()
    {
        audioSourceCineLoop2.clip = stormLoop;
        audioSourceCineLoop2.Play();
    }
    public void PlayThunderP1()
    {
        audioSourceCineEffects.clip = thunderP1;
        audioSourceCineEffects.Play();
    }
    public void PlayThunderP2()
    {
        audioSourceCineEffects.clip = thunderP2;
        audioSourceCineEffects.Play();
    }
    public void PlayBoatCrash()
    {
        audioSourceCineEffects.clip = boatCrash;
        audioSourceCineEffects.Play();
    }
}
