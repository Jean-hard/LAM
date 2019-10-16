using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class horlogemanager : MonoBehaviour
{
    public GameObject minute;
    public Transform minpos;
    public Transform posm;
    private GameObject vv;

    public Transform heurepos; 
    public GameObject heure;
    public Transform posh;
    private GameObject hh;


    private float distance;
    private float distanceh;


    public List<GameObject> pointminute = new List<GameObject>();
    public List<GameObject> pointheure = new List<GameObject>();


    private bool m;
    private float mint;
    private float minh;

    private bool h;
    private float heuret;
    private float heureh;

    // Start is called before the first frame update
    void Start()
    {
        for (int i=0; i<60;i++)
        {
            
            minpos.rotation= Quaternion.Euler(0f, 0f, -i*6+180);
            Vector2 pipi = new Vector2(posm.position.x, posm.position.y);
            vv = Instantiate(minute, pipi, Quaternion.identity);
            pointminute.Add(vv);
        }

        for (int i = 0; i < 12; i++)
        {

            heurepos.rotation = Quaternion.Euler(0f, 0f, -i * 30 + 180);
            Vector2 hihi = new Vector2(posh.position.x, posh.position.y);
             hh = Instantiate(heure, hihi, Quaternion.identity);
            pointheure.Add(hh);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
        mint = System.DateTime.Now.Minute;
        heuret = System.DateTime.Now.Hour;
       
        for (int i = 0; i < 60; i++)
        {
            distance = Vector2.Distance(pointminute[i].transform.position, minute.transform.position);
            if (distance <= 0.1)
            {
                minh = i;
                if (minh==mint)
                {
                    m = true;
                }
                else
                {
                    m = false;
                }
                
            }
        }

        for (int i = 0; i < 12; i++)
        {
            distanceh = Vector2.Distance(pointheure[i].transform.position, heure.transform.position);
            if (distanceh <= 0.1)
            {
                heureh = i;
                if (heureh == heuret|| heureh+12==heuret)
                {
                    h = true;
                }

                else
                {
                    h = false;
                }

            }
        }

        if (h&&m)
        {
            Debug.Log("youwin");
        }


    }
}
