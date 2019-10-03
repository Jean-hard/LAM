using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneScript : MonoBehaviour
{
    [SerializeField]
    private Vector2 initPlayerPos;

    [SerializeField]
    private GameObject[] interElementTab;
    private GameObject actualPlane;

    // Start is called before the first frame update
    void Start()
    {
        actualPlane = GetComponent<GameObject>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnDesactive()
    {
        this.gameObject.SetActive(false);
        for (int i = 0; i < interElementTab.Length; i++)
            interElementTab[i].SetActive(false);
    }

    public void OnActive()
    {
        this.gameObject.SetActive(true);
        for (int i = 0; i < interElementTab.Length; i++)
            interElementTab[i].SetActive(true);
    }

    public Vector3 GetInitPlayerPos()
    {
        return initPlayerPos;
    }
}
