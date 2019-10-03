using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mask : MonoBehaviour
{
    public int id;
    public string maskName;
    public Vector2 maskPosition;    //position dans la scene, pas du button /!\
    private bool isFound;
    //public MaskModel mask;

    void Start()
    {

    }

    void Update()
    {
        if (this.isFound)
        {
            //placer l'icône du masque en bas à gauche

        }
    }

    public void SetIsFound(bool b)
    {
        this.isFound = b;
    }
}
