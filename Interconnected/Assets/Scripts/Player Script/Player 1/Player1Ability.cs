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

    [Header("Player Dash")]
    public bool isDashing;
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;

    [Header("Player Pull Up")]
    public bool isPullingUp;
    public bool isPlayer1SetPosToPullUpObject;
    [SerializeField] int pullUpProgress;
    [SerializeField] GameObject player2;
    [SerializeField] GameObject pullUpObject;
    [SerializeField] float pullUpDuration;
    [SerializeField] float distanceFromPlayer2;

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
        if (Vector2.Distance(transform.position, player2.transform.position) > distanceFromPlayer2) 
        {
            //Debug.Log("jauh");
        }
        if (isPullingUp) 
        {
            if (pullUpProgress >= 2) 
            {
                pullUpProgress = 2;
            }
            if (pullUpDuration > 0 && pullUpProgress == 1) 
            {
                pullUpDuration -= 1 * Time.deltaTime;
            }

            if (player2.transform.position == pullUpObject.transform.position || pullUpDuration <= 0) 
            {
                isPullingUp = false;
            }

        }
        else 
        {
            pullUpObject.SetActive(false);
            pullUpDuration = 5;
            pullUpProgress = 0;
        }
    }
    IEnumerator Dashing() 
    {
        isDashing = true;
        rb.velocity = new Vector2(player1Movement.inputDir.x * dashSpeed, player1Movement.inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    
    //dash input
    public void dashInput(InputAction.CallbackContext context) 
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            if (!globalVariable.isPlayerDestroyed
                && !GameFinish.gameFinish.isGameFinish
                && !GameOver.gameOver.isGameOver
                && !Pause.pause.isGamePaused
                && ReadyToStart.readyToStart.isGameStart
                && !globalVariable.isPlayerSharingLives)
            {
                if (context.performed
                    && !player1Movement.isBraking
                    && !player1Movement.isBrakingWithInput
                    && player1Movement.maxPlayerSpeed > 3)
                {
                    if (player1Stamina.curStamina > player1Stamina.dashStaminaCost)
                    {
                        StartCoroutine(Dashing());
                        player1Stamina.curStamina -= player1Stamina.dashStaminaCost;
                        if (player1Stamina.curStamina < 0) { player1Stamina.curStamina = 0; }
                        player1Stamina.staminaFunctionCallback();
                    }
                }
            }
        }
        
    }
   
    //pull up
    public void pullUpInput(InputAction.CallbackContext context) 
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            if (!globalVariable.isPlayerDestroyed
               && !GameFinish.gameFinish.isGameFinish
               && !GameOver.gameOver.isGameOver
               && !Pause.pause.isGamePaused
               && ReadyToStart.readyToStart.isGameStart
               && !globalVariable.isPlayerSharingLives)
            {
                if (Vector2.Distance(transform.position, player2.transform.position) > distanceFromPlayer2 && !linkRay.isPlayerLinkedEachOther)
                {
                    if (context.performed)
                    {
                        if (!isPullingUp)
                        {
                            if (pullUpProgress <= 2)
                            {
                                pullUpProgress++;

                            }

                            isPullingUp = true;

                            if (pullUpProgress == 1)
                            {
                                pullUpObject.SetActive(true);
                                pullUpObject.transform.position = transform.position;
                            }
                            if (player1Stamina.curStamina > player1Stamina.pullUpStaminaCost * 2)
                            {
                                player1Stamina.curStamina -= player1Stamina.pullUpStaminaCost;
                                if (player1Stamina.curStamina < 0) { player1Stamina.curStamina = 0; }
                                player1Stamina.staminaFunctionCallback();
                            }
                        }
                        else
                        {
                            if (!isPlayer1SetPosToPullUpObject)
                            {
                                if (pullUpProgress <= 2)
                                {
                                    pullUpProgress++;

                                }
                                if (pullUpProgress == 2)
                                {
                                    player2.transform.position = pullUpObject.transform.position;
                                }
                                if (player1Stamina.curStamina > player1Stamina.pullUpStaminaCost * 2)
                                {
                                    player1Stamina.curStamina -= player1Stamina.pullUpStaminaCost;
                                    if (player1Stamina.curStamina < 0) { player1Stamina.curStamina = 0; }
                                    player1Stamina.staminaFunctionCallback();
                                }

                            }

                        }

                    }
                }

            }
        }
       
    }
   
}
