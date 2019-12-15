using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script présent sur chaque image, permettant de la faire bouger et de la placer dans les slots
 * vérifie aussi si l'image est dans le bon slot.
 */

public class MoveImagesTableau : MonoBehaviour
{
    private bool imageSelected; // image sélectionnée ?
    private bool imageInSlot;  // image déposée dans un slot ?

    private SpriteRenderer imageSprite;
    private float distImageSlot;    // distance entre l'image sélectionnée et un slot
    private Vector2 initialPosition;    // position initiale de l'image

    // Start is called before the first frame update
    void Start()
    {
        imageSprite = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForInitPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (imageSelected && !TableauMgr.tableauGameDone)
        {
            MoveImage();
        }
    }

    private void MoveImage()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // on set les coordonnées de la souris dans un vecteur
        transform.position = new Vector3(cursorPos.x, cursorPos.y, -0.02f);     // on donne à l'image les coordonnées de la souris
        imageSprite.sortingOrder = 2;   // on place l'image sélectionnée devant les autres
        imageInSlot = false;

        if (Input.GetKeyUp(KeyCode.Mouse0)) // si on relâche l'image 
        {
            for (int i = 0; i < TableauMgr.Instance.imagesSlots.Count; i++)    // vérifie pour chacun des 3 slots 
            {
                if (!imageInSlot)
                {
                    distImageSlot = Vector2.Distance(transform.position, TableauMgr.Instance.imagesSlots[i].position);      // la distance entre l'image et les slots 

                    if (distImageSlot <= 1)  // si la pièce est près de l'un des slots 
                    {
                        transform.position = TableauMgr.Instance.imagesSlots[i].position;  // l'image prend la position du slot en question
                        transform.position += new Vector3(0,0,-0.01f);  // on place l'image légèrement au devant pour pas qu'elle se place derrière le font
                        //imageSprite.sortingOrder = 1;   // on remet l'image au bon ording layer
                        imageInSlot = true;
                        CheckImageInSlot(TableauMgr.Instance.imagesSlots[i].gameObject, i);  // on vérifie si l'image correspond au bon slot en question
                    }
                }
            }

            if (!imageInSlot)       // si l'image est déposée trop loin d'un slot
            {
                transform.position = initialPosition;   // l'image reprend sa position initiale
                //imageSprite.sortingOrder = 1;   // on remet l'image au bon ording layer
            }

            imageSelected = false;
        }
    }

    public void CheckImageInSlot(GameObject slot, int slotIndex)
    {
        if(slot.CompareTag(this.gameObject.tag))    // si le tag de l'image correspond au tag du slot
        {
            TableauMgr.Instance.CheckSlotsDone(slotIndex);    // on ajoute ce slot à la liste des slots validés dans le TableauMgr
            this.gameObject.GetComponent<BoxCollider2D>().enabled = false;  // on désactive le collider de l'image pour qu'on ne puisse plus la bouger
        }
    }

    IEnumerator WaitForInitPosition() 
    {
        yield return new WaitForSeconds(0.1f);  // attend que l'image soit à sa place 
        initialPosition = transform.position;   // on sauvegarde la position initiale de l'image
    }

    void OnMouseOver()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))   // si on clique sur l'image
        {
            imageSelected = true;   // l'image est sélectionnée 
        }
    }
}
