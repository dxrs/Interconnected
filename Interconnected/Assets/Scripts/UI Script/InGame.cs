using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame : MonoBehaviour
{
    public static InGame inGameUI;

    [SerializeField] TextMeshProUGUI textMainObjective;
    [SerializeField] TextMeshProUGUI textShareLivesAvailable;

    [SerializeField] Image imageShareLivesBar;
    [SerializeField] Image imageInnerCircleShareLives;

    [SerializeField] bool isBarFillDecrease;

    [SerializeField] GameObject shareLivesBar;



    private void Awake()
    {
        inGameUI = this;
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        imageInnerCircleShareLives.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            textMainObjective.text = "Collect " + GarbageCollector.garbageCollector.currentGarbageStored + "/" + GarbageCollector.garbageCollector.targetGarbageStored + " Garbages";
        }
        

        if (isBarFillDecrease) 
        {
            imageShareLivesBar.fillAmount = 1 - (GlobalVariable.globalVariable.curShareLivesDelayTime / 10f);
            imageInnerCircleShareLives.color = Color.Lerp(imageInnerCircleShareLives.color, new Color(1, 1, 1, 0), 5 * Time.deltaTime);

        }
        else
        {
            if(LevelStatus.levelStatus.levelID != 4) 
            {
                if (Player1Health.player1Health.curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < 4 || Player2Health.player2Health.curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < 4)
                {
                    shareLivesBar.SetActive(true);
                    imageInnerCircleShareLives.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
                    imageInnerCircleShareLives.color = Color.Lerp(imageInnerCircleShareLives.color, new Color(1,1,1,1), 5 * Time.deltaTime);
                }
                else
                {
                    shareLivesBar.SetActive(false);
                }
            }
            else 
            {
                if (Player1Health.player1Health.curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < 4 || Player2Health.player2Health.curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < 4)
                {
                    
                    
                    imageInnerCircleShareLives.color = Color.Lerp(imageInnerCircleShareLives.color, new Color(1, 1, 1, 1), 5 * Time.deltaTime);
                }
            }
            
        }
        if (GlobalVariable.globalVariable.curShareLivesDelayTime > 0) 
        {
            isBarFillDecrease = true;
        }
        if (GlobalVariable.globalVariable.curShareLivesDelayTime <= 0) 
        {
            isBarFillDecrease = false;
        }
    }

   

   
}
