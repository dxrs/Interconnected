using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable globalVariable;


    public bool isPlayerDestroyed;
    public bool isTimerStart;
    public bool isPlayerSharingLives;
    public bool isRopeVisible;
    public bool isCameraBoundariesActive;
    public bool[] isDoorButtonPressed;

    public int maxDoor;
    

    public int curEnemySpawn;
    public int maxEnemySpawn;

    public string[] playerShieldTagCollision;

    public float curShareLivesDelayTime;

    [Range(0.1f,1)]
    public float deltaTimeValueShareLives;

    [SerializeField] SpriteRenderer spriteRendererPlayer1;
    [SerializeField] SpriteRenderer spriteRendererPlayer2;

    [SerializeField] PolygonCollider2D player1Collider;
    [SerializeField] PolygonCollider2D player2Collider;


    float maxShareLivesDelayTime = 10;

    GameObject player1, player2;

    [SerializeField] bool isAddingCheckpointValue = false;

    private void Awake()
    {
        globalVariable = this;
    }
    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        isRopeVisible = true;
    }

    

    private void Update()
    {
        if (isPlayerDestroyed) 
        {
            for(int j = 0; j < isDoorButtonPressed.Length; j++) 
            {
                isDoorButtonPressed[j] = false;
            }
        }
        if (isDoorButtonPressed[0] && isDoorButtonPressed[1] && !isAddingCheckpointValue) 
        {
            //Checkpoint.checkpoint.curCheckpointValue++;
            if (LevelStatus.levelStatus.levelID == 4) 
            {
                //Checkpoint.checkpoint.curCheckpointValue++;
                Tutorial.tutorial.tutorialProgress++;
                isAddingCheckpointValue = true;
            }
            
        }
        partOfSharingLives();
        StartCoroutine(setDefaultValueCurDoorValue());
       
    }
    public void delayTimeToShareLives()
    {
        curShareLivesDelayTime = maxShareLivesDelayTime;
    }

    public void colliderInactive() 
    {
        player1Collider.isTrigger = true;
        player2Collider.isTrigger = true;
    }

    public void colliderActive() 
    {
        player1Collider.isTrigger = false;
        player2Collider.isTrigger = false;
    }

    public void playerVisible() 
    {
        if(player1 && player2 != null) 
        {
            player1Collider.enabled = true;
            player2Collider.enabled = true;
            spriteRendererPlayer1.enabled = true;
            spriteRendererPlayer2.enabled = true;
        }
       
    }
    public void playerInvisible()
    {
        if (player1 && player2 != null)
        {
            player1Collider.enabled = false;
            player2Collider.enabled = false;
            spriteRendererPlayer1.enabled = false;
            spriteRendererPlayer2.enabled = false;

        }
       
    }

    private void partOfSharingLives() 
    {
        if (curShareLivesDelayTime > 0)
        {
            curShareLivesDelayTime -= deltaTimeValueShareLives * Time.deltaTime;
        }
        if (curShareLivesDelayTime <= 0)
        {
            curShareLivesDelayTime = 0;
        }

        if (Player1Health.player1Health.isSharingLivesToP2 || Player2Health.player2Health.isSharingLivesToP1)
        {
            isPlayerSharingLives = true;
        }
        else { isPlayerSharingLives = false; }
        if (!Pause.pause.isGamePaused)
        {
            if (Player1Health.player1Health.isSharingLivesToP2 || Player2Health.player2Health.isSharingLivesToP1)
            {
                Time.timeScale = 0.5f;
            }

        }
    }

    IEnumerator setDefaultValueCurDoorValue() 
    {
        if (isDoorButtonPressed[0] && isDoorButtonPressed[1]) 
        {
            yield return new WaitForSeconds(1);
            for (int j = 0; j < isDoorButtonPressed.Length; j++)
            {
                isDoorButtonPressed[j] = false;
            }
            isAddingCheckpointValue = false;
        }
    }
   
}
