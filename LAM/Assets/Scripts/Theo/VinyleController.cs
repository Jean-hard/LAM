using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VinyleController : MonoBehaviour
{
    public AudioSource bruit;
    public float bonneValeur = 0.7f;

    public void AjusteBruitBlanc(Slider slider)
    {
        bruit.volume = Mathf.Abs(bonneValeur - slider.value); // plus le slider est proche de la bonne valeur, moins il y a de bruit blanc
    }
}
