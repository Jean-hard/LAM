using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Vector2 playerBasePose = new Vector2(0, 2);//à considérer comme une constante

    [System.NonSerialized]
    public Vector2 targetPosition;

    //private float scaleFactor;
    private Vector2 positionOnRightDoor;
    private Vector2 positionOnLeftDoor;

    public float moveSpeed = 3f;
    
    void Start()
    {
        targetPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //avance le perso en fonction de l'endroit du click
        /*
        if (Input.GetMouseButton(0))
        {
            mousePosition = Input.mousePosition;
            mousePosition = Camera.main.ScreenToWorldPoint(mousePosition);
        }*/
        if (targetPosition != playerBasePose)
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        else
            transform.position = playerBasePose;
    }
}
