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
        if (isGameOver)
        {
            GlobalVariable.globalVariable.playerInvisible();
            //SceneSystem.sceneSystem.isRestartScene = true;
            Destroy(player1,1);
            Destroy(player2,1);
            SceneSystem.sceneSystem.isRestartScene = true;
            if (player1 && player2 == null) 
            {
                
            }
        }
    }
}
