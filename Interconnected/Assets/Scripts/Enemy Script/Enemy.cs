using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] float enemyMovementSpeed;

    [Header("Enemy Status")]
    [SerializeField] float enemyHealth;

    #region enemy hit effect
    [Header("Enemy Hit Effect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    #endregion

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
        if (player1 != null && player1 != null) 
        {
            if (indexPlayer == 0 && !Player1.player1.isKnockedOut) 
            {
                dir = player1.transform.position - transform.position;
            }
            else if(Player1.player1.isKnockedOut)
            {
                dir = player2.transform.position - transform.position;
            }
            if (indexPlayer == 1 && !Player2.player2.isKnockedOut) 
            {
                dir = player2.transform.position - transform.position;
            }
            else if(Player2.player2.isKnockedOut) 
            {
                dir = player1.transform.position - transform.position;
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
        }
        if(collision.gameObject.tag=="Player 1"||collision.gameObject.tag=="Player 2") 
        {
            Destroy(gameObject);
        }
        
    }
}
