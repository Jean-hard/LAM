using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BrasController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public float diffAngle = 100f;
    public AudioSource musique;
    public AudioSource bruit;
    public VolumeController boutonVolume;

    private float angleVinyleMin = 272f;
    private float angleVinyleMax = 338f;
    private bool selected = false;

    void Start()
    {
        musique.volume = 0f; // le bras n'est pas sur le vinyle au début donc on ne joue pas la musique 
        bruit.Pause();
    }

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
        bruit.Pause();
        selected = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        selected = false;

        float zAngle = transform.eulerAngles.z; // l'angle auquel le bras est lâché

        if (angleVinyleMin < zAngle && zAngle < angleVinyleMax) // si le bras est sur le vinyle
        {
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
            musique.volume = boutonVolume.tabVolumes[boutonVolume.position]; // on remet le volume car le bras est désormais sur le vinyle
            bruit.Play();
        }
    }
}
