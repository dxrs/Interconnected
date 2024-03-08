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
    [SerializeField] GameObject[] shareLivesProgressObject;

    [SerializeField] TextMeshProUGUI textWaitShareLives;
    [SerializeField] TextMeshProUGUI textShareLivesCount;
    [SerializeField] TextMeshProUGUI[] textGarbages;

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
        for (int j = 0; j < shareLivesProgressObject.Length; j++)
        {
            shareLivesProgressObject[j].SetActive(false);
        }
    }

    private void Update()
    {
        if (isReadyToShareLives) 
        {
            if (shareLivesProgress >= 1) 
            {
                textWaitShareLives.enabled = true;
            }
            if (tutorialProgress <= 2) 
            {
                textShareLivesCount.enabled = true;
            }
 
            textWaitShareLives.text = Mathf.RoundToInt(GlobalVariable.globalVariable.curShareLivesDelayTime).ToString();
            textShareLivesCount.text = shareLivesProgress.ToString();
        }
        else 
        {
            
            textWaitShareLives.enabled = false;
        }
        if (tutorialProgress == 2) 
        {
            if (isReadyToShareLives) 
            {
                for (int j = 0; j < shareLivesProgressObject.Length; j++)
                {
                    shareLivesProgressObject[j].SetActive(true);
                }
            }
            if(player1.transform.position.x >= 7.5f && player2.transform.position.x >= 7.5f) 
            {
                isEnemyReadyToShoot = true;
            }
            if (shareLivesProgress >= 2) 
            {
                Destroy(wallStatic);
                isReadyToShareLives = false;
            }
            
        }
        if (tutorialProgress >= 3) 
        {
            textShareLivesCount.enabled = false;
            wallBlocker.SetActive(true);
            for(int j = 0; j < shareLivesProgressObject.Length; j++) 
            {
                shareLivesProgressObject[j].SetActive(false);
            }
            if (DialogueManager.dialogueManager.curTextValue >= 21 && !DialogueManager.dialogueManager.isDialogueActive) 
            {
                for (int j = 0; j < textGarbages.Length; j++)
                {
                    textGarbages[j].enabled = true;
                }
            }
            
            
        }
    }

    public void onButtonContinuePressed() 
    {
        SceneSystem.sceneSystem.isNextScene = true;
    }
    
}
