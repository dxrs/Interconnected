using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseCursorActivated : MonoBehaviour
{
    public static MouseCursorActivated mouseCursorActivated;

    public bool isMouseActive;

    [SerializeField] bool isMouseCanVisible;

    [SerializeField] float widthDivide;
    [SerializeField] float heighDivide;

    private void Awake()
    {
        mouseCursorActivated = this;
    }
    private void Start()
    {
        if (isMouseCanVisible) 
        {
            Cursor.visible = false;
        }
        else 
        {
            Cursor.visible = true;
        }
    }
    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.1f || Mathf.Abs(mouseY) > 0.1f)
        {
            isMouseActive = true;

        }
        if (!isMouseActive) 
        {
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width / widthDivide, Screen.height / heighDivide));
            if (isMouseCanVisible) 
            {
                Cursor.visible = false;
            }
        }
        else 
        {
            if (isMouseCanVisible) 
            {
                Cursor.visible = true;
            }
        }
        
       
    }
}
