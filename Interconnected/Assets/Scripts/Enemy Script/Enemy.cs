using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] EnemyHitEffect enemyHitEffect1;

    [Header("Enemy Movement")]
    [SerializeField] float enemyMovementSpeed;

    [Header("Enemy Status")]
    [SerializeField] float enemyHealth;

    Rigidbody2D rb;

    GameObject player1, player2;

    Vector2 dir, movement;

    int indexPlayer;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");

        indexPlayer = Random.Range(0, 2);
    }

    private void Update()
    {
        if (enemyHealth <= 0) 
        {
            Destroy(gameObject);
        }

        enemyChassingPlayer();
    }

    private void FixedUpdate()
    {
        enemyMovement(movement);
    }

    private void enemyChassingPlayer() 
    {
        if (GlobalVariable.globalVariable.isGameOver) 
        {
            Destroy(gameObject);
        }
        
        if (player1 != null) 
        {
            if (!Player1.player1.isGhosting) 
            {
                if (indexPlayer == 0)
                {
                    if (!Player1.player1.isKnockedOut)
                    {
                        dir = player1.transform.position - transform.position;
                    }
                    else
                    {
                        indexPlayer = 1;
                    }
                }
            }
            else if(Player1.player1.isGhosting && !Player2.player2.isGhosting) 
            {
                dir = player2.transform.position - transform.position;
                indexPlayer = 1;
            }
            
        }

        if (player2 != null) 
        {
            if (!Player2.player2.isGhosting) 
            {
                if (indexPlayer == 1)
                {
                    if (!Player2.player2.isKnockedOut)
                    {
                        dir = player2.transform.position - transform.position;
                    }
                    else
                    {
                        indexPlayer = 0;
                    }
                }
            }
            else if (!Player1.player1.isGhosting && Player2.player2.isGhosting)
            {
                dir = player1.transform.position - transform.position;
                indexPlayer = 0;
            }

        }
        else { return; }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        dir.Normalize();
        movement = dir;
    }

    private void enemyMovement(Vector2 direction) 
    {
        rb.MovePosition((Vector2)transform.position + (direction * enemyMovementSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Bullet P1" || collision.gameObject.tag=="Bullet P2") 
        {
            enemyHealth -= 1;
            EnemyHitEffect enemyHitEffect = gameObject.GetComponent<EnemyHitEffect>();
           
        }
        if(collision.gameObject.tag=="Player 1") 
        {
            if (!Player1.player1.isKnockedOut) 
            {
                Destroy(gameObject);
            }
        }
        if(collision.gameObject.tag=="Player 2") 
        {
            if (!Player2.player2.isKnockedOut) 
            {
                Destroy(gameObject);
            }
        }
        
    }
}
