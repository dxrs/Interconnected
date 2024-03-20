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
        if(LevelStatus.levelStatus.levelID != 4) 
        {
            textMainObjective.text = "Collect " + GarbageCollector.garbageCollector.currentGarbageStored + "/" + GarbageCollector.garbageCollector.targetGarbageStored + " Garbages";
        }
        

        if (isBarFillDecrease) 
        {
            imageShareLivesBar.fillAmount = GlobalVariable.globalVariable.curShareLivesDelayTime / 10f;

            if (imageShareLivesBar.fillAmount <= 0)
            {
                imageShareLivesBar.fillAmount = 1;

            }
        }
        if(!isBarFillDecrease) 
        {
            
            

            if (Player1Health.player1Health.curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < 4 || Player2Health.player2Health.curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < 4) 
            {
                shareLivesBar.SetActive(true);
            }
            else 
            {
                shareLivesBar.SetActive(false);
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
