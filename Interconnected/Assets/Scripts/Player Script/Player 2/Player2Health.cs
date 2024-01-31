using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player2Health : MonoBehaviour // kurang slow motion
{
    public static Player2Health player2Health;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] LinkRay linkRay;

    public int curPlayer2Health;

    public bool isSharingLivesToP1;

    [SerializeField] Image[] playerHealthImg;

    int maxPlayerHealth = 4;

    GameObject player1;

    private void Awake()
    {
        if (player2Health == null) { player2Health = this; }
    }

    private void Start()
    {
        curPlayer2Health = maxPlayerHealth;
        player1 = GameObject.FindGameObjectWithTag("Player 1");
    }

    private void Update()
    {
        shareLives();
    }

    private void shareLives()
    {
        for (int i = 0; i < playerHealthImg.Length; i++)
        {

            int clampHealthIndex = Mathf.Max(0, Mathf.Min(i, maxPlayerHealth - 1)); //hitung indeks yang dibatasi dalam rentang 0 hingga maxHealth - 1

            bool isEnableHealthImg = curPlayer2Health >= i + 1;
            playerHealthImg[clampHealthIndex].enabled = isEnableHealthImg;
        }
    }

    //input share lives
    public void shareLivesInput(InputAction.CallbackContext context)
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart)
        {
            if (context.started && !Player1Health.player1Health.isSharingLivesToP2 && linkRay.playerLinkedEachOther)
            {
                if (curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < maxPlayerHealth)
                {
                    isSharingLivesToP1 = true;
                }
            }
            if (context.performed && linkRay.playerLinkedEachOther && isSharingLivesToP1)
            {
                if (curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < maxPlayerHealth)
                {
                    curPlayer2Health--;
                    if (Player1Health.player1Health.curPlayer1Health < maxPlayerHealth && player1 != null)
                    {
                        Player1Health.player1Health.curPlayer1Health++;
                    }
                }
            }
            if (context.canceled && isSharingLivesToP1)
            {
                isSharingLivesToP1 = false;
            }
        }
    }
}
