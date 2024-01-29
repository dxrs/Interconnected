using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //[SerializeField] EnemyHitEffect enemyHitEffect1;

    public ParticleSystem deathParticle;


    [Header("Enemy Movement")]
    [SerializeField] float enemyMovementSpeed;

    [Header("Enemy Status")]
    [SerializeField] float enemyHealth;

    private bool isEnemyDestroyed = false;

    Rigidbody2D rb;

    GameObject player1, player2;

    EnemyHitEffect enemyHitEffect;

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
        if (enemyHealth <= 0 && !isEnemyDestroyed) 
        {
            EnemyTargetDestroy.enemyTargetDestroy.curValueEnemyDestroy++;
            isEnemyDestroyed = true;
            Instantiate(deathParticle,this.transform.position,Quaternion.identity);
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
        if (GlobalVariable.globalVariable.isGameOver
            || GlobalVariable.globalVariable.isGameFinish) 
        {
            Destroy(gameObject);
        }
        
        if (player1 != null) 
        {
            if (indexPlayer == 0)
            {
                dir = player1.transform.position - transform.position;
            }
        }

        if (player2 != null) 
        {
            if (indexPlayer == 1)
            {
                dir = player2.transform.position - transform.position;
               
            }
          
            

        }
        else { return; }

        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        dir.Normalize();
        movement = dir;
    }
    private void OnDestroy()
    {
        GlobalVariable.globalVariable.curEnemySpawn--;
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
            enemyHitEffect = GetComponent<EnemyHitEffect>();
            if (enemyHitEffect != null) 
            {
                enemyHitEffect.hitEffectEnemy();
            }
           
        }
        if(collision.gameObject.tag=="Player 1 Shield" || collision.gameObject.tag=="Player 2 Shield") 
        {
            Instantiate(deathParticle,transform.position,Quaternion.identity);
            enemyHealth = 0;
        }
        if (collision.gameObject.tag == "Wall") 
        {
            GlobalVariable.globalVariable.isTimerStart = true;
        }
        if(collision.gameObject.tag=="Player 1" || collision.gameObject.tag == "Player 2") 
        {

            Destroy(gameObject);

        }
       
        
    }
}
