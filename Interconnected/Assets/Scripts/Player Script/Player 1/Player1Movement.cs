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

    [SerializeField] float playerBrakingPower;
    [SerializeField] float totalMaxSpeedPlayer1;

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
                rb.AddForce(inputDir * maxPlayerSpeed);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxPlayerSpeed);
                
            }
        }
       

    }

    private void playerSpeedComparison() 
    {
        if (maxPlayerSpeed <= 0) 
        {
            if(linkRay.isPlayerLinkedEachOther && !globalVariable.isPlayerDestroyed) 
            {
                StartCoroutine(setMaxSpeedPlayer());
            }
        }
        if (!linkRay.isPlayerLinkedEachOther || globalVariable.isPlayerDestroyed || GarbageCollector.garbageCollector.garbageCollected == 0) 
        {
            curMaxSpeed = totalMaxSpeedPlayer1;
            maxPlayerSpeed = curMaxSpeed;

        }
        if(globalVariable.isPlayerDestroyed 
            || globalVariable.isPlayerSharingLives
            || player1Collision.isStopAtCameraTrigger) 
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
            StartCoroutine(setSimulatedRigidbody());
        }
        if (GameOver.gameOver.isGameOver) 
        {
            maxPlayerSpeed = 0;
            isMoving = false;
            rb.simulated = false;
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

                if (isBraking || player1Collision.isCrashToOtherBoat)
                {
                    float lerpSpeed = isBrakingWithInput ? 10f : 3.5f;
                    rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, lerpSpeed * Time.deltaTime);
                }
                else if (isBrakingWithInput)
                {
                    rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, 5f * Time.deltaTime);
                }
                else
                {
                    Vector2 force = inputDir * maxPlayerSpeed * Time.deltaTime;

                    rb.AddForce(force, ForceMode2D.Impulse);
                    rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxPlayerSpeed);

                    rb.drag = 0; // Mengatur rb.drag menjadi 0 ketika tidak ada pengereman
                }
            }
            
        }
       
       
    }

    IEnumerator setSimulatedRigidbody() 
    {
        yield return new WaitForSeconds(.5f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
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
            if (!globalVariable.isPlayerDestroyed
                && !GameFinish.gameFinish.isGameFinish
                && !GameOver.gameOver.isGameOver
                && !Pause.pause.isGamePaused
                && ReadyToStart.readyToStart.isGameStart
                && !globalVariable.isPlayerSharingLives)
            {
                if (context.performed)
                {
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
