using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorActivated : MonoBehaviour
{
    public static MouseCursorActivated mouseCursorActivated;

    public bool isMouseActive;

    private void Awake()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        mouseCursorActivated = this;
    }

    

    private void Update()
    {
        if (isMouseActive) 
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None; 
        }
        else 
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.1f || Mathf.Abs(mouseY) > 0.1f)
        {
            isMouseActive = true;
            
        }
    }
}
