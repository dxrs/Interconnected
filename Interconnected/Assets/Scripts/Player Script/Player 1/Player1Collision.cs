using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Player1Collision : MonoBehaviour
{
    public static Player1Collision player1Collision;

    public bool isCrashToOtherBoat;
    public bool isStopAtCameraTrigger;
    public bool isHitDorrButton;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] Player1Movement player1Movement;
    [SerializeField] Player1Ability player1Ability;
    [SerializeField] Player1Health player1Health;

    [SerializeField] ParticleSystem playerHitParticle;
    [SerializeField] float crashForceValue;
    

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
        if (globalVariable.isPlayerDestroyed) 
        {
            rb.simulated = false; //bug nanti diperbaiki note
            player1Movement.isMoving = false;
            player1Movement.isBraking = true;
        }
        else 
        {
            rb.simulated = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        handleDumpPointCollision(collision);
        handleObstacleCollision(collision);
        handlePullUpObjectCollision(collision);
        handleOutlineColliderCollision(collision);
        handleCameraMoveTriggerCollision(collision);
        handleDoorButtonCollision(collision);
        handleEnemyCollision(collision);
        handleFinishPointCollision(collision);

        if (collision.gameObject.tag == "Camera Boundaries") 
        {
            Debug.Log("kena");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Camera Boundaries")
        {
            Debug.Log("tidak kena");
        }
        if (collision.gameObject.tag == "Player Pull Up Object")
            player1Ability.isPlayer1SetPosToPullUpObject = false;

        if (collision.gameObject.tag == "Camera Move Trigger")
            isStopAtCameraTrigger = false;

        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue--;

        if (collision.gameObject.tag == "Garbage Center Point")
            GarbageCollector.garbageCollector.playerReadyToStoreValue[0] = 0;

        if (collision.gameObject.tag == "Door Button") 
        {
            isHitDorrButton = false;
            globalVariable.isDoorButtonPressed[0] = false;
        }
            
    }

    private void handleObstacleCollision(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("Gun Bullet"))
        {
            if (!Player2Ability.player2Ability.isShielding)
            {
                CameraShaker.Instance.ShakeOnce(8, 4, 0.1f, 1f);
                if (LevelStatus.levelStatus.levelID != 4) 
                    player1Health.curPlayer1Health--;

                
                globalVariable.playerInvisible();
                if (player1Health.curPlayer1Health >= 1) 
                {
                    globalVariable.isRopeVisible = false;
                    globalVariable.isPlayerDestroyed = true;
                    StartCoroutine(player1SetPosToCheckpoint());
                }

                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
                
            }
            else
            {
                if (!globalVariable.isPlayerDestroyed)
                    player1BoucedCollision(collision);
            }
        }
    }

    private void handleDumpPointCollision(Collider2D collision) 
    {
        if (collision.gameObject.tag == "Garbage Center Point")
            GarbageCollector.garbageCollector.playerReadyToStoreValue[0] = 1;
    }

    private void handlePullUpObjectCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player Pull Up Object")
            player1Ability.isPlayer1SetPosToPullUpObject = true;
    }

    private void handleOutlineColliderCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 2 Outline Collider")
            player1BoucedCollision(collision);
    }

    private void handleCameraMoveTriggerCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Camera Move Trigger")
            isStopAtCameraTrigger = true;
    }

    private void handleDoorButtonCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door Button") 
        {
            isHitDorrButton = true;
            globalVariable.isDoorButtonPressed[0] = true;
        }
      
    }

    private void handleEnemyCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (!Player2Ability.player2Ability.isShielding)
            {
                player1Health.curPlayer1Health--;
                Instantiate(playerHitParticle, transform.position, Quaternion.identity);
            }
            else
            {
                if (!globalVariable.isPlayerDestroyed)
                    player1BoucedCollision(collision);
            }
        }
    }

    private void handleFinishPointCollision(Collider2D collision)
    {
        if (collision.gameObject.tag == "Finish Point")
            GameFinish.gameFinish.finishValue++;
    }

   

    public void player1BoucedCollision(Collider2D collider)
    {
        StartCoroutine(playerCrash());

        // Mengurangi dampak pantulan jika isBrakingWithInput aktif
        float adjustedCrashForce = player1Movement.isBrakingWithInput ? crashForceValue * 0.5f : crashForceValue;
        //Debug.Log($"crashForceValue: {crashForceValue}, adjustedCrashForce: {adjustedCrashForce}");

        if (!isHitDorrButton) 
        {
            Vector2 backwardMovePos = (transform.position - collider.transform.position).normalized;
            rb.AddForce(backwardMovePos * adjustedCrashForce, ForceMode2D.Impulse); // sedang konflik dgn void playerMovement
        }
       
    }

    IEnumerator playerCrash()
    {
        player1Movement.isBraking = false;
        player1Movement.isBrakingWithInput = false;
        isCrashToOtherBoat = true;
        yield return new WaitForSeconds(.5f);
        isCrashToOtherBoat = false;
        
    }

    IEnumerator player1SetPosToCheckpoint()
    {
        yield return new WaitForSeconds(0.5f);
        globalVariable.isPlayerDestroyed = false;
        yield return new WaitForSeconds(.5f);
        globalVariable.playerVisible();
        globalVariable.isRopeVisible = true;

    }
}
