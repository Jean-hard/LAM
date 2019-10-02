using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private bool selected;
    public List<GameObject> pos = new List<GameObject>();
    void Update()
    {
        if (selected==true)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
            gameObject.layer = 5;
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            selected = false;
            gameObject.layer = 0;
        }
        

    }

    private void OnMouseOver()
    {
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            selected = true;

        }
    }

    
}
