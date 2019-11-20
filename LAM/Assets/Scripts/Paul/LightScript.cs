using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    public float minIntensity = 0f;
    public float maxIntensity = 0f;

    private Light currentlight;

    float random;

    void Start()
    {
        currentlight = GetComponent<Light>();
        random = Random.Range(0.0f, 65535.0f);
    }

    void Update()
    {
        float noise = Mathf.PerlinNoise(random, Time.time);
        currentlight.intensity = Mathf.Lerp(minIntensity, maxIntensity, noise);
        Debug.Log(currentlight.intensity);
    }
}
