using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player1Shield : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        string[] playerShieldTags = GlobalVariable.globalVariable.playerShieldTagCollision;
        if (playerShieldTags.Contains(collision.gameObject.tag))
        {
            if (Player2Shield.player2Shield.playerShieldHealth[1] > 0) 
            {
                Player2Shield.player2Shield.playerShieldHealth[1]--;
            }
        }
    }
}
