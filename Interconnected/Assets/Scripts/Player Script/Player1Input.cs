using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class Player1Input : MonoBehaviour
{
    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float dashSpeed;

    bool isBreaking;

    Vector2 inputDir;

    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
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
}
