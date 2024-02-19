using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2Health : MonoBehaviour // kurang slow motion
{
    public static Player2Health player2Health;

    [SerializeField] Player2Ability player2Ability;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] LinkRay linkRay;

    public int curPlayer2Health;

    public bool isSharingLivesToP1;

    [SerializeField] Image[] playerHealthImg;

    int maxPlayerHealth = 4;

    [SerializeField] GameObject healthPoint;

    Vector3 maxScalePlayer = new Vector3(.4f, .4f, .4f);
    Vector3 curScalePlayer;

    GameObject player1;

    bool isPlayer1ShareLivesInTutorial = false;

    private void Awake()
    {
        if (player2Health == null) { player2Health = this; }
    }

    private void Start()
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            curPlayer2Health = maxPlayerHealth;
        }
       
       
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        curScalePlayer = transform.localScale;
    }

    private void Update()
    {
        shareLives();
        if (curPlayer2Health <= 0)
        {
            GameOver.gameOver.isGameOver = true;
        }
    }

    private void shareLives()
    {
        for (int i = 0; i < playerHealthImg.Length; i++)
        {

            int clampHealthIndex = Mathf.Max(0, Mathf.Min(i, maxPlayerHealth - 1)); //hitung indeks yang dibatasi dalam rentang 0 hingga maxHealth - 1

            bool isEnableHealthImg = curPlayer2Health >= i + 1;
            playerHealthImg[clampHealthIndex].enabled = isEnableHealthImg;

            if (isSharingLivesToP1)
            {
                transform.localScale = Vector3.Lerp(transform.localScale, maxScalePlayer, 1 * Time.unscaledDeltaTime);
                if (!LinkRay.linkRay.isPlayerLinkedEachOther)
                {
                    isSharingLivesToP1 = false;
                    Time.timeScale = 1;
                }
            }
            else
            {
                if (LevelStatus.levelStatus.levelID == 4 && isPlayer1ShareLivesInTutorial)
                {
                    Tutorial.tutorial.shareLiveProgress = 3;
                    isPlayer1ShareLivesInTutorial = false;

                }
                transform.localScale = Vector3.Lerp(transform.localScale, curScalePlayer, 10 * Time.unscaledDeltaTime);
            }

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
            && !player2Ability.isShielding
            && !player2Ability.isDashing
            && globalVariable.waitTimeToShareLives <= 0) ||
            (LevelStatus.levelStatus.levelID == 4 &&
            Tutorial.tutorial.cameraMoveValue == 2
            && !GameFinish.gameFinish.isGameFinish
            && !Pause.pause.isGamePaused
            && globalVariable.waitTimeToShareLives <= 0
            && Tutorial.tutorial.shareLiveProgress == 2))
        {
            if (context.started && !Player1Health.player1Health.isSharingLivesToP2 && linkRay.isPlayerLinkedEachOther)
            {
                if (curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < maxPlayerHealth)
                {
                    isSharingLivesToP1 = true;
                }
            }
            if (context.performed && linkRay.isPlayerLinkedEachOther && isSharingLivesToP1)
            {
                if (curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < maxPlayerHealth)
                {
                    isPlayer1ShareLivesInTutorial = Tutorial.tutorial.shareLiveProgress == 2;
                    Instantiate(healthPoint, transform.position, Quaternion.identity);
                    globalVariable.delayTimeToShareLives();
                    isSharingLivesToP1 = false;
                    curPlayer2Health--;
                    if (Player1Health.player1Health.curPlayer1Health < maxPlayerHealth && player1 != null)
                    {
                        Player1Health.player1Health.curPlayer1Health++;
                    }
                }
            }
            if (context.canceled && isSharingLivesToP1)
            {
                Time.timeScale = 1;
                isSharingLivesToP1 = false;
            }
        }
    }
}
