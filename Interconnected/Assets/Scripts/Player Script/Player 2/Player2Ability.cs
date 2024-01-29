using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player2Ability : MonoBehaviour
{
    public static Player2Ability player2Ability;

    [SerializeField] LinkRay linkRay;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player2Movement player2Movement;
    [SerializeField] Player2Stamina player2Stamina;

    [Header("Player 2 Dash")]
    public bool isDashing;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    [Header("Player 2 Shield")]
    public bool isShielding;
    [SerializeField] GameObject playerShield;
    [SerializeField] float shieldDuration;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player2Ability == null) { player2Ability = this; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        rb.velocity = new Vector2(player2Movement.inputDir.x * dashSpeed, player2Movement.inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private void Shielding()
    {
        if (isShielding)
        {

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
            shieldDuration = 10;
            playerShield.SetActive(false);
        }
    }
    //dash input
    public void dashInput(InputAction.CallbackContext context)
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart)
        {
            if (context.performed
                && !player2Movement.isBraking
                && !player2Movement.isBrakingWithInput)
            {
                if (player2Stamina.curStamina > player2Stamina.shieldStaminaCost)
                {
                    StartCoroutine(Dashing());
                    player2Stamina.curStamina -= player2Stamina.dashStaminaCost;
                    if (player2Stamina.curStamina < 0) { player2Stamina.curStamina = 0; }
                    player2Stamina.staminaFunctionCallback();
                }
            }
        }
    }

    //shield input
    public void shieldInput(InputAction.CallbackContext context)
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart)
        {
            if (context.performed && !isShielding)
            {
                if (player2Stamina.curStamina > player2Stamina.shieldStaminaCost)
                {
                    isShielding = true;
                    player2Stamina.curStamina -= player2Stamina.shieldStaminaCost;
                    if (player2Stamina.curStamina < 0) { player2Stamina.curStamina = 0; }
                    player2Stamina.staminaFunctionCallback();
                }
            }
        }
    }

}
