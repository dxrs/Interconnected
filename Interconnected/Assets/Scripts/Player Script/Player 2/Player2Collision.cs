using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Collision : MonoBehaviour
{
    public static Player2Collision player2Collision;

    public bool isCrashToOtherBoat;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player2Movement player2Movement;
    [SerializeField] Player2Ability player2Ability;
    [SerializeField] Player2Health player2Health;

    [SerializeField] ParticleSystem playerHitParticle;

    [SerializeField] float crashForceValue;

    [SerializeField] GameObject playerCrashObject;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player2Collision == null) { player2Collision = this; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void Update()
    {
        playerCrashObject.transform.position = transform.position;
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Spike" || collision.gameObject.tag == "Trap" || collision.gameObject.tag=="Gear") 
        {
            if (!player2Ability.isShielding) 
            {
                player2Health.curPlayer2Health--;
                player2Movement.isBraking = true;
                globalVariable.isTriggeredWithObstacle = true;
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
                StartCoroutine(player2SetPosToCheckpoint());
            }
            else 
            {
                if (!globalVariable.isTriggeredWithObstacle)
                {
                    player2BouncedCollision(collision);
                }
            }
        }

        if (collision.gameObject.tag == "Player 1 Crash Trigger")
        {
            if (!globalVariable.isTriggeredWithObstacle)
            {
                player2BouncedCollision(collision);
            }

        }

       
        if (collision.gameObject.tag == "Enemy")
        {
            if (!player2Ability.isShielding) 
            {
                player2Health.curPlayer2Health--;
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
            }
            else 
            {
                if (!globalVariable.isTriggeredWithObstacle) 
                {
                    player2BouncedCollision(collision);
                }
               
            }
   
        }
    }

    public void player2BouncedCollision(Collider2D collider) 
    {
        player2Movement.isBraking = false;
        player2Movement.isBrakingWithInput = false;
        StartCoroutine(playerCrash());
        Vector2 backwardMovePos = (transform.position - collider.transform.position).normalized;

        rb.AddForce(backwardMovePos * crashForceValue, ForceMode2D.Impulse);
    }

    IEnumerator playerCrash()
    {
        isCrashToOtherBoat = true;
        yield return new WaitForSeconds(.1f);
        if (!player2Movement.isBraking)
        {
            player2Movement.isBraking = true;
        }
        isCrashToOtherBoat = false;
    }

    IEnumerator player2SetPosToCheckpoint()
    {
        yield return new WaitForSeconds(.5f);
        globalVariable.isTriggeredWithObstacle = false;
    }
}
