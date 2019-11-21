using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnqèteManager : MonoBehaviour
{
    public List<GameObject> imagetodrag = new List<GameObject>();
    private List<GameObject> imagetodragrandomized = new List<GameObject>();
    public float x;
    public float y;
    private int nbrdecare = 0;
    // Start is called before the first frame update
    void Start()
    {
       imagetodragrandomized  = imagetodrag.OrderBy(g => Random.value).ToList();//nouvelle liste comprenant les pièce mais dans un ordre aléatoire
        for (int i=0; i<=8;i++)
        {
            
            imagetodragrandomized[i].transform.position = new Vector2(x, y);
            nbrdecare += 1;
            x += 2.8f;
            if (nbrdecare == 5)
            {
                y -= 2.4f;
                x = -6.41f;
            }

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
