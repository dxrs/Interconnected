using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player1Collision : MonoBehaviour
{
    public static Player1Collision player1Collision;

    public bool isCrashToOtherBoat;

    public bool isStopAtCameraTrigger;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player1Movement player1Movement;
    [SerializeField] Player1Ability player1Ability;
    [SerializeField] Player1Health player1Health;

    [SerializeField] ParticleSystem playerHitParticle;

    [SerializeField] float crashForceValue;

    [SerializeField] GameObject playerOutlineCollider;


    Rigidbody2D rb;

    private void Awake()
    {
        if (player1Collision == null) { player1Collision = this; }
    }
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        playerOutlineCollider.transform.position = transform.position;
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Gear" || collision.gameObject.tag == "Trap")
        {
            if (!Player2Ability.player2Ability.isShielding)
            {
                if (LevelStatus.levelStatus.levelID != 4)
                {
                    player1Health.curPlayer1Health--;
                }

                player1Movement.isBraking = true;
                globalVariable.isTriggeredWithObstacle = true;
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
                StartCoroutine(player1SetPosToCheckpoint());
            }
            else 
            {
                if (!globalVariable.isTriggeredWithObstacle) 
                {
                    player1BoucedCollision(collision);
                }
  
            }
        }
       
        if(collision.gameObject.tag=="Player Pull Up Object") 
        {
            player1Ability.isPlayer1SetPosToPullUpObject = true;
        }
        if (collision.gameObject.tag=="Player 2 Outline Collider") 
        {

            if (!globalVariable.isTriggeredWithObstacle)
            {
                player1BoucedCollision(collision);
            }

        }

        if(collision.gameObject.tag=="Camera Move Trigger") 
        {
          
            isStopAtCameraTrigger = true;
        }
        if (LevelStatus.levelStatus.levelID == 4)
        {
            if (collision.gameObject.tag == "Braking Trigger")
            {
                Tutorial.tutorial.playerBrakingValue++;

            }
            
        }
       


        if (collision.gameObject.tag == "Enemy") 
        {
            if (!Player2Ability.player2Ability.isShielding) 
            {
                player1Health.curPlayer1Health--;
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
            }
            else 
            {
                if (!globalVariable.isTriggeredWithObstacle)
                {
                    player1BoucedCollision(collision);
                }
            }
   
        }
        if(collision.gameObject.tag== "Finish Point") 
        {
            GameFinish.gameFinish.finishValue++;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Pull Up Object")
        {
            player1Ability.isPlayer1SetPosToPullUpObject = false;
        }
        if (collision.gameObject.tag == "Camera Move Trigger")
        {
            isStopAtCameraTrigger = false;
        }
        if (collision.gameObject.tag == "Finish Point")
        {
            GameFinish.gameFinish.finishValue--;
        }
    }

    IEnumerator player1SetPosToCheckpoint() 
    {
        yield return new WaitForSeconds(.5f);
        globalVariable.isTriggeredWithObstacle = false;
    }

    public void player1BoucedCollision(Collider2D collider)
    {
        player1Movement.isBraking = false;
        player1Movement.isBrakingWithInput = false;
        StartCoroutine(playerCrash());
        Vector2 backwardMovePos = (transform.position - collider.transform.position).normalized;

        rb.AddForce(backwardMovePos * crashForceValue, ForceMode2D.Impulse);
    }

    IEnumerator playerCrash() 
    {
        isCrashToOtherBoat = true;
        yield return new WaitForSeconds(.1f);
        if (!player1Movement.isBraking) 
        {
            player1Movement.isBraking = true;
        }
        isCrashToOtherBoat = false;
    }
}
