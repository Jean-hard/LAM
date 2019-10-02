using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragandDrop : MonoBehaviour
{
    private bool selected;
    void Update()
    {
        if (selected==true)
        {
            Vector2 cursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position = new Vector2(cursorPos.x, cursorPos.y);
        }

        if(Input.GetKeyUp(KeyCode.Mouse0))
        {
            selected = false;
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
