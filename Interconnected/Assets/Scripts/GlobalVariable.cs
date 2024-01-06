using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable globalVariable;

    public bool isEnteringSurvivalArea;
    public bool isEnteringTrapArea;

    public bool isGameOver;

    public bool isTriggeredWithObstacle;

    public bool[] circleIsTriggeredWithPlayers;

    public int maxDoor;

    public bool isNotShoot;

    [SerializeField] SpriteRenderer p1Sr;
    [SerializeField] SpriteRenderer p2Sr;

    [SerializeField] CircleCollider2D p1cc;
    [SerializeField] CircleCollider2D p2cc;

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
        if(player1 && player2 != null) 
        {
            if (isTriggeredWithObstacle)
            {
                p1Sr.enabled = false;
                p2Sr.enabled = false;
                p1cc.enabled = false;
                p2cc.enabled = false;
            }
            else
            {
                p1Sr.enabled = true;
                p2Sr.enabled = true;
                p1cc.enabled = true;
                p2cc.enabled = true;
            }
        }

        if (circleIsTriggeredWithPlayers[0] || circleIsTriggeredWithPlayers[1] == true) 
        {
            isNotShoot = true;
        }
        if (!circleIsTriggeredWithPlayers[0] && !circleIsTriggeredWithPlayers[1]) { isNotShoot = false; }
       
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
