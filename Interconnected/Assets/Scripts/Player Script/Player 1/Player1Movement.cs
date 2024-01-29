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

    public bool isBraking;
    public bool isBrakingWithInput;

    [SerializeField] float curPlayerSpeed;
    [SerializeField] float maxPlayerSpeed;
    [SerializeField] float playerBrakingPower;

    [HideInInspector]
    public Vector2 inputDir;

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
            rb.AddForce(inputDir * curPlayerSpeed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxPlayerSpeed);
        }

    }

    private void playerSpeedComparison() 
    {
        if(globalVariable.isTriggeredWithObstacle || globalVariable.isGameFinish) 
        {
            maxPlayerSpeed = 0;
        }
        else 
        {
            maxPlayerSpeed = curPlayerSpeed;
        }
    }

    private void playerBraking() 
    {
        if (!player1Collision.isCrashToOtherBoat) 
        {
            if (isBraking || isBrakingWithInput)
            {
                rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, 1 * Time.deltaTime);
            }
            else { rb.drag = 0; }
        }
        else 
        {
            rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, 1 * Time.deltaTime);
        }
    }

    //movement input
    public void playerMovementInput(InputAction.CallbackContext context) 
    {
        if(!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart) 
        {
            if (context.performed) 
            {
                if (!isBrakingWithInput) 
                {
                    isBraking = false;
                }
            }
            else 
            {
                isBraking = true;
           
            }
            inputDir = context.ReadValue<Vector2>();
        }
    }

    //braking input
    public void playerBrakeInput(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            isBrakingWithInput = true;

        }
        else { isBrakingWithInput = false; }
    }

   
}