using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrasController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float diffAngle = 100f;
    public AudioSource musique;
    public AudioSource bruit;
    public float time;
    public VolumeController boutonVolume;

    private float angleVinyleMin = 272f;
    private float angleVinyleMax = 338f;
    private bool selected = false;

    void Start()
    {
        musique.volume = 0f;
        bruit.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 offset = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + diffAngle);
            Debug.Log(transform.eulerAngles.z);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        musique.volume = 0f;
        bruit.Pause();
        selected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selected = false;

        float zAngle = transform.eulerAngles.z;

        if (angleVinyleMin < zAngle && zAngle < angleVinyleMax)
        {
            float setTime;
            if (angleVinyleMax - 5f < zAngle && zAngle < angleVinyleMax)
            {
                setTime = 0f; // pour pouvoir facilement lancer le début de la chanson en placant la tête sur le bord du vinyle.
            }
            else
            {
                setTime = ((angleVinyleMax - zAngle)) * musique.clip.length / (angleVinyleMax - angleVinyleMin);
            }
            musique.time = setTime;
            musique.volume = boutonVolume.tabVolumes[boutonVolume.position];
            bruit.Play();
        }
    }
}
