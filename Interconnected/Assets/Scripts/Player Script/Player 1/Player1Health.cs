using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class Player1Health : MonoBehaviour // kurang slow motion
{
    public static Player1Health player1Health;

    [SerializeField] Player1Ability player1Ability;
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] LinkRay linkRay;

    public int curPlayer1Health;

    public bool isSharingLivesToP2;

    [SerializeField] Image[] playerHealthImg;

    int maxPlayerHealth = 4;

    GameObject player2;

    private void Awake()
    {
        if (player1Health == null) { player1Health = this; }
    }

    private void Start()
    {
        curPlayer1Health = maxPlayerHealth;
        player2 = GameObject.FindGameObjectWithTag("Player 2");
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
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(.4f,.4f,.4f), 10 * Time.unscaledDeltaTime);
        }
        else 
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(.3f, .3f, .3f), 1 * Time.unscaledDeltaTime);
        }
    }

    //input share lives
    public void shareLivesInput(InputAction.CallbackContext context) 
    {
        if(!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart
            && !player1Ability.isDashing
            && !player1Ability.isShielding) 
        {
            if (context.started && !Player2Health.player2Health.isSharingLivesToP1 && linkRay.playerLinkedEachOther) 
            {
 
                if (curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < maxPlayerHealth) 
                {
                    isSharingLivesToP2 = true;
                }
            }
            if (context.performed && linkRay.playerLinkedEachOther && isSharingLivesToP2) 
            {
 
                if (curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < maxPlayerHealth)
                {
                    isSharingLivesToP2 = false;
                    curPlayer1Health--;
                    if (Player2Health.player2Health.curPlayer2Health < maxPlayerHealth && player2 != null)
                    {
                        Player2Health.player2Health.curPlayer2Health++;
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
