using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial tutorial;

    public int tutorialProgress;
    public int shareLivesProgress;

    public bool isEnemyReadyToShoot;
    public bool isReadyToShareLives;
    public bool[] isPlayersEnterGarbageArea;

    [SerializeField] GameObject wallBlocker;
    [SerializeField] GameObject wallStatic;
    [SerializeField] GameObject garbages;
    [SerializeField] GameObject shareLivesProgressObject;
    [SerializeField] TextMeshProUGUI textShareLivesCount;
    [SerializeField] TextMeshProUGUI textMainObjective;

    GameObject player1, player2;

    private void Awake()
    {
        tutorial = this;
    }

    private void Start()
    {
        tutorialProgress = 1;
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        shareLivesProgressObject.SetActive(false);
    }

    private void Update()
    {
        if (isReadyToShareLives) 
        {
            
            if (tutorialProgress <= 2) 
            {
                textShareLivesCount.enabled = true;
            }
 
           
            textShareLivesCount.text = shareLivesProgress.ToString();
        }

        progress1();
        progress2();
        progress3();
        
       
    }

    private void progress1() 
    {
        if (tutorialProgress == 1)
        {
            if (!DialogueManager.dialogueManager.isDialogueActive)
            {
                textMainObjective.text = "Walk to the white object";
            }
        }
    }

    private void progress2() 
    {
        if (tutorialProgress == 2)
        {
            if (!isReadyToShareLives && shareLivesProgress < 2)
            {
                textMainObjective.text = "";
            }
            if (isReadyToShareLives)
            {
                shareLivesProgressObject.SetActive(true);
               
                if (shareLivesProgress < 2)
                {
                    if (DialogueManager.dialogueManager.curTextValue >= 14)
                    {
                        if (Player1Health.player1Health.curPlayer1Health == 2)
                        {
                            textMainObjective.text = "Player 1 Hold F To Share Lives";
                        }
                        if (Player2Health.player2Health.curPlayer2Health == 2)
                        {
                            textMainObjective.text = "Player 2 Hold Button Y To Share Lives";
                        }
                    }

                }
                for (int j = 0; j < isPlayersEnterGarbageArea.Length; j++)
                {
                    if (shareLivesProgress >= 2 && !isPlayersEnterGarbageArea[j])
                    {
                        textMainObjective.text = "Done";
                    }

                }

            }
            if (player1.transform.position.x >= 7.5f && player2.transform.position.x >= 7.5f)
            {
                isEnemyReadyToShoot = true;
            }
            if (shareLivesProgress >= 2)
            {
                Destroy(wallStatic);
                isReadyToShareLives = false;
            }

        }
    }

    private void progress3() 
    {
        if (tutorialProgress == 3)
        {
            if (DialogueManager.dialogueManager.curTextValue < 21)
            {
                textMainObjective.text = "";
            }
            textShareLivesCount.enabled = false;
            wallBlocker.SetActive(true);
            shareLivesProgressObject.SetActive(false);
            if (DialogueManager.dialogueManager.curTextValue >= 19)
            {
                garbages.SetActive(true);
            }
            if (DialogueManager.dialogueManager.curTextValue >= 21 && !DialogueManager.dialogueManager.isDialogueActive)
            {
                textMainObjective.text = "Collect " + GarbageCollector.garbageCollector.currentGarbageStored + "/" + GarbageCollector.garbageCollector.targetGarbageStored + " Garbages";
            }


        }
    }

    public void onButtonContinuePressed() 
    {
        SceneSystem.sceneSystem.isNextScene = true;
        LevelManager.levelManager.saveDataCurrentLevel();
    }
    
}
