using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorNameDisplayer : MonoBehaviour
{
    private GameObject thisButton;
    public GameObject colorName;

    // Start is called before the first frame update
    void Start()
    {
        thisButton = this.gameObject;
    }

    public void DisplayButtonName()
    {
        colorName.transform.position = thisButton.transform.position;
        colorName.SetActive(true);
    }
}
