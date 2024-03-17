using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InGame : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI textMainObjective;
    [SerializeField] TextMeshProUGUI textShareLivesAvailable;

    [SerializeField] Image imageShareLivesBar;

    [SerializeField] bool isBarFillDecrease;

    [SerializeField] float barScaleX_Max;

    private void Start()
    {
        imageShareLivesBar.enabled = false;
    }
    private void Update()
    {
        if(LevelStatus.levelStatus.levelID != 4) 
        {
            textMainObjective.text = "Collect " + GarbageCollector.garbageCollector.currentGarbageStored + "/" + GarbageCollector.garbageCollector.targetGarbageStored + " Garbages";
        }
        

        if (isBarFillDecrease) 
        {
            textShareLivesAvailable.enabled = false;
            imageShareLivesBar.enabled = true;
            imageShareLivesBar.transform.localScale = Vector2.MoveTowards(imageShareLivesBar.transform.localScale, new Vector2(0, .2f), GlobalVariable.globalVariable.deltaTimeValueShareLives / 2 * Time.deltaTime);
        }
        else 
        {
            imageShareLivesBar.enabled = false;
            imageShareLivesBar.transform.localScale = new Vector2(barScaleX_Max, .2f);

            if(Player1Health.player1Health.curPlayer1Health > 1 && Player2Health.player2Health.curPlayer2Health < 4 || Player2Health.player2Health.curPlayer2Health > 1 && Player1Health.player1Health.curPlayer1Health < 4) 
            {
                textShareLivesAvailable.enabled = true;
            }
            else 
            {
                textShareLivesAvailable.enabled = false;
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
