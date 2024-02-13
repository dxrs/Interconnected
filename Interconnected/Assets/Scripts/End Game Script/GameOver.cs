using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver gameOver;

    public bool isGameOver;

    [SerializeField] GlobalVariable globalVariable;

    GameObject player1, player2;

    private void Awake()
    {
        gameOver = this;
    }

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    private void Update()
    {
        if (player1 == null || player2 == null)
        {
           //globalVariable.isGameOver = true;
        }

        if (isGameOver)
        {
            GlobalVariable.globalVariable.isTriggeredWithObstacle = true;
            //SceneSystem.sceneSystem.isRestartScene = true;
            //Destroy(player1);
            //Destroy(player2);
        }
    }
}
