using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class Player1Ability : MonoBehaviour
{
    public static Player1Ability player1Ability;

    [SerializeField] LinkRay linkRay;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player1Movement player1Movement;
    [SerializeField] Player1Stamina player1Stamina;

    [SerializeField] GameObject crashTriggerObject;

    [Header("Player 1 Dash")]
    public bool isDashing;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    [Header("Player 1 Shield")]
    public bool isShielding;
    [SerializeField] GameObject playerShield;
    [SerializeField] float shieldDuration;

    Rigidbody2D rb;


    private void Awake()
    {
        if (player1Ability == null) { player1Ability = this; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Shielding();
    }
    IEnumerator Dashing() 
    {
        isDashing = true;
        rb.velocity = new Vector2(player1Movement.inputDir.x * dashSpeed, player1Movement.inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private void Shielding()
    {
        if (isShielding)
        {
            crashTriggerObject.SetActive(false);
            playerShield.SetActive(true);
            if (shieldDuration > 0)
            {
                shieldDuration -= 1 * Time.deltaTime;
            }
        }

        if (isShielding && shieldDuration <= 0)
        {
            isShielding = false;
            shieldDuration = 10;
        }

        if (!isShielding)
        {
            crashTriggerObject.SetActive(true);
            shieldDuration = 10;
            playerShield.SetActive(false);
        }
    }

    //dash input
    public void dashInput(InputAction.CallbackContext context) 
    {
        if(!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart
            && !globalVariable.isPlayerSharingLives) 
        {
            if(context.performed
                && !player1Movement.isBraking
                && !player1Movement.isBrakingWithInput) 
            {
                if (player1Stamina.curStamina > player1Stamina.shieldStaminaCost) 
                {
                    StartCoroutine(Dashing());
                    player1Stamina.curStamina -= player1Stamina.dashStaminaCost;
                    if (player1Stamina.curStamina < 0) { player1Stamina.curStamina = 0; }
                    player1Stamina.staminaFunctionCallback();
                }
            }
        }
    }
    //shield input
    public void shieldInput(InputAction.CallbackContext context) 
    {
        if(!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart
            && !globalVariable.isPlayerSharingLives) 
        {
            if (context.performed && !isShielding) 
            {
                if (player1Stamina.curStamina > player1Stamina.shieldStaminaCost)
                {
                    isShielding = true;
                    player1Stamina.curStamina -= player1Stamina.shieldStaminaCost;
                    if (player1Stamina.curStamina < 0) { player1Stamina.curStamina = 0; }
                    player1Stamina.staminaFunctionCallback();
                }
            }
        }
    }
}
