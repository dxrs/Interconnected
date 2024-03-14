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

    float playerBrakingPower = 5;
    float totalMaxSpeedPlayer1 = 10;

    [HideInInspector]
    public Vector2 inputDir;
    [HideInInspector]
    public float curMaxSpeed;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player1Movement == null) { player1Movement = this; }
        curMaxSpeed = totalMaxSpeedPlayer1;
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isBraking = true;
        maxPlayerSpeed = curMaxSpeed;
    }

    private void Update()
    {
        playerSpeedComparison();
       
    }

    private void FixedUpdate()
    {
        //Debug.Log(Vector2.ClampMagnitude(rb.velocity, maxPlayerSpeed));
        if (player1Ability.isDashing) { return; }
        playerMovement();
        playerBraking();
    }

    private void playerMovement() 
    {
        if (!globalVariable.isPlayerDestroyed) 
        {
            if (!isBraking && !isBrakingWithInput)
            {
                Vector2 force = inputDir * maxPlayerSpeed * 1.3f * Time.deltaTime;

                rb.AddForce(force, ForceMode2D.Impulse);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxPlayerSpeed);

            }

            if (player1Collision.isHitGravityArea) 
            {
                if (isBraking) 
                {
                    rb.gravityScale = 2.5f;
                }
                else 
                {
                    rb.gravityScale = 0;
                }
            }
            else 
            {
                rb.gravityScale = 0;
            }

        }
        else 
        {
            rb.gravityScale = 0;
        }

    }

    private void playerSpeedComparison() 
    {
       
        if (!linkRay.isPlayerLinkedEachOther || globalVariable.isPlayerDestroyed || GarbageCollector.garbageCollector.garbageCollected == 0) 
        {
            curMaxSpeed = totalMaxSpeedPlayer1;
            maxPlayerSpeed = curMaxSpeed;

        }
        if(globalVariable.isPlayerDestroyed 
            || globalVariable.isPlayerSharingLives
            || player1Collision.isHitCameraBound
            || DialogueManager.dialogueManager.isDialogueActive
            || Pause.pause.isGamePaused) 
        {
            maxPlayerSpeed = 0;
        }
        
        else 
        {
            maxPlayerSpeed = curMaxSpeed;
        }
        if (GameFinish.gameFinish.isGameFinish) 
        {
            isMoving = false;
            maxPlayerSpeed = Mathf.Lerp(maxPlayerSpeed,0,5*Time.deltaTime);
            rb.drag = Mathf.Lerp(rb.drag, 10, 6 * Time.deltaTime);
            StartCoroutine(setConstRigidbody());
        }
        if (GameOver.gameOver.isGameOver) 
        {
            maxPlayerSpeed = 0;
            isMoving = false;
            StartCoroutine(setConstRigidbody());
        }
        if (player1Collision.isHitCameraBound || DialogueManager.dialogueManager.isDialogueActive || Pause.pause.isGamePaused) 
        {
            isMoving = false;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
        }
        else 
        {
            if (!GameFinish.gameFinish.isGameFinish) 
            {
                rb.constraints = ~RigidbodyConstraints2D.FreezePositionX & ~RigidbodyConstraints2D.FreezePositionY;
            }

        }
    }

    private void playerBraking() 
    {
        if (!GameFinish.gameFinish.isGameFinish) 
        {
            if (!globalVariable.isPlayerDestroyed) 
            {
                if (isMoving)
                {
                    isBraking = false;
                }
                if (!isMoving) { isBraking = true; }

                if (isBraking || player1Collision.isCrashToOtherBoat || player1Collision.isHitDoorButton)
                {
                    float lerpSpeed = player1Collision.isHitDoorButton ? 8f : 4f;
                    rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, lerpSpeed * Time.deltaTime);
                }
                else if (player1Collision.isHitDoorButton)
                {
                    rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, 5f * Time.deltaTime);
                }
                else
                {
             
                    rb.drag = 0; // Mengatur rb.drag menjadi 0 ketika tidak ada pengereman
                }
            }
            
        }
       
       
    }

    IEnumerator setConstRigidbody() 
    {
        yield return new WaitForSeconds(.5f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

   

    //movement input
    public void playerMovementInput(InputAction.CallbackContext context) 
    {
        if (!globalVariable.isPlayerDestroyed
                && !GameFinish.gameFinish.isGameFinish
                && !GameOver.gameOver.isGameOver
                && !Pause.pause.isGamePaused
                && ReadyToStart.readyToStart.isGameStart
                && !globalVariable.isPlayerSharingLives
                && !player1Collision.isHitCameraBound
                && !DialogueManager.dialogueManager.isDialogueActive)
        {
            if (context.performed)
            {
                globalVariable.isCameraBoundariesActive = true;
                MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
                if (globalVariable.isPlayerDestroyed)
                {
                    isMoving = false;
                }
                else { isMoving = true; }

            }
            else
            {
                isMoving = false;
            }
            inputDir = context.ReadValue<Vector2>();
        }
    }

    //braking input
    public void playerBrakeInput(InputAction.CallbackContext context) 
    {
        if (context.performed && !globalVariable.isPlayerDestroyed) 
        {
            MouseCursorActivated.mouseCursorActivated.isMouseActive = false;
            isBrakingWithInput = true;

        }
        else { isBrakingWithInput = false; }
    }

}
