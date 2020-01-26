using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class TitreManager : MonoBehaviour
{
    public FadeScript fadeScreen;
    public FadeScript casqueScreen;
    public VideoPlayer introVideo;

    private void Start()
    {
        if (introVideo)
            introVideo.loopPointReached += EndofIntro;
    }

    public void LoadGame()
    {
        //fadeScreen.FadeIn();
        casqueScreen.FadeIn();
        StartCoroutine(WaitForLoadGame());
    }

    public IEnumerator WaitForLoadGame()
    {
        yield return new WaitForSeconds(2f);
        //fadeScreen.FadeOut();
    
        yield return new WaitForSeconds(2f);
        casqueScreen.FadeOut();

        if (introVideo)
            introVideo.Play();
        else
        {
            fadeScreen.FadeIn();
            Debug.Log("changement sans video");
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(1);
        }
    }

    public void EndofIntro(UnityEngine.Video.VideoPlayer vp)
    {
        Debug.Log("changement en fin de video");
        SceneManager.LoadScene(1);
    }
}
