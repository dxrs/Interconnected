using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
    private void Update()
    {
        if (Player1.player1.isKnockedOut && Player2.player2.isKnockedOut) 
        {
            Destroy(GameObject.FindGameObjectWithTag("Player 1"));
            Destroy(GameObject.FindGameObjectWithTag("Player 2"));
        }
    }
}
