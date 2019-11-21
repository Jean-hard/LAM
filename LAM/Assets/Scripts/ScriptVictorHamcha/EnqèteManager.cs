using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnqèteManager : MonoBehaviour
{
    public List<GameObject> imagetodrag = new List<GameObject>();//les images qu'ils faut drag 
    public List<GameObject> imagetodragrandomized = new List<GameObject>();//les images qu'il faut drag mélangé 
    public float x;//positon en X des pièce 
    public float y;//position en Y de la pièce 
    private int nbrdecare = 0;// nbr de pièce par collone 
    // Start is called before the first frame update 
    void Start()
    {
        imagetodragrandomized = imagetodrag.OrderBy(g => Random.value).ToList();//nouvelle liste comprenant les pièce mais dans un ordre aléatoire 
        for (int i = 0; i <= 8; i++)//pour toutes les pièces 
        {

            imagetodragrandomized[i].transform.position = new Vector2(x, y);//la positon de la pièce est eagal a X et Y 
            nbrdecare += 1;//retiens qu'il y a une pièce de plus sur la colomne 
            x += 2.8f;//la prochaine pièce est à 2.8f de la dernière place posée 
            if (nbrdecare == 5)//si y a deja 4 pièce sur la colomne 
            {
                y -= 2.4f;// la pochaine pièce se place à 2.4f en dessous  
                x = -6.41f;// sur la première ligne 
            }
           
        }
    }

    // Update is called once per frame 
    void Update()
    {

    }
}
