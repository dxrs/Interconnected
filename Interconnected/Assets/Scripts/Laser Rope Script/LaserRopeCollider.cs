using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRopeCollider : MonoBehaviour
{

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage")) 
        {
            GarbageCollector.garbageCollector.garbageCollected++;
            if (Player1Movement.player1Movement.maxPlayerSpeed > 1 && Player2Movement.player2Movement.maxPlayerSpeed > 1) 
            {
                Player1Movement.player1Movement.maxPlayerSpeed -= 0.5f;
                Player2Movement.player2Movement.maxPlayerSpeed -= 0.5f;
            }


        }
    }
   
}
