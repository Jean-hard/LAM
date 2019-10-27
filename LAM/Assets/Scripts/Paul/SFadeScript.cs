using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/**
 * Version du fade avec Sprite renderer
 */
public class SFadeScript : MonoBehaviour
{
    public float FadeRate;
    private SpriteRenderer sprite;
    private float targetAlpha;

    // Use this for initialization
    void Start()
    {
        this.sprite = this.GetComponent<SpriteRenderer>();
        if (this.sprite == null)
        {
            Debug.LogError("Error: No image on " + this.name);
        }
        this.targetAlpha = this.sprite.color.a;
    }

    // Update is called once per frame
    void Update()
    {
        Color curColor = this.sprite.color;
        float alphaDiff = Mathf.Abs(curColor.a - this.targetAlpha);
        if (alphaDiff > 0.0001f)
        {
            curColor.a = Mathf.Lerp(curColor.a, targetAlpha, this.FadeRate * Time.deltaTime);
            this.sprite.color = curColor;
        }
    }

    public void FadeOut()
    {
        this.targetAlpha = 0.0f;
    }

    public void FadeIn()
    {
        this.targetAlpha = 1.0f;
    }
}
