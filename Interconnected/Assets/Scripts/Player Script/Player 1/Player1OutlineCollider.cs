using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1OutlineCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage"))
        {
            if (Player1Stamina.player1Stamina.curStamina > 0)
            {
                Player1Stamina.player1Stamina.curStamina -= 0.05f;
            }

            if (Player1Stamina.player1Stamina.curStamina < 0) { Player1Stamina.player1Stamina.curStamina = 0; }
            Player1Stamina.player1Stamina.staminaFunctionCallback();
        }
    }
}
