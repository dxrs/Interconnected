using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player2Collision : MonoBehaviour
{
    public static Player2Collision player2Collision;

    public bool isCrashToOtherBoat;
    public bool isHitCameraBound;
    public bool isHitDoorButton;
    public bool isHitGravityArea;

    [SerializeField] string[] playerDestroyCollision;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player2Movement player2Movement;
    [SerializeField] Player2Ability player2Ability;
    [SerializeField] Player2Health player2Health;

    [SerializeField] ParticleSystem playerHitParticle;
    [SerializeField] float crashForceValue;

    [SerializeField] Rigidbody2D rb;

    private void Awake()
    {
        if (player2Collision == null) { player2Collision = this; }
    }

    private void Update()
    {
        if (globalVariable.isPlayerDestroyed) 
        {
            rb.simulated = false; //bug nanti diperbaiki note
            player2Movement.isMoving = false;
            player2Movement.isBraking = true;
        }
        else 
        {
            rb.simulated = true;
        }

        if(globalVariable.isPlayerDestroyed)
        {
            if(isHitDoorButton)
            {
                isHitDoorButton=false;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handleDumpPointCollision(collision);
        handleObstacleCollision(collision);
        handleOutlineColliderCollision(collision);
        handleCameraBoundCollision(collision);
        handleDoorButtonCollision(collision);
        handleEnemyCollision(collision);
        handleFinishPointCollision(collision);
        if(collision.gameObject.tag=="Garbage Area") 
        {
            if (LevelStatus.levelStatus.levelID == 4) 
            {
                Tutorial.tutorial.isPlayersEnterGarbageArea[0] = true;
            }
        }

        if (collision.gameObject.tag == "Obstacle P1")
        {
            isHitGravityArea = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue--;

        if (collision.gameObject.tag == "Camera Boundaries")
            isHitCameraBound = false;

        if (collision.gameObject.tag == "Garbage Center Point")
            GarbageCollector.garbageCollector.playerReadyToStoreValue[1] = 0;

        if (collision.gameObject.tag == "Door Button")
        {
            isHitDoorButton = false;
            globalVariable.isDoorButtonPressed[1] = false;
        }

        if (collision.gameObject.tag == "Obstacle P1")
        {
            isHitGravityArea = false;
        }
    }

    private void handleObstacleCollision(Collider2D collision)
    {
        for(int j = 0; j < playerDestroyCollision.Length; j++) 
        {
            if (collision.gameObject.CompareTag(playerDestroyCollision[j])) 
            {
                if (!player2Ability.isShielding)
                {
                    CameraShaker.Instance.ShakeOnce(8, 4, 0.1f, 1f);
                    LinkRay.linkRay.isPlayerLinkedEachOther = false;
                    if (LevelStatus.levelStatus.levelID != 4)
                        player2Health.curPlayer2Health--;



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
       
    }

    private void handleDumpPointCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Garbage Center Point")
            GarbageCollector.garbageCollector.playerReadyToStoreValue[1] = 1;
    }
    private void handleOutlineColliderCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1 Outline Collider")
            player2BouncedCollision(collision);
    }

    private void handleCameraBoundCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Camera Boundaries")
            isHitCameraBound = true;
    }

    private void handleDoorButtonCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door Button")
        {
            isHitDoorButton = true;
            globalVariable.isDoorButtonPressed[1] = true;
        }
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
        float adjustedCrashForce = player2Movement.isBraking ? crashForceValue * 0.5f : crashForceValue;

        if (!isHitDoorButton)
        {
            Vector2 backwardMovePos = (transform.position - collider.transform.position).normalized;
            rb.AddForce(backwardMovePos * adjustedCrashForce, ForceMode2D.Impulse); // sedang konflik dgn void playerMovement
        }
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
        globalVariable.isPlayerDestroyed = false;
        yield return new WaitForSeconds(.5f);
        globalVariable.playerVisible();
        globalVariable.isRopeVisible = true;
    }
}
