using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Script présent sur chaque pièce du puzzle, permettant de la faire bouger et de la placer dans les slots
 * vérifie aussi si la pièce est dans le bon slot.
 */

public class MovePiecesPuzzle : MonoBehaviour
{
    private bool pieceSelected; // pièce sélectionnée ?
    private bool pieceInSlot;  // pièce déposée dans un slot ?

    private SpriteRenderer pieceSprite;
    private float distPieceSlot;    // distance entre la pièce sélectionnée et un slot
    private Vector2 initialPosition;    // position initiale de la pièce

    // Start is called before the first frame update
    void Start()
    {
        pieceSprite = this.gameObject.GetComponent<SpriteRenderer>();
        StartCoroutine(WaitForInitPosition());
    }

    // Update is called once per frame
    void Update()
    {
        if (pieceSelected && !PuzzleMgr.Instance.puzzleGameDone)
        {
            MovePiece();
        }
    }

    private void MovePiece()
    {
        Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);    // on set les coordonnées de la souris dans un vecteur
        transform.position = new Vector2(cursorPos.x, cursorPos.y);     // on donne à la piece les coordonnées de la souris
        pieceSprite.sortingOrder = 3;   // on place la piece sélectionnée devant les autres
        pieceInSlot = false;

        if (Input.GetKeyUp(KeyCode.Mouse0)) // si on relâche l'image 
        {
            for (int i = 0; i < PuzzleMgr.Instance.piecesSlots.Count; i++)    // vérifie pour chacun des 12 slots 
            {
                if (!pieceInSlot)
                {
                    distPieceSlot = Vector2.Distance(transform.position, PuzzleMgr.Instance.piecesSlots[i].position);      // la distance entre l'image et les slots 

                    if (distPieceSlot <= 1)  // si la pièce est près de l'un des slots 
                    {
                        transform.position = PuzzleMgr.Instance.piecesSlots[i].position;  // l'image prend la position du slot en question
                        pieceSprite.sortingOrder = 1;   // on remet l'image au bon ording layer
                        pieceInSlot = true;
                        CheckPieceInSlot(PuzzleMgr.Instance.piecesSlots[i].gameObject, i);  // on vérifie si l'image correspond au bon slot en question
                    }
                }
            }
            
            if (!pieceInSlot)       // si l'image est déposée trop loin d'un slot, on la laisse où elle est
            {
                pieceSprite.sortingOrder = 2;   // on remet l'image au bon ording layer
            }

            pieceSelected = false;
        }
    }

    public void CheckPieceInSlot(GameObject slot, int slotIndex)
    {
        if (slot.CompareTag(this.gameObject.tag))    // si le tag de la pièce correspond au tag du slot
        {
            PuzzleMgr.Instance.CheckSlotsDone(slotIndex);    // on ajoute ce slot à la liste des slots validés dans le PuzzleMgr
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
            pieceSelected = true;   // l'image est sélectionnée 
        }
    }
}
