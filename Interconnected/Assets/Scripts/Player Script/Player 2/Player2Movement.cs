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

    float playerBrakingPower = 5;
    float totalMaxSpeedPlayer2 = 10;

    [HideInInspector]
    public Vector2 inputDir;
    [HideInInspector]
    public float curMaxSpeed;


    Rigidbody2D rb;

    private void Awake()
    {
        if (player2Movement == null) { player2Movement = this; }
        curMaxSpeed = totalMaxSpeedPlayer2;
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
        if (player2Ability.isDashing) { return; }
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
            if (player2Collision.isHitGravityArea)
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
            curMaxSpeed = totalMaxSpeedPlayer2;
            maxPlayerSpeed = curMaxSpeed;
        }
        if (globalVariable.isPlayerDestroyed 
            || globalVariable.isPlayerSharingLives
            || player2Collision.isHitCameraBound
            || DialogueManager.dialogueManager.isDialogueActive
            || Pause.pause.isGamePaused
            || player2Collision.isHitGarbageButton)
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
            maxPlayerSpeed = Mathf.Lerp(maxPlayerSpeed, 0, 5 * Time.deltaTime);
            rb.drag = Mathf.Lerp(rb.drag, 10, 6 * Time.deltaTime);
            StartCoroutine(setConstRigidbody());
        }

        if (GameOver.gameOver.isGameOver)
        {
            maxPlayerSpeed = 0;
            isMoving = false;
            StartCoroutine(setConstRigidbody());
        }

        if (player2Collision.isHitCameraBound || DialogueManager.dialogueManager.isDialogueActive || Pause.pause.isGamePaused || player2Collision.isHitGarbageButton)
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
            if (isMoving)
            {
                isBraking = false;
            }
            if (!isMoving) { isBraking = true; }

            if (!globalVariable.isPlayerDestroyed) 
            {
                if (isBraking || player2Collision.isCrashToOtherBoat || player2Collision.isHitDoorButton)
                {
                    float lerpSpeed = player2Collision.isHitDoorButton ? 8f : 4f;
                    rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, lerpSpeed * Time.deltaTime);
                }
                else if (player2Collision.isHitDoorButton)
                {
                    rb.drag = Mathf.Lerp(rb.drag, playerBrakingPower, 5f * Time.deltaTime);
                }
                else
                {
                   
                    rb.drag = 0;
                }
            }

        }
       
    }

    IEnumerator setConstRigidbody()
    {
        yield return new WaitForSeconds(.5f);
        rb.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
    }

    public void playerMovementInput(InputAction.CallbackContext context)
    {
        if (!globalVariable.isPlayerDestroyed
                && !GameFinish.gameFinish.isGameFinish
                && !GameOver.gameOver.isGameOver
                && !Pause.pause.isGamePaused
                && ReadyToStart.readyToStart.isGameStart
                && !globalVariable.isPlayerSharingLives
                && !player2Collision.isHitCameraBound
                && !DialogueManager.dialogueManager.isDialogueActive
                && !player2Collision.isHitGarbageButton)
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
            isBrakingWithInput = true;

        }
        else { isBrakingWithInput = false; }
    }
}
