using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class FadeScript : MonoBehaviour
{
    public float FadeRate;
    private Image image;
    private Text texte;
    private float targetAlpha;

    // Use this for initialization
    void Start()
    {
        if (this.GetComponent<Image>() != null)
        {
            this.image = this.GetComponent<Image>();
            if (this.image == null)
            {
                Debug.LogError("Error: No image on " + this.name);
            }
            this.targetAlpha = this.image.color.a;
        }
        else if (this.GetComponent<Text>() != null)
        {
            this.texte = this.GetComponent<Text>();
            this.targetAlpha = this.texte.color.a;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (this.GetComponent<Image>() != null)
        {
            Color curColor = this.image.color;
            float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
            if (alphaDiff > 0.0001f)
            {
                curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
                this.image.color = curColor;
            }
        }
        else if (this.GetComponent<Text>() != null)
        {
            Color curColor = this.texte.color;
            float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
            if (alphaDiff > 0.0001f)
            {
                curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
                this.texte.color = curColor;
            }
        }
    }

    public void FadeOut()
    {
        if (this.GetComponent<Image>() != null)
            image.raycastTarget = false;
        else if (this.GetComponent<Text>() != null)
            texte.raycastTarget = false;
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        if (this.GetComponent<Image>() != null)
            image.raycastTarget = true;
        else if (this.GetComponent<Text>() != null)
            texte.raycastTarget = true;
        this.targetAlpha = 1.0f;
    }
}
