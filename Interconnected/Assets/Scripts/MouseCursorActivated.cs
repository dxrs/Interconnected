using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorActivated : MonoBehaviour
{
    public static MouseCursorActivated mouseCursorActivated;

    public bool isMouseActive;

    private Vector2 lastMousePosition;

    private void Awake()
    {
        Cursor.visible = false;
        mouseCursorActivated = this;
        lastMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
    }

    

    private void Update()
    {
        if (isMouseActive) 
        {
            Cursor.visible = true;
        }
        else 
        {
            Cursor.visible = false;
            if (!Cursor.visible) 
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            Vector2 currentMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            if (currentMousePosition != lastMousePosition)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.lockState = CursorLockMode.None;
                lastMousePosition = currentMousePosition;
            }
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.1f || Mathf.Abs(mouseY) > 0.1f)
        {
            isMouseActive = true;
            
        }
    }
}
