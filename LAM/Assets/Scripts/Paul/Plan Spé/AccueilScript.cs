using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccueilScript : PlaneScript
{
    public override void OnActive()
    {
        base.OnActive();
        AccueilCouloirManager.Instance.ShowAccueilDia();
        Debug.Log("in onactive");

        // on check sur quelle version de l'accueil on est pour lancer la bonne anim de meteo
        if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "AccueilNormal")
        {
            Debug.Log("Accueil normal is active");
            WeatherManager.Instance.inAccueilNormal = true;
            StartCoroutine(WeatherManager.Instance.SetRainAccueilNormal());
            StartCoroutine(WeatherManager.Instance.SetThunderAccueilNormal());
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "AccueiltorduV1")
        {
            Debug.Log("Accueil tordu 1 is active");
            WeatherManager.Instance.inAccueilTordu1 = true;
            StartCoroutine(WeatherManager.Instance.SetWeatherAccueilTordu1());
            StartCoroutine(WeatherManager.Instance.SetThunderTordu1());
        }
        else if (this.gameObject.GetComponent<SpriteRenderer>().sprite.name == "AccueiltorduV2")
        {
            Debug.Log("Accueil tordu 2 is active");
            WeatherManager.Instance.inAccueilTordu2 = true;
            StartCoroutine(WeatherManager.Instance.SetWeatherAccueilTordu2());
            StartCoroutine(WeatherManager.Instance.SetThunderTordu2());
        } else
        {
            Debug.Log("mauvais nom de sprite");
        }
    }

    public override void OnDesactive()
    {
        base.OnDesactive();
        WeatherManager.Instance.inAccueilNormal = false;
        WeatherManager.Instance.inAccueilTordu1 = false;
        WeatherManager.Instance.inAccueilTordu2 = false;
        WeatherManager.Instance.thunderAccueilNormal.SetActive(false);
        WeatherManager.Instance.thunderAccueilTordu1.SetActive(false);
        WeatherManager.Instance.thunderAccueilTordu2.SetActive(false);
        for (int i = 0; i < WeatherManager.Instance.rainAccueilNormalList.Count; i++)
        {
            WeatherManager.Instance.rainAccueilNormalList[i].gameObject.SetActive(false);
            WeatherManager.Instance.rainAccueilTordu1List[i].gameObject.SetActive(false);
            WeatherManager.Instance.rainAccueilTordu2List[i].gameObject.SetActive(false);
        }
    }
}
