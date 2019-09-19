using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public Vector2 PLAYER_BASE_POS = new Vector2(0, 2);//à considérer comme une constante

    [System.NonSerialized]
    public Vector2 targetPosition;

    //private float scaleFactor;
    private Vector2 positionOnRightDoor;
    private Vector2 positionOnLeftDoor;

    public float moveSpeed = 3f;

    // Start is called before the first frame update
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

        transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        //change la taille du perso en fonction de la position en y du player
        //MARCHE A MOITIE
        /*
        float dist = startPosition.y + transform.position.y;
        Debug.Log(dist);
        if (dist > 4)
        {
            transform.localScale = Vector3.forward;
            return;
        }

        Vector3 newScale = Vector3.MoveTowards(Vector3.one, Vector3.forward, dist/4);
        transform.localScale = newScale;
        */
    }
}
