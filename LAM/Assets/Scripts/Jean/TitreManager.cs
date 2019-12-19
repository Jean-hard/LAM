using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitreManager : MonoBehaviour
{
    public FadeScript fadeScreen;
    public FadeScript casqueScreen;

    public void LoadGame()
    {
        fadeScreen.FadeIn();
        StartCoroutine(WaitForLoadGame());
    }

    public IEnumerator WaitForLoadGame()
    {
        yield return new WaitForSeconds(2f);
        casqueScreen.FadeIn();
        yield return new WaitForSeconds(4f);
        SceneManager.LoadScene(1);
    }
}
