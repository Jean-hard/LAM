using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleIMG : MonoBehaviour
{
    public List<GameObject> cut = new List<GameObject>();
    public List<GameObject> img = new List<GameObject>();
    public List<GameObject> bloc = new List<GameObject>();
    public List<SpriteRenderer> trueimg = new List<SpriteRenderer>();
    private int slice = 0;
    float x = -11.71f;
    float y = 3.972f;
    // Start is called before the first frame update
    void Start()
    {
        
        Slice();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Slice ()
    {
        for (int i=0; i<16; i++)
        {
           
            
            if (slice<4)
            {
                x += 4.68f;
                slice++;

            }
            else
            {
                y-= 2.602f;
                slice = 1;
                x = -7.01f;
            }
            
            cut[i].transform.parent = img[i].transform;
            cut[i].transform.position = new Vector2(x, y);
            bloc[i].transform.position = new Vector2(x, y);
            img[i].transform.parent = bloc[i].transform;
            bloc[i].transform.position = new Vector2(7.009999f,3.972f);
            bloc[i].transform.localScale = new Vector2(0.7165048f, 0.7165048f);
        }

    }
}
