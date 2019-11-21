﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VolumeController : MonoBehaviour, IPointerDownHandler
{
    public AudioSource audioSource;
    public Transform brasTransform;
    public float[] tabVolumes = { 0.1f, 0.3f, 0.5f };
    public int position = 1;

    private float[] tabPositions = { 45, 0, 315 };

    private float angleVinyleMin = 272f;
    private float angleVinyleMax = 338f;
    private float zAngleBras;

    // Start is called before the first frame update
    void Start()
    {
        audioSource.volume = tabVolumes[position];
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        // boutonVolumeSelected = true;

        if (position < 2)
        {
            position++;
        }
        else
        {
            position = 0;
        }
        transform.rotation = Quaternion.Euler(0, 0, tabPositions[position]);

        zAngleBras = brasTransform.eulerAngles.z;

        if (angleVinyleMin < zAngleBras && zAngleBras < angleVinyleMax)
        {
            audioSource.volume = tabVolumes[position];
        }
    }

    // public float boutonVolumeDiffAngle = -90f;
    // public int tailleCransBoutonVolume = 24;

    // private bool boutonVolumeSelected = false;
    // private bool boutonVolumeWorking = false;
    // private bool avertiVolume = false;

    // ______

    // public void OnPointerUp(PointerEventData eventData)
    // {
    //     boutonVolumeSelected = false;
    // }

    // Update is called once per frame
    // void Update()
    // {
    // if (boutonVolumeSelected)
    // {
    //     Vector2 mouse = Input.mousePosition;
    //     Vector2 offset = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
    //     float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
    //     int angleDef = tailleCransBoutonVolume * (int)Mathf.Round((angle + boutonVolumeDiffAngle) / tailleCransBoutonVolume);
    //     transform.rotation = Quaternion.Euler(0, 0, angleDef);
    // }

    // if (!boutonVolumeWorking)
    // {
    //     if (!avertiVolume && transform.eulerAngles == new Vector3(0, 0, 4 * tailleCransBoutonVolume))
    //     {
    //         Debug.Log("si je continue de le tourner vers la gauche il va se dévisser...");
    //         avertiVolume = true;
    //     }

    //     if (transform.eulerAngles == new Vector3(0, 0, 360 - 3 * tailleCransBoutonVolume))
    //     {
    //         Debug.Log("ça marche!");
    //         boutonVolumeWorking = true;
    //         //lancer le volume
    //     }
    // }
    // }
}
