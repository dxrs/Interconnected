using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    public static GameOver gameOver;

    public bool isGameOver;

    [SerializeField] GlobalVariable globalVariable;

    GameObject player1, player2;

    bool isSlowTime=false;

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
        if ((Player1Health.player1Health.curPlayer1Health <= 0 || Player2Health.player2Health.curPlayer2Health <= 0)
            || (Timer.timerInstance.isTimerLevel && Timer.timerInstance.curTimerValue <= 0))
        {
            isGameOver = true;
        }
        if (isGameOver)
        {
            if (Timer.timerInstance.isTimerLevel) 
            {
                if (Timer.timerInstance.curTimerValue <= 0) 
                {
                    globalVariable.colliderInactive();
                }
            }
            if(Player1Health.player1Health.curPlayer1Health <= 0 || Player2Health.player2Health.curPlayer2Health <= 0) 
            {
                globalVariable.playerInvisible();
                if(!isSlowTime)
                {
                    Time.timeScale=.3f;
                    isSlowTime=true;
                }
                StartCoroutine(setDefaultTimeScale());
                Destroy(player1, 1);
                Destroy(player2, 1);
            }
            
        }
    }
    IEnumerator setDefaultTimeScale()
    {
        yield return new WaitForSecondsRealtime(1.5f);
        Time.timeScale=1;
    }
}
