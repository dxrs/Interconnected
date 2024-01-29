using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Collision : MonoBehaviour
{
    public static Player1Collision player1Collision;

    public bool isCrashToOtherBoat;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player1Movement player1Movement;
    [SerializeField] Player1Ability player1Ability;
    [SerializeField] Player1Health player1Health;

    [SerializeField] ParticleSystem playerHitParticle;

    [SerializeField] float crashForceValue;

    [SerializeField] GameObject playerCrashObject;

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
        playerCrashObject.transform.position = transform.position;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Spike" || collision.gameObject.tag == "Gear") 
        {
            if (!player1Ability.isShielding) 
            {
                //player1Health.curPlayer1Health--;
                //globalVariable.isTriggeredWithObstacle = true;
                //particle
               // StartCoroutine(player1SetPosToCheckpoint());
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Gear")
        {
            if (!player1Ability.isShielding)
            {
                player1Health.curPlayer1Health--;
                //globalVariable.isTriggeredWithObstacle = true;
                //particle
                //StartCoroutine(player1SetPosToCheckpoint());
            }
        }

        if (collision.gameObject.tag=="Player 2 Crash Trigger") 
        {
            player1Movement.isBraking = false;
            player1Movement.isBrakingWithInput = false;
            StartCoroutine(playerCrash());
            Vector2 backwardMovePos = (transform.position - collision.transform.position).normalized;
            
            rb.AddForce(backwardMovePos * crashForceValue, ForceMode2D.Impulse);
            
        }
    }

    IEnumerator player1SetPosToCheckpoint() 
    {
        yield return new WaitForSeconds(1);
        globalVariable.isTriggeredWithObstacle = false;
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
