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
    

    //Basic Ability
    [Header("Player 1 Basic Ability")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [SerializeField] float dashCooldown;
    bool isDashing;
    bool canDash;


    bool isBreaking;

    Vector2 inputDir;

    Rigidbody2D rb;



    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        isBreaking = true;
        canDash = true;
    }

    private void Update()
    {
        
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

    

    #region player 1 movement function
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

    #region player 1 give health function & ability trigger
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

    #region player 1 breaking function
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

    public void player1dashing(InputAction.CallbackContext context) 
    {
        
        if (context.performed) 
        {
            StartCoroutine(dashing());

        }
        
       
    }

    IEnumerator dashing()
    {
        //canDash = false;
        isDashing = true;
        rb.velocity = new Vector2(inputDir.x * dashSpeed, inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
       // yield return new WaitForSeconds(dashCooldown);
        //canDash = true;
    }

    #endregion

    public void changeLinkMethod(InputAction.CallbackContext context)
    {
        if (context.performed)
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
}
