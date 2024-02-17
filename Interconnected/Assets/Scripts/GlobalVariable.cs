using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable globalVariable;


    
    public bool isTriggeredWithObstacle;
    public bool isNotShoot;
    public bool isTimerStart;
    public bool isPlayerSharingLives;



    public int maxDoor;
    public int curDoorOpenValue;

    public int curEnemySpawn;
    public int maxEnemySpawn;

    public string[] playerShieldTagCollision;

    public float waitTimeToShareLives;

    [SerializeField] SpriteRenderer p1Sr;
    [SerializeField] SpriteRenderer p2Sr;

    [SerializeField] CircleCollider2D p1cc;
    [SerializeField] CircleCollider2D p2cc;

    [Header("Tutorial")]
    public int cameraMovementValue;

    float maxDelayTime = 10;

    GameObject player1, player2;

    private void Awake()
    {
        globalVariable = this;
    }
    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    

    private void Update()
    {
        partOfSharingLives();


        partOfTriggerWithObstacle();
      

       

        StartCoroutine(defaultValueCurDoorValue());
    }
    public void delayTimeToShareLives()
    {
        waitTimeToShareLives = maxDelayTime;
    }

    private void partOfTriggerWithObstacle() 
    {
        if (player1 && player2 != null)
        {
            if (!isTriggeredWithObstacle)
            {
                StartCoroutine(ty());

                p1cc.enabled = true;
                p2cc.enabled = true;
                p1Sr.enabled = true;
                p2Sr.enabled = true;
            }
            if(isTriggeredWithObstacle)
            {
                 p1Sr.enabled = false;
                 p2Sr.enabled = false;
                 p1cc.enabled = false;
                 p2cc.enabled = false;

            }
        }
    }

    private void partOfSharingLives() 
    {
        if (waitTimeToShareLives > 0)
        {
            waitTimeToShareLives -= 1 * Time.deltaTime;
        }
        if (waitTimeToShareLives <= 0)
        {
            waitTimeToShareLives = 0;
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


    IEnumerator ty() 
    {
        yield return new WaitForSeconds(.4f);
      
    }
    IEnumerator defaultValueCurDoorValue() 
    {
        
        if (curDoorOpenValue >= 2) 
        {
            yield return new WaitForSeconds(3);
            curDoorOpenValue = 0;
        }
    }
}
