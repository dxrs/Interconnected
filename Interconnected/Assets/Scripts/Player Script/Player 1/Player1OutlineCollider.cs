using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1OutlineCollider : MonoBehaviour
{
    [SerializeField] Player1Stamina player1Stamina;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Garbage" && LinkRay.linkRay.isPlayerLinkedEachOther) 
        {
            if (player1Stamina != null) 
            {
                player1Stamina.curStamina -= 5;
                if (player1Stamina.curStamina < 0) { player1Stamina.curStamina = 0; }
                player1Stamina.staminaFunctionCallback();
            }
         
        }

    }
}
