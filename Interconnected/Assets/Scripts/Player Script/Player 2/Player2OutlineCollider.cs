using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2OutlineCollider : MonoBehaviour
{
    [SerializeField] Player2Stamina player2Stamina;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Garbage" && LinkRay.linkRay.isPlayerLinkedEachOther) 
        {
            if (player2Stamina != null)
            {
                player2Stamina.curStamina -= 5;
                if (player2Stamina.curStamina < 0) { player2Stamina.curStamina = 0; }
                player2Stamina.staminaFunctionCallback();
            }
        }
       
    }
}
