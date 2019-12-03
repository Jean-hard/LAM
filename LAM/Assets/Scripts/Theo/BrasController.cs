using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrasController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float diffAngle = 100f;
    public AudioSource musique;
    public AudioSource bruit;
    public VinyleManager manager;
    public float angleVinyleMin = 272f;
    public float angleVinyleMax = 348f;

    private bool selected = false;

    // Update is called once per frame
    void Update()
    {
        if (selected) // si le bras est cliqué (resté appuyé) il suit la souris en tournant
        {
            Vector2 mouse = Input.mousePosition;
            Vector2 offset = new Vector2(mouse.x - transform.position.x, mouse.y - transform.position.y);
            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Euler(0, 0, angle + diffAngle);
            // Debug.Log(transform.eulerAngles.z);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {// la musique s'arrête quand on soulève le bras
        musique.volume = 0f;
        selected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selected = false;

        float zAngle = transform.eulerAngles.z; // l'angle auquel le bras est lâché

        if (angleVinyleMin < zAngle && zAngle < angleVinyleMax) // si le bras est sur le vinyle
        {
            Debug.Log("placé");
            float setTime; // le timer auquel la musique se lance dépend de la position du bras
            if (angleVinyleMax - 5f < zAngle && zAngle < angleVinyleMax)
            {
                setTime = 0f; // pour pouvoir facilement lancer le début de la chanson en placant la tête sur le bord du vinyle.
            }
            else
            {
                // le timer est proportionnel à la surface parcourue par le bras avant d'avoir laché ; 
                // plus le bras est loin du bord du vinyle plus la chanson se lance tard
                setTime = ((angleVinyleMax - zAngle)) * musique.clip.length / (angleVinyleMax - angleVinyleMin);
            }
            musique.time = setTime;
            musique.volume = manager.tabVolumeValeurs[manager.volumePosition]; // on remet le volume car le bras est désormais sur le vinyle
        }
    }
}
