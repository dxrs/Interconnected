using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRopeCollider : MonoBehaviour
{

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Garbage" ) 
        {
            if (Player1Movement.player1Movement.maxPlayerSpeed > 0 && Player2Movement.player2Movement.maxPlayerSpeed > 0) 
            {
                Player1Movement.player1Movement.maxPlayerSpeed -= 0.25f;
                Player2Movement.player2Movement.maxPlayerSpeed -= 0.25f;
            }


        }
    }
   
}
