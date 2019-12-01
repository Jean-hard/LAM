using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnqueteManager : MonoBehaviour
{
    public List<GameObject> imagetodrag = new List<GameObject>();//les images qu'il faut drag 
    public List<GameObject> imagetodragrandomized = new List<GameObject>();//les images qu'il faut drag mélangées 
    public List<GameObject> goodpiece = new List<GameObject>();//est-ce que la pièce fait partie des bonnes pièce 
    public List<Transform> slots = new List<Transform>();//slots ou drop les images 
    public List<bool> piececorrectpos = new List<bool>();//vérifie si la piece est a la bonne position 
    public float x;//positon en X des pièces
    public float y;//position en Y des pièces
    public float z;     // Position en Z des pièces
    private int nbrdecare = 0;// nbr de pièces par colonne 
    private bool enigmeFinished;
    //Dialogue
    [SerializeField]
    private Dialogue myDialogue;
    private bool dialogueDisplayed;

    // Start is called before the first frame update 
    void Start()
    {
        imagetodragrandomized = imagetodrag.OrderBy(g => Random.value).ToList();        //nouvelle liste comprenant les pièces mais dans un ordre aléatoire 
        for (int i = 0; i <= 8; i++)        //pour toutes les pièces 
        {
            imagetodragrandomized[i].transform.position = new Vector3(x, y, z);        //la position de la pièce est egal a X et Y 
            nbrdecare += 1;     //retient qu'il y a une pièce de plus sur la colomne 
            x += 2.8f;      //la prochaine pièce est à 2.8f de la dernière place posée 
            if (nbrdecare == 5)     //si y a deja 4 pièce sur la colomne 
            {
                y -= 2.4f;      // la pochaine pièce se place à 2.4f en dessous  
                x = -6.41f;     // sur la première ligne 
            }
        }
    }

    // Update is called once per frame 
    void Update()
    {

        if (piececorrectpos[0] && piececorrectpos[1] && piececorrectpos[2] && piececorrectpos[3])//si toutes les pièces sont au bonnes endroits 
        {
            enigmeFinished = true;
            Debug.Log("youwin");

        }

        if (enigmeFinished && !dialogueDisplayed)
        {
            GameManager.Instance.InitDialogue(myDialogue);
            dialogueDisplayed = true;
        }
    }
}
