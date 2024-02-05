using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player2Movement : MonoBehaviour
{
    public static Player2Movement player2Movement;

    [SerializeField] LinkRay linkRay;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player2Ability player2Ability;
    [SerializeField] Player2Collision player2Collision;

    public bool isBraking;
    public bool isBrakingWithInput;

    public float maxPlayerSpeed;
    [SerializeField] float curPlayerSpeed;
    [SerializeField] float playerBrakingPower;

    [HideInInspector]
    public Vector2 inputDir;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player2Movement == null) { player2Movement = this; }
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
        if (player2Ability.isDashing) { return; }
        playerMovement();
        playerBraking();
    }

    private void playerSpeedComparison()
    {
        if (globalVariable.isTriggeredWithObstacle || globalVariable.isGameFinish || globalVariable.isPlayerSharingLives)
        {
            curPlayerSpeed = 0;
        }
        else
        {
            curPlayerSpeed = maxPlayerSpeed;
        }
    }

    private void playerMovement()
    {
        if (!isBraking && !isBrakingWithInput)
        {
            rb.AddForce(inputDir * maxPlayerSpeed);
            rb.velocity = Vector2.ClampMagnitude(rb.velocity, curPlayerSpeed);
        }

    }

    private void playerBraking()
    {
        if (!player2Collision.isCrashToOtherBoat)
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
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart
            && !globalVariable.isPlayerSharingLives)
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
