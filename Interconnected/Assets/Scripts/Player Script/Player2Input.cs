using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class Player2Input : MonoBehaviour
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

    public void p2Ability(InputAction.CallbackContext context) 
    {
        if (context.performed) 
        {
            Debug.Log("p2 ability" + context.phase);
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
