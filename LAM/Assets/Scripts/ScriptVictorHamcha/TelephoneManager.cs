using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TelephoneManager : MonoBehaviour
{
    public List<int> numtel = new List<int>();
    public bool end;
    public bool goodnumber;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (numtel.Count == 6 && numtel[0] == 1 && numtel[1] == 1 && numtel[2] == 1 && numtel[3] == 1 && numtel[4] == 1 && numtel[5] == 1)
        {
            goodnumber = true;
            
        }
        if (end&&goodnumber)
        {
            Debug.Log("youwin");
        }
    }
}
