using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player1Movement : MonoBehaviour
{
    public static Player1Movement player1Movement;

    [SerializeField] LinkRay linkRay;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player1Ability player1Ability;
    [SerializeField] Player1Collision player1Collision;

    public bool isMoving;
    public bool isBraking;
    public bool isBrakingWithInput;

    public float maxPlayerSpeed;
    [SerializeField] float curPlayerSpeed;
    [SerializeField] float playerBrakingPower;

    [HideInInspector]
    public Vector2 inputDir;

    [SerializeField] Transform playerSprite;
    bool isFacingRight;
    float faceDirection;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player1Movement == null) { player1Movement = this; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isBraking = true;
    }

    private void Update()
    {
        playerSpeedComparison();
        print(inputDir);
        faceDirection = inputDir.x;
        if(faceDirection > 0 && isFacingRight )
        {
            flip();
        }
        if(faceDirection < 0 && !isFacingRight )
        {
            flip();
        }
    }

    private void FixedUpdate()
    {
        if (player1Ability.isDashing) { return; }
        playerMovement();
        playerBraking();
    }

    private void playerMovement() 
    {
        if (!isBraking && !isBrakingWithInput) 
        {
            rb.AddForce(inputDir * maxPlayerSpeed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, curPlayerSpeed);
        }

    }

    private void playerSpeedComparison() 
    {
        if (maxPlayerSpeed <= 0) 
        {
            if(linkRay.isPlayerLinkedEachOther && !globalVariable.isTriggeredWithObstacle) 
            {
                StartCoroutine(setMaxSpeedPlayer());
            }
        }
        if (!linkRay.isPlayerLinkedEachOther || globalVariable.isTriggeredWithObstacle) 
        {
            maxPlayerSpeed = 5;

        }
        if(globalVariable.isTriggeredWithObstacle 
            || GameFinish.gameFinish.isGameFinish 
            || globalVariable.isPlayerSharingLives
            || player1Collision.isStopAtCameraTrigger) 
        {
            curPlayerSpeed = 0;
        }
        else 
        {
            curPlayerSpeed = maxPlayerSpeed;
        }
        if (GameFinish.gameFinish.isGameFinish) 
        {
            maxPlayerSpeed = Mathf.Lerp(maxPlayerSpeed,0,5*Time.deltaTime);
            rb.drag = Mathf.Lerp(rb.drag, 10, 6 * Time.deltaTime);
        }
    }

    private void playerBraking() 
    {
        if (!GameFinish.gameFinish.isGameFinish) 
        {
            if (isBraking || player1Collision.isCrashToOtherBoat)
            {
                float lerpSpeed = isBrakingWithInput ? 10f : 2.5f;
                rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, lerpSpeed * Time.deltaTime);
            }
            else if (isBrakingWithInput)
            {
                rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, 5f * Time.deltaTime);
            }
            else
            {
                // Menggunakan deltaTime untuk menjaga kecepatan yang konsisten
                Vector2 force = inputDir * maxPlayerSpeed * Time.deltaTime;

                // Menggunakan AddForce untuk memberikan kekuatan pada rigidbody
                rb.AddForce(force, ForceMode2D.Impulse);

                // Membatasi kecepatan agar tidak melebihi maksimum
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, curPlayerSpeed);

                rb.drag = 0; // Mengatur rb.drag menjadi 0 ketika tidak ada pengereman
                //return;
            }
        }


        if (isMoving)
        {
            isBraking = false;
        }
        if (!isMoving) { isBraking = true; }
    }

    IEnumerator setMaxSpeedPlayer() 
    {
        yield return new WaitForSeconds(1);
        maxPlayerSpeed = 5;
    }

    //movement input
    public void playerMovementInput(InputAction.CallbackContext context) 
    {
        bool isLevel4 = LevelStatus.levelStatus.levelID == 4;

        if (!isLevel4 || !player1Collision.isStopAtCameraTrigger)
        {
            if (!globalVariable.isTriggeredWithObstacle
                && !GameFinish.gameFinish.isGameFinish
                && !GameOver.gameOver.isGameOver
                && !Pause.pause.isGamePaused
                && ReadyToStart.readyToStart.isGameStart
                && !globalVariable.isPlayerSharingLives)
            {
                if (context.performed)
                {
                    MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
                    //isBraking = false;
                    isMoving = true;
                }
                else
                {
                    //isBraking = true;
                    isMoving = false;
                }
                inputDir = context.ReadValue<Vector2>();
            }
        }
    }

    //braking input
    public void playerBrakeInput(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            isBrakingWithInput = true;

        }
        else { isBrakingWithInput = false; }
    }


    //buat hadap spite
    private void flip()
    {
        isFacingRight = !isFacingRight;
        playerSprite.transform.Rotate(0,180,0);
    }

   
}
