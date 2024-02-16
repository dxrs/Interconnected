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

    public bool isMoving;
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
        if (maxPlayerSpeed <= 0)
        {
            if (linkRay.isPlayerLinkedEachOther && !globalVariable.isTriggeredWithObstacle)
            {
                StartCoroutine(setMaxSpeedPlayer());
            }
        }
        if (!linkRay.isPlayerLinkedEachOther || globalVariable.isTriggeredWithObstacle)
        {
            maxPlayerSpeed = 5;
        }
        if (globalVariable.isTriggeredWithObstacle 
            || GameFinish.gameFinish.isGameFinish 
            || globalVariable.isPlayerSharingLives
            || player2Collision.isStopAtCameraTrigger)
        {
            curPlayerSpeed = 0;
        }
        else
        {
            curPlayerSpeed = maxPlayerSpeed;
        }
        if (GameFinish.gameFinish.isGameFinish)
        {
            maxPlayerSpeed = Mathf.Lerp(maxPlayerSpeed, 0, 5 * Time.deltaTime);
            rb.drag = Mathf.Lerp(rb.drag, 10, 6 * Time.deltaTime);
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
        if (!GameFinish.gameFinish.isGameFinish) 
        {
            if (isBraking || player2Collision.isCrashToOtherBoat)
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
                Vector2 force = inputDir * maxPlayerSpeed * Time.deltaTime;

                rb.AddForce(force, ForceMode2D.Impulse);
                rb.velocity = Vector2.ClampMagnitude(rb.velocity, curPlayerSpeed);

                rb.drag = 0; 
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

    public void playerMovementInput(InputAction.CallbackContext context)
    {
        bool isLevel4 = LevelStatus.levelStatus.levelID == 4;

        if (!isLevel4 || !player2Collision.isStopAtCameraTrigger)
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
                    isMoving = true;
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
        if (context.performed)
        {
            isBrakingWithInput = true;

        }
        else { isBrakingWithInput = false; }
    }
}
