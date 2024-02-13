using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Collision : MonoBehaviour
{
    public static Player2Collision player2Collision;

    public bool isCrashToOtherBoat;
    public bool isStopAtCameraTrigger;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player2Movement player2Movement;
    [SerializeField] Player2Ability player2Ability;
    [SerializeField] Player2Health player2Health;

    [SerializeField] ParticleSystem playerHitParticle;
    [SerializeField] float crashForceValue;
    [SerializeField] GameObject playerOutlineCollider;

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
        playerOutlineCollider.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        HandleObstacleCollision(collision);
        HandleOutlineColliderCollision(collision);
        HandleCameraMoveTriggerCollision(collision);
        HandleBrakingTriggerCollision(collision);
        HandleEnemyCollision(collision);
        HandleFinishPointCollision(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue--;

        if (collision.gameObject.tag == "Camera Move Trigger")
            isStopAtCameraTrigger = false;
    }

    private void HandleObstacleCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Gear"))
        {
            if (!player2Ability.isShielding)
            {
                if (LevelStatus.levelStatus.levelID != 4)
                    player2Health.curPlayer2Health--;

                player2Movement.isBraking = true;
                globalVariable.isTriggeredWithObstacle = true;
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
                StartCoroutine(player2SetPosToCheckpoint());
            }
            else
            {
                if (!globalVariable.isTriggeredWithObstacle)
                    player2BouncedCollision(collision);
            }
        }
    }

    private void HandleOutlineColliderCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1 Outline Collider" && !globalVariable.isTriggeredWithObstacle)
            player2BouncedCollision(collision);
    }

    private void HandleCameraMoveTriggerCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Camera Move Trigger")
            isStopAtCameraTrigger = true;
    }

    private void HandleBrakingTriggerCollision(Collider2D collision)
    {
        if (LevelStatus.levelStatus.levelID == 4 && collision.gameObject.tag == "Braking Trigger")
            Tutorial.tutorial.playerBrakingValue++;
    }

    private void HandleEnemyCollision(Collider2D collision)
    {
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
                    player2BouncedCollision(collision);
            }
        }
    }

    private void HandleFinishPointCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue++;
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
        yield return new WaitForSeconds(0.1f);
        if (!player2Movement.isBraking)
            player2Movement.isBraking = true;

        isCrashToOtherBoat = false;
    }

    IEnumerator player2SetPosToCheckpoint()
    {
        yield return new WaitForSeconds(0.5f);
        globalVariable.isTriggeredWithObstacle = false;
    }
}
