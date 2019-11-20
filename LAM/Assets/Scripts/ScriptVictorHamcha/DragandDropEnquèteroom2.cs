using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDropEnquèteroom2 : MonoBehaviour
{
    private bool selected;
    public List<Transform> slots = new List<Transform>();
    public int numérodepiéce;
    [SerializeField]
    private float distance;
    private SpriteRenderer _sprite;
    private Vector2 initialposition;
    private bool droped;
    public List<GameObject> goodpiece = new List<GameObject>();
    public List<bool> piececorrectpos = new List<bool>();
    
    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        StartCoroutine(waitforinitialisation());
    }

    // Update is called once per frame
    void Update()
    {
        if (selected)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);//la pèce prend la position de la souris
            _sprite.sortingOrder = 2;
            
        }

        for (int i = 0; i <= 3; i++)
        {

            
                float distgoodnumber = Vector2.Distance(goodpiece[i].transform.position, slots[i].position);
            if (distgoodnumber == 0)
            {

                piececorrectpos[i] = true;

            }
            else
            {
                piececorrectpos[i] = false;
            }

           
            

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            
            for (int i=0; i<=3;i++)
            {
                
                if (selected)
                {
                    distance = Vector2.Distance(transform.position, slots[i].position);
                    if (distance <= 1)
                    {
                        transform.position = slots[i].position;
                        droped = true;
                       
                    }
                   

                }
                
              
            }
            if (!droped && selected)
            {
                transform.position = initialposition;
            }
            _sprite.sortingOrder = 0;
            selected = false;
            droped = false;
        }

        if (piececorrectpos[0]&& piececorrectpos[1] && piececorrectpos[2] && piececorrectpos[3])
        {
            Debug.Log("youwin");
        }
        
    }

    IEnumerator waitforinitialisation ()
    {
        yield return new WaitForSeconds(0.1f);
        initialposition = transform.position;
        
    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            selected = true;
        }
    }
}
