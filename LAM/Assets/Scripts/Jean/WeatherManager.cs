using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeatherManager : MonoBehaviour
{
    // spriteRenderer de l'accueil pour savoir si le plan est tordu 
    public SpriteRenderer accueilSpriteRenderer;

    // sprites de l'accueil normal pour la pluie et l'orage
    public List<GameObject> rainAccueilNormalList;
    public GameObject thunderAccueilNormal;

    // sprites de l'accueil tordu 1 pour la pluie et l'orage
    public List<GameObject> rainAccueilTordu1List;
    public GameObject thunderAccueilTordu1;

    // sprites de l'accueil tordu 2 pour la pluie et l'orage
    public List<GameObject> rainAccueilTordu2List;
    public GameObject thunderAccueilTordu2;

    // sprites de la chambre 1 view 1 pour la pluie et l'orage
    public List<GameObject> rainRoom1View1List;
    public GameObject thunderRoom1View1;

    // sprites de la chambre 1 view 2 pour l'orage
    public GameObject thunderRoom1View2;

    public float timeBetweenRains = 0.5f;

    public bool inAccueilNormal;
    public bool inAccueilTordu1;
    public bool inAccueilTordu2;
    public bool inRoom1View1;
    public bool inRoom1View2;

    // SINGLETON ---------------------------------------------
    private static WeatherManager _instance;

    public static WeatherManager Instance { get { return _instance; } }

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

    public IEnumerator SetRainAccueilNormal()
    {
        while(inAccueilNormal)
        {
            for (int i = 0; i < rainAccueilNormalList.Count; i++)
            {
                rainAccueilNormalList[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(timeBetweenRains);
                rainAccueilNormalList[i].gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator SetThunderAccueilNormal()
    {
        while (inAccueilNormal)
        {
            thunderAccueilNormal.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilNormal.SetActive(false);
            yield return new WaitForSeconds(3f);
            thunderAccueilNormal.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilNormal.SetActive(false);
            yield return new WaitForSeconds(.1f);
            thunderAccueilNormal.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilNormal.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
    }

    public IEnumerator SetWeatherAccueilTordu1()
    {
        while (inAccueilTordu1)
        {
            for (int i = 0; i < rainAccueilTordu1List.Count; i++)
            {
                rainAccueilTordu1List[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(timeBetweenRains);
                rainAccueilTordu1List[i].gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator SetThunderTordu1()
    {
        while (inAccueilTordu1)
        {
            thunderAccueilTordu1.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu1.SetActive(false);
            yield return new WaitForSeconds(3f);
            thunderAccueilTordu1.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu1.SetActive(false);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu1.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu1.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
    }

    public IEnumerator SetWeatherAccueilTordu2()
    {
        while (inAccueilTordu2)
        {
            for (int i = 0; i < rainAccueilTordu2List.Count; i++)
            {
                rainAccueilTordu2List[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(timeBetweenRains);
                rainAccueilTordu2List[i].gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator SetThunderTordu2()
    {
        while (inAccueilTordu2)
        {
            thunderAccueilTordu2.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu2.SetActive(false);
            yield return new WaitForSeconds(3f);
            thunderAccueilTordu2.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu2.SetActive(false);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu2.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderAccueilTordu2.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
    }

    public IEnumerator SetWeatherRoom1View1()
    {
        while (inRoom1View1)
        {
            for (int i = 0; i < rainRoom1View1List.Count; i++)
            {
                rainRoom1View1List[i].gameObject.SetActive(true);
                yield return new WaitForSeconds(timeBetweenRains);
                rainRoom1View1List[i].gameObject.SetActive(false);
            }
        }
    }

    public IEnumerator SetThunderRoom1View1()
    {
        while (inRoom1View1)
        {
            thunderRoom1View1.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderRoom1View1.SetActive(false);
            yield return new WaitForSeconds(3f);
            thunderRoom1View1.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderRoom1View1.SetActive(false);
            yield return new WaitForSeconds(.1f);
            thunderRoom1View1.SetActive(true);
            yield return new WaitForSeconds(.1f);
            thunderRoom1View1.SetActive(false);
            yield return new WaitForSeconds(5f);
        }
    }
}
