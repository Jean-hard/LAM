using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class TableauMgr : MonoBehaviour
{
    public List<GameObject> imagesList = new List<GameObject>(); // liste d'images
    private List<GameObject> imagesShuffledList = new List<GameObject>(); // liste mélangée des images
    public List<Transform> imagesSlots = new List<Transform>(); // liste des slots
    public List<bool> imageInGoodSlot = new List<bool>();   // liste des booleens donnant les slots ayant la bonne image

    public float xFirstImagePos;  // position en x de la première pièce
    public float yFirstImagePos;  // position en y de la première pièce
    private float xPos;
    private float yPos;
    public float xOffset;   // distance en x des pièces
    public float yOffset;   // séparation en y des pièces

    private int nbImageLigne;   // nombre d'images sur une ligne

    public static bool tableauGameDone;

    // Dialogue
    [SerializeField]
    private Dialogue tableauWinDialogue;
    private bool finalDialogueDisplayed;

    // SINGLETON du TableauMgr pour récup ses valeurs partout
    private static TableauMgr _instance;
    public static TableauMgr Instance { get { return _instance; } }

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
        imagesShuffledList = imagesList.OrderBy(g => Random.value).ToList();    // on mélange la liste d'images
        InitImagesPos();    // on place les images en rang
        for (int i = 0; i <= 3; i++)
        {
            imageInGoodSlot.Add(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (tableauGameDone && !finalDialogueDisplayed)
        {
            GameManager.Instance.InitDialogue(tableauWinDialogue);
            finalDialogueDisplayed = true;
        }
    }

    public void InitImagesPos()
    {
        xPos = xFirstImagePos;
        yPos = yFirstImagePos;

        for (int i = 0; i < imagesList.Count; i++)
        {
            imagesShuffledList[i].transform.position = new Vector2(xPos, yPos);
            xPos += xOffset;
            nbImageLigne++;
            if (nbImageLigne == 5)
            {
                xPos = xFirstImagePos;  // la prochaine image sera sur la première colonne
                yPos -= yOffset;    // la prochaine image sera en dessous de la première ligne d'image
            }
        }
    }

    public void CheckSlotsDone(int slotIndex)
    {
        imageInGoodSlot[slotIndex] = true;

        for (int j = 0; j <= 3; j++)
        {
            if(!imageInGoodSlot[j])
                return;
        }

        tableauGameDone = true;
    }
}
