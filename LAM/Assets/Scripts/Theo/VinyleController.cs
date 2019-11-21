using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VinyleController : MonoBehaviour
{
    public AudioSource bruit;
    public float bonneValeur = 0.7f;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AjusteBruitBlanc(Slider slider)
    {
        bruit.volume = Mathf.Abs(bonneValeur - slider.value);
    }
}
