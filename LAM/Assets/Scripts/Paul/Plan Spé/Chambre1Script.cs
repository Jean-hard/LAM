using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chambre1Script : PlaneScript
{
    public override void OnActive()
    {
        base.OnActive();
        Room1Manager.Instance.ShowRoom1Dia();
        WeatherManager.Instance.inRoom1View1 = true;
        StartCoroutine(WeatherManager.Instance.SetWeatherRoom1View1());
        StartCoroutine(WeatherManager.Instance.SetThunderRoom1View1());
    }

    public override void OnDesactive()
    {
        base.OnDesactive();
        WeatherManager.Instance.inRoom1View1 = false;
    }
}
