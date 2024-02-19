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
        handleDumpPointCollision(collision);
        handleObstacleCollision(collision);
        handleOutlineColliderCollision(collision);
        handleCameraMoveTriggerCollision(collision);
        handleBrakingTriggerCollision(collision);
        handleEnemyCollision(collision);
        handleFinishPointCollision(collision);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue--;

        if (collision.gameObject.tag == "Camera Move Trigger")
            isStopAtCameraTrigger = false;

        if (collision.gameObject.tag == "Garbage Center Point")
            GarbageCollector.garbageCollector.playerReadyToStoreValue[1] = 0;

        if (LevelStatus.levelStatus.levelID == 4 && collision.gameObject.tag == "Braking Trigger")
            Tutorial.tutorial.playerBrakingValue--;
    }

    private void handleObstacleCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("Gun Bullet"))
        {
            if (!player2Ability.isShielding)
            {
                if (LevelStatus.levelStatus.levelID != 4)
                    player2Health.curPlayer2Health--;

  
                rb.simulated = false;
                player2Movement.isMoving = false;
                player2Movement.isBraking = true;
                globalVariable.playerInvisible();
                if (player2Health.curPlayer2Health >= 1)
                {
                    globalVariable.isRopeVisible = false;
                    globalVariable.isPlayerDestroyed = true;
                    StartCoroutine(player2SetPosToCheckpoint());
                }
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
            }
            else
            {
                if (!globalVariable.isPlayerDestroyed)
                    player2BouncedCollision(collision);
            }
        }
    }

    private void handleDumpPointCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Garbage Center Point")
            GarbageCollector.garbageCollector.playerReadyToStoreValue[1] = 1;
    }
    private void handleOutlineColliderCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1 Outline Collider" )
            player2BouncedCollision(collision);
    }

    private void handleCameraMoveTriggerCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Camera Move Trigger")
            isStopAtCameraTrigger = true;
    }

    private void handleBrakingTriggerCollision(Collider2D collision)
    {
        if (LevelStatus.levelStatus.levelID == 4 && collision.gameObject.tag == "Braking Trigger")
            Tutorial.tutorial.playerBrakingValue++;
    }

    private void handleEnemyCollision(Collider2D collision)
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
                if (!globalVariable.isPlayerDestroyed)
                    player2BouncedCollision(collision);
            }
        }
    }

    private void handleFinishPointCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue++;
    }

    public void player2BouncedCollision(Collider2D collider)
    {
        StartCoroutine(playerCrash());

        // Mengurangi dampak pantulan jika isBrakingWithInput aktif
        float adjustedCrashForce = player2Movement.isBrakingWithInput ? crashForceValue * 0.5f : crashForceValue;

        Vector2 backwardMovePos = (transform.position - collider.transform.position).normalized;
        rb.AddForce(backwardMovePos * adjustedCrashForce, ForceMode2D.Impulse);
    }

    IEnumerator playerCrash()
    {
        // Menonaktifkan isBraking dan isBrakingWithInput selama beberapa waktu
        player2Movement.isBraking = false;
        player2Movement.isBrakingWithInput = false;
        isCrashToOtherBoat = true;
        yield return new WaitForSeconds(.5f);
        isCrashToOtherBoat = false;
    }

    IEnumerator player2SetPosToCheckpoint()
    {
        yield return new WaitForSeconds(0.5f);
        rb.simulated = true;
        globalVariable.isPlayerDestroyed = false;
        yield return new WaitForSeconds(.5f);
        globalVariable.playerVisible();
        globalVariable.isRopeVisible = true;
    }
}
