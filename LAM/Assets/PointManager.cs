using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointManager : MonoBehaviour
{
    public GameObject PlayerPoint;
   // private int sb=1;
    public LayerMask touchable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Mouse0) )
        {
            PlayerPoint.transform.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Aienemy.speed = 300f;
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
           
                Aienemy.speed = 0f;
            
            
               
            

            

        }
    }
}
