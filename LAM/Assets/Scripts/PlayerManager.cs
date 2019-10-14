using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Vector2 playerBasePose = new Vector2(0, 2);//à considérer comme une constante

    [System.NonSerialized]
    public Vector2 targetPosition;
    
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
        if (targetPosition != playerBasePose)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if(targetPosition.x > 0)
            {
                //this.GetComponent<Renderer>().
            }
            else
            {

            }
        }
    }
}
