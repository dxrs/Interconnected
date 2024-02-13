using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2OutlineCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage"))
        {
            if (Player2Stamina.player2Stamina.curStamina > 0)
            {
                Player2Stamina.player2Stamina.curStamina -= 0.05f;
            }

            if (Player2Stamina.player2Stamina.curStamina < 0) { Player2Stamina.player2Stamina.curStamina = 0; }
            Player2Stamina.player2Stamina.staminaFunctionCallback();
        }
    }
}
