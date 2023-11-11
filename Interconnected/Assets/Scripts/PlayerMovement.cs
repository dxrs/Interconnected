using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Mathematics;

public class PlayerMovement : MonoBehaviour
{
    public Rigidbody2D rb;

    public float speed;
    public float maxSpeed;
    bool isBreaking;
    Vector2 inputDir;

    private void FixedUpdate() {
        rb.AddForce(inputDir*speed);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        playerBreak();
    }
    public void Movement(InputAction.CallbackContext contex)
    {
        if(contex.performed)
        {
            isBreaking = false;
        }
        else if(contex.canceled)
        {
            isBreaking = true;
        }
        inputDir = contex.ReadValue<Vector2>();
    }

    void playerBreak()
    {
        if(isBreaking)
        {
            rb.drag = math.lerp(rb.drag, 3, 1 * Time.deltaTime);
        }
        else
        {
            rb.drag = 0;
        }
    }
}
