using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/**
 * Script gérant le puzzle
 * 
 * Pour modifier la position du puzzle entier, il faut changer la position du premier slot : xFirstSlotPos et yFirstSlotPos 
 * cela placera la grille en fonction de cette position
 */

public class PuzzleMgr : MonoBehaviour
{
    public GameObject puzzleGame;   // gameObject contenant tout le jeu, activé lors du click sur un bouton

    public List<GameObject> piecesList = new List<GameObject>();   // liste des pièces du puzzle
    private List<GameObject> piecesShuffledList = new List<GameObject>(); // liste mélangée des pièces du puzzle
    public List<Transform> piecesSlots = new List<Transform>(); // liste des slots
    public List<bool> piecesInGoodSlot = new List<bool>();   // liste des booleens donnant les slots ayant la bonne pièce

    public GameObject templateHidePieces;   // image qui cache la pile de pièces
    public GameObject grille; // image de la grille

    public float xPilePiecesPos;  // position en x des pièces
    public float yPilePiecesPos;  // position en y des pièces
    public float xFirstSlotPos; // position en x du premier slot
    public float yFirstSlotPos; // position en y du premier slot
    private float xSlotPos;
    private float ySlotPos;
    public float xOffset;   // distance en x entre les centres de 2 slots
    public float yOffset;   // distance en y entre les centres de 2 slots
    private int nbSlotLigne;    // nombre de slots sur une ligne

    [System.NonSerialized]
    public bool puzzleGameDone;

    public GameObject puzzlePlan;   // le plan du puzzle pour pouvoir changer son sprite une fois le puzzle terminé
    public Sprite fontPuzzleDone;   // font à mettre lorsque le puzzle est terminé

    [SerializeField]
    private float timeAfterPuzzleDone;

    // Dialogue
    [SerializeField]
    private Dialogue puzzleDoneDialogue;
    private bool finalDialogueDisplayed;

    // SINGLETON du TableauMgr pour récup ses valeurs partout
    private static PuzzleMgr _instance;
    public static PuzzleMgr Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        piecesShuffledList = piecesList.OrderBy(g => Random.value).ToList();    // on mélange la liste de pièces
        InitPiecesPos();    // on place les pièces à l'endroit voulu

        for (int i = 0; i < 12; i++)
        {
            piecesInGoodSlot.Add(false);    // on place que des False dans la liste de booleens dont on a setté la taille à 12 dans le for
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (puzzleGameDone && !finalDialogueDisplayed)
        {
            GameManager.Instance.InitDialogue(puzzleDoneDialogue);  // on affiche le dialogue d'énigme réussie
            finalDialogueDisplayed = true;
            Room1Manager.puzzleDone = true; // on dit au room1manager que l'énigme est validée
            puzzlePlan.GetComponent<SpriteRenderer>().sprite = fontPuzzleDone; // on applique le nouveau sprite avec le trou dans la tapisserie rempli
            
            StartCoroutine(TimeAfterPuzzleDone()); // coroutine à la fin de laquelle le jeu va se désactiver
        }
    }

    public void InitPiecesPos()
    {
        grille.transform.position = new Vector2(xFirstSlotPos + 4.29f, yFirstSlotPos - 2.14f); // on set la position de la grille par rapport à la position du premier slot

        templateHidePieces.transform.position = new Vector2(xPilePiecesPos, yPilePiecesPos);    // on place le template à la position des pièces
        templateHidePieces.GetComponent<SpriteRenderer>().sortingOrder = 2; // au premier plan pour cacher les pièces

        xSlotPos = xFirstSlotPos;
        ySlotPos = yFirstSlotPos;

        for (int i = 0; i < piecesList.Count; i++)
        {
            piecesShuffledList[i].transform.position = new Vector2(xPilePiecesPos, yPilePiecesPos);     // on place toutes les pièces à une position choisie

            // on initialise la position des slots
            piecesSlots[i].transform.position = new Vector2(xSlotPos, ySlotPos);
            xSlotPos += xOffset;
            nbSlotLigne++;  // on compte le nombre de slot qu'on a placé sur 1 ligne

            if (nbSlotLigne == 4)   // si on a 4 slot sur 1 ligne
            {
                nbSlotLigne = 0;
                xSlotPos = xFirstSlotPos;   // le prochain slot sera sur la première colonne
                ySlotPos -= yOffset;    // on créé la ligne suivante pour la suite des slots
            }
        }
    }

    // fonction appelée lorsqu'une pièce est placée dans un slot
    public void CheckSlotsDone(int slotIndex)
    {
        piecesInGoodSlot[slotIndex] = true;

        for (int j = 0; j < piecesInGoodSlot.Count; j++)   // on regarde si tous les éléments de la liste de booléens sont true
        {
            if (!piecesInGoodSlot[j])
                return;                 // on sort de la fonction si on rencontre un false dans la liste
        }

        puzzleGameDone = true;  // devient true si tous les booleens sont à true, et donc si toutes les pièces sont dans leurs slots respectifs
    }

    // fonction appelée lorsqu'on clicke sur le morceau manquant sur le mur pour lancer le puzzle
    public void StartPuzzleGame()
    {
        if(!puzzleGameDone)
        {
            puzzleGame.SetActive(true);
        }
    }

    // fonction appelée lorsqu'on clicke sur le bouton back
    public void DesactivePuzzleGame()
    {
        puzzleGame.SetActive(false);
    }

    // réussite du jeu --> attente avant de désafficher le jeu
    IEnumerator TimeAfterPuzzleDone()
    {
        yield return new WaitForSeconds(timeAfterPuzzleDone);
        DesactivePuzzleGame();
    }
}
