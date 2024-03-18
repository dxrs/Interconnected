using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSelectChapter : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    [SerializeField] float edgeScrollLimitXPositive = 100f; // Batasan edge scroll pada sumbu X positif
    [SerializeField] float edgeScrollLimitXNegative = -100f; // Batasan edge scroll pada sumbu X negatif
    [SerializeField] float edgeScrollLimitYPositive = 100f; // Batasan edge scroll pada sumbu Y positif
    [SerializeField] float edgeScrollLimitYNegative = -100f; // Batasan edge scroll pada sumbu Y negatif


    float scrollBoundary = 25.0f;
    float currentScrollSpeed = 15;
    

    Vector3 moveDirection;
    Vector3 mousePosition;
    Vector2 screenSize;

    void Update()
    {
        if (!MouseCursorActivated.mouseCursorActivated.isMouseActive) 
        {
            if (SelectChapter.selectChapter.curValueButton != 0)
            {
                scrollSpeed = 7.5f;
            }
            else
            {
                scrollSpeed = currentScrollSpeed;
            }
        }
        else 
        {
            scrollSpeed = currentScrollSpeed;
        }
        
        mousePosition = Input.mousePosition;

        screenSize = new Vector2(Screen.width, Screen.height);


        float moveX = 0;
        float moveY = 0;

        if (mousePosition.y >= screenSize.y - scrollBoundary)
        {
            moveY = 1;
        }
        else if (mousePosition.y <= scrollBoundary)
        {
            moveY = -1;
        }

        if (mousePosition.x >= screenSize.x - scrollBoundary)
        {
            moveX = 1;
        }

        else if (mousePosition.x <= scrollBoundary)
        {
            moveX = -1;
        }

       
        Vector3 edgeScrollDirection = new Vector2(moveX, moveY).normalized;
        Vector3 totalMoveDirection = moveDirection + edgeScrollDirection;

        // Menerapkan batasan edge scroll untuk setiap sumbu
        if (transform.position.x >= edgeScrollLimitXPositive && totalMoveDirection.x > 0)
        {
            totalMoveDirection.x = 0;
        }
        else if (transform.position.x <= edgeScrollLimitXNegative && totalMoveDirection.x < 0)
        {
            totalMoveDirection.x = 0;
        }

        if (transform.position.y >= edgeScrollLimitYPositive && totalMoveDirection.y > 0)
        {
            totalMoveDirection.y = 0;
        }
        else if (transform.position.y <= edgeScrollLimitYNegative && totalMoveDirection.y < 0)
        {
            totalMoveDirection.y = 0;
        }

        transform.position += totalMoveDirection * scrollSpeed * Time.deltaTime;
    }
    public void navigationMoveMap(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
        }
        moveDirection = context.ReadValue<Vector2>();
    }

    
}

