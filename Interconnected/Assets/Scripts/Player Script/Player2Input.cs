using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Mathematics;

public class Player2Input : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;

    #region basic ability variable
    [Header("Player 2 Basic Ability Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    bool isDashing;
    [Header("Player 2 Basic Ability Ghost")]
    [SerializeField] float ghostDuration;
    [SerializeField] Color curPlayerTransparentColor;
    float colorGhostA = 0.4f;
    float curColorA = 1f;
    SpriteRenderer spriteRenderer;
    bool isGhosting;
    #endregion

    #region variable stamina
    [Header("Player 2 Stamina")]
    [SerializeField] Image staminaImg;
    [SerializeField] float maxStamina;
    [SerializeField] float curStamina;
    [SerializeField] float dashStaminaCost;
    [SerializeField] float ghostStaminaCost;
    [SerializeField] float staminaRegenRate;
    Coroutine staminaRegen;
    #endregion

    bool isBreaking;

    Vector2 inputDir;

    Rigidbody2D rb;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isBreaking = true;
        curPlayerTransparentColor.a = curColorA;
        spriteRenderer.color = curPlayerTransparentColor;
    }
    private void Update()
    {
        Ghosting();
        changeLayer();
    }
    private void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }
        rb.AddForce(inputDir * curSpeed);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        player2IsBreaking();
    }
    

    #region player 2 movement function
    public void p2Move(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isBreaking = false;
            
        }
        else
        {
            isBreaking = true;
        }

        inputDir = context.ReadValue<Vector2>();
    }
    #endregion

    #region player 2 give health function & ability trigger
    public void p2GiveHealth(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("give health to p1!!!" + context.phase);
        }
    }

   
    #endregion

    #region player 2 breaking function
    void player2IsBreaking()
    {
        if (isBreaking)
        {
            rb.drag = math.lerp(rb.drag, 3, 1 * Time.deltaTime);
        }
        else
        {
            rb.drag = 0;
        }
    }
    #endregion

    #region player 1 basic ability

    public void player1Dashing(InputAction.CallbackContext context)
    {
        if (context.performed && !isBreaking)
        {
            StartCoroutine(dashing());
        }
    }

    public void player1Ghosting(InputAction.CallbackContext context)
    {
        if (context.performed && !isGhosting)
        {
            isGhosting = true;
        }
    }

    IEnumerator dashing()
    {
        isDashing = true;
        rb.velocity = new Vector2(inputDir.x * dashSpeed, inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private void Ghosting()
    {
        if (isGhosting)
        {
            curColorA = math.lerp(curColorA, colorGhostA, 1.5f * Time.deltaTime);
            curPlayerTransparentColor.a = curColorA;
            spriteRenderer.color = curPlayerTransparentColor;
            if (ghostDuration > 0)
            {
                ghostDuration -= 1 * Time.deltaTime;
            }
        }
        if (isGhosting && ghostDuration <= 0)
        {
            isGhosting = false;
            ghostDuration = 10;
        }
        if (!isGhosting)
        {
            curColorA = math.lerp(curColorA, 1, 1.5f * Time.deltaTime);
            curPlayerTransparentColor.a = curColorA;
            spriteRenderer.color = curPlayerTransparentColor;
        }
    }

    #endregion

    #region change link dan change layer player 2

    private void changeLayer()
    {
        if (!linkRay.isLinkedToPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Obstacle");
        }
    }

    public void changeLinkMethod(InputAction.CallbackContext context)
    {
        if (context.performed && !isGhosting)
        {
            if (!linkRay.isLinkedToPlayer)
            {
                linkRay.isLinkedToPlayer = true;
            }
            else
            {
                linkRay.isLinkedToPlayer = false;
            }
        }
    }
    #endregion

    //gamepad disconnect
    public void gamepadDiconnected() 
    {
        //disconnect function
    }

    //gamepad reconnect
    public void gamepadReconnect() 
    {
        //reconnect function
    }
}
