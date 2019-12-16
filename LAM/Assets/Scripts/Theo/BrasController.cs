using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrasController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float diffAngle = 120f;
    public AudioSource musique;
    public AudioSource bruit;
    public AudioSource crackle;
    public VinyleManager manager;
    public float angleVinyleMin = 290f;
    public float angleVinyleMax = 6f;

    private bool selected = false;
    public static bool isOnDisc = false;

    // Update is called once per frame
    void Update()
    {
        if (selected) // si le bras est cliqué (resté appuyé) il suit la souris en tournant
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 offset = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + diffAngle);
            //Debug.Log(transform.eulerAngles.z);
        }

        if (transform.eulerAngles.z < 294f && transform.eulerAngles.z > 146f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 295f);
        }
        else if (transform.eulerAngles.z > 30f && transform.eulerAngles.z < 145f)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, transform.eulerAngles.y, 29f);
        }

    }

    public void OnPointerDown(PointerEventData eventData)
    {// la musique s'arrête quand on soulève le bras
        musique.Pause();
        bruit.Pause();
        crackle.Pause();
        isOnDisc = false;
        selected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selected = false;

        float zAngle = transform.eulerAngles.z; // l'angle auquel le bras est lâché

        if ((angleVinyleMin < zAngle && zAngle < 360f) || (0f < zAngle && zAngle < angleVinyleMax)) // si le bras est sur le vinyle
        {
            isOnDisc = true;
            Debug.Log("placé");
            musique.Play();
            bruit.Play();
            crackle.Play();
            musique.time = 0f;
            musique.volume = manager.tabVolumeValeurs[manager.volumePosition]; // on remet le volume car le bras est désormais sur le vinyle
            
        }
    }
}
