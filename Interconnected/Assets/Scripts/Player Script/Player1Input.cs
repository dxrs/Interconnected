using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class Player1Input : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;


    #region basic ability variable
    [Header("Player 1 Basic Ability Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    bool isDashing;
    [Header("Player 1 Basic Ability Ghost")]
    [SerializeField] float ghostDuration;
    [SerializeField] Color curPlayerTransparentColor;
    float colorGhostA = 0.4f;
    float curColorA = 1f;
    SpriteRenderer spriteRenderer;
    bool isGhosting;
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
    }

    private void FixedUpdate()
    {
        if (isDashing) 
        {
            return;
        }
        rb.AddForce(inputDir * curSpeed);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        player1IsBreaking();
    }

    

    #region player 1 movement 
    public void p1Move(InputAction.CallbackContext context) 
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

    #region player 1 give health function
    public void p1GiveHealth(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            Debug.Log("give health to p2!!!" + context.phase);
        }

    }
    public void p1Ability(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("p1 ability" + context.phase);
        }
    }
    #endregion

    #region player 1 breaking 
    void player1IsBreaking() 
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
        if(context.performed && !isGhosting) 
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
        if(isGhosting && ghostDuration <= 0) 
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

    #region change link
    public void changeLinkMethod(InputAction.CallbackContext context)
    {
        if (context.performed && !isGhosting)
        {
            if (!linkRay.isChangeLinkMethod)
            {
                linkRay.isChangeLinkMethod = true;
            }
            else
            {
                linkRay.isChangeLinkMethod = false;
            }
        }
    }
    #endregion
}
