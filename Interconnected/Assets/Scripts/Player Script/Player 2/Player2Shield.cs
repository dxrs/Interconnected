using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player2Shield : MonoBehaviour
{
    public static Player2Shield player2Shield;

    public bool isShieldInactive;

    public int[] playerShieldHealth;

    private void Awake()
    {
        if (player2Shield == null) { player2Shield = this; }
    }

    private void Start()
    {
        for(int j = 0; j < playerShieldHealth.Length; j++) 
        {
            playerShieldHealth[j] = 5;
        }
    }

    private void Update()
    {
        if (playerShieldHealth[0] <= 0 || playerShieldHealth[1] <= 0) 
        {
            isShieldInactive = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        string[] playerShieldTags = GlobalVariable.globalVariable.playerShieldTagCollision;
        if (playerShieldTags.Contains(collision.gameObject.tag))
        {
            if (playerShieldHealth[0] > 0) 
            {
                playerShieldHealth[0]--;
            }
        }
    }
}
