using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable globalVariable;

    public bool isEnteringSurvivalArea;

    public bool isGameOver;

    public bool isTriggeredWithObstacle;

    GameObject player1, player2;

    private void Awake()
    {
        globalVariable = this;
    }
    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }
    private void Update()
    {
        if (Player1.player1.isKnockedOut && Player2.player2.isKnockedOut) 
        {
            isGameOver = true;
        }

        if (player1 == null || player2 == null) 
        {
            isGameOver = true;
        }

        if (isGameOver) 
        {
            Destroy(player1);
            Destroy(player2);
        }
    }
}
