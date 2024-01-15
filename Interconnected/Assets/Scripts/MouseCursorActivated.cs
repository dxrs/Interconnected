using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseCursorActivated : MonoBehaviour
{
    public static MouseCursorActivated mouseCursorActivated;

    public bool isMouseActive;


    private void Awake()
    {
        mouseCursorActivated = this;
    }

    private void Start()
    {
        //Cursor.visible = false;
    }

    private void Update()
    {
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        if (Mathf.Abs(mouseX) > 0.1f || Mathf.Abs(mouseY) > 0.1f)
        {
            isMouseActive = true;
        }
    }
}
