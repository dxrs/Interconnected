using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player1Health : MonoBehaviour // kurang slow motion
{
    public static Player1Health player1Health;

    [SerializeField] Player1Movement player1Movement;
    [SerializeField] Player1Ability player1Ability;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] LinkRay linkRay;

    public int curPlayer1Health;

    public bool isSharingLivesToP2;

    [SerializeField] Image[] playerHealthImg;

    [SerializeField] GameObject healthPoint;

    Vector3 maxScalePlayer = new Vector3(.4f, .4f, .4f);
    Vector3 curScalePlayer;

    int maxPlayerHealth = 4;

    GameObject player2;

    private void Awake()
    {
        if (player1Health == null) { player1Health = this; }
    }

    private void Start()
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            curPlayer1Health = maxPlayerHealth;
        }
       

        player2 = GameObject.FindGameObjectWithTag("Player 2");
        curScalePlayer = transform.localScale;
    }

    private void Update()
    {
        shareLives();
        
    }

    private void shareLives() 
    {
        for(int i = 0; i < playerHealthImg.Length; i++) 
        {
            int clampHealthIndex = Mathf.Max(0, Mathf.Min(i, maxPlayerHealth - 1));
            bool isEnableHealthImg = curPlayer1Health >= i + 1;
            playerHealthImg[clampHealthIndex].enabled = isEnableHealthImg;
        }
        if (isSharingLivesToP2) 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, maxScalePlayer, 1 * Time.unscaledDeltaTime);
            if (!LinkRay.linkRay.isPlayerLinkedEachOther) 
            {
                isSharingLivesToP2 = false;
                Time.timeScale = 1;
            }
        }
        else 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, curScalePlayer, 10 * Time.unscaledDeltaTime);
        }
    }

    //input share lives
    public void shareLivesInput(InputAction.CallbackContext context) 
    {
        if ((LevelStatus.levelStatus.levelID != 4 &&
             !globalVariable.isPlayerDestroyed
             && !GameFinish.gameFinish.isGameFinish
             && !GameOver.gameOver.isGameOver
             && !Pause.pause.isGamePaused
             && ReadyToStart.readyToStart.isGameStart
             && !player1Ability.isDashing
             && !Player2Ability.player2Ability.isShielding
             && globalVariable.curShareLivesDelayTime <= 0) ||
             (LevelStatus.levelStatus.levelID == 4 && //<- tutorial
             Tutorial.tutorial.isReadyToShareLives
             && !GameFinish.gameFinish.isGameFinish
             && !Pause.pause.isGamePaused
             && globalVariable.curShareLivesDelayTime <= 0))
        {
            if (context.started && !Player2Health.player2Health.isSharingLivesToP1 && linkRay.isPlayerLinkedEachOther)
            {
                if (curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < maxPlayerHealth)
                {
                    isSharingLivesToP2 = true;
                }
            }
            if (context.performed && linkRay.isPlayerLinkedEachOther && isSharingLivesToP2)
            {
                if (curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < maxPlayerHealth)
                {
                    player1Movement.isMoving = false;
                    Instantiate(healthPoint, transform.position, Quaternion.identity);
                    globalVariable.delayTimeToShareLives();
                    isSharingLivesToP2 = false;
                    curPlayer1Health--;
                    if (Player2Health.player2Health.curPlayer2Health < maxPlayerHealth && player2 != null)
                    {
                        Player2Health.player2Health.curPlayer2Health++;
                    }
                    if (LevelStatus.levelStatus.levelID == 4)
                    {
                        Tutorial.tutorial.shareLivesProgress++;
                    }
                }
            }
            if (context.canceled && isSharingLivesToP2)
            {
                Time.timeScale = 1;
                isSharingLivesToP2 = false;
            }
        }


    }
}
