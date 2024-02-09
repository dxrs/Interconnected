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
    [SerializeField] Player2Shield player2Shield;

    [SerializeField] GameObject[] crashTriggerObject;

    [Header("Player Dash")]
    public bool isDashing;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    [Header("Player Shield")]
    public bool isShielding;
    [SerializeField] GameObject[] playerShield;
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
        Shielding();
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
            crashTriggerObject[0].SetActive(false);
            crashTriggerObject[1].SetActive(false);
            playerShield[0].SetActive(true);
            playerShield[1].SetActive(true);
            if (shieldDuration > 0)
            {
                shieldDuration -= 1 * Time.deltaTime;
            }

            if(shieldDuration <= 0 || player2Shield.isShieldInactive) 
            {
                isShielding = false;
                shieldDuration = 10;
            }
        }
       

        if (!isShielding)
        {
            for(int j = 0; j < player2Shield.playerShieldHealth.Length; j++) 
            {
                player2Shield.playerShieldHealth[j] = 5;
            }
            crashTriggerObject[0].SetActive(true);
            crashTriggerObject[1].SetActive(true);
            shieldDuration = 10;
            playerShield[0].SetActive(false);
            playerShield[1].SetActive(false);
        }
    }
    //dash input
    public void dashInput(InputAction.CallbackContext context)
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart
            && !globalVariable.isPlayerSharingLives)
        {
            if (context.performed
                && !player2Movement.isBraking
                && !player2Movement.isBrakingWithInput
                && player2Movement.maxPlayerSpeed > 3)
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
            && ReadyToStart.readyToStart.isGameStart
            && !globalVariable.isPlayerSharingLives
            && linkRay.playerLinkedEachOther)
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
