using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraSelectChapter : MonoBehaviour
{
    [SerializeField] float scrollSpeed;
    [SerializeField] float limitScrollX_Plus = 100f; 
    [SerializeField] float limitScrollX_Min = -100f; 
    [SerializeField] float limitScrollY_Plus = 100f; 
    [SerializeField] float limitScrollY_Min = -100f; 


    float scrollBoundary = 25.0f;
    float currentScrollSpeed = 15;
    

    Vector3 moveDirection;
    Vector3 mousePosition;
    Vector2 screenSize;

    private void Start()
    {
        scrollSpeed = currentScrollSpeed;
    }

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

        if(SelectChapter.selectChapter.isReadyToInteractWIthMap && SelectChapter.selectChapter.isSelectChapterActive) 
        {
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
            if (transform.position.x >= limitScrollX_Plus && totalMoveDirection.x > 0)
            {
                totalMoveDirection.x = 0;
            }
            else if (transform.position.x <= limitScrollX_Min && totalMoveDirection.x < 0)
            {
                totalMoveDirection.x = 0;
            }

            if (transform.position.y >= limitScrollY_Plus && totalMoveDirection.y > 0)
            {
                totalMoveDirection.y = 0;
            }
            else if (transform.position.y <= limitScrollY_Min && totalMoveDirection.y < 0)
            {
                totalMoveDirection.y = 0;
            }

            transform.position += totalMoveDirection * scrollSpeed * Time.deltaTime;
        }
        
      
    }
    public void navigationMoveMap(InputAction.CallbackContext context) 
    {
        if (SelectChapter.selectChapter.isReadyToInteractWIthMap && SelectChapter.selectChapter.isSelectChapterActive)
        {
            if (context.performed)
            {
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            }
            moveDirection = context.ReadValue<Vector2>();
        }
       
    }

    
}

