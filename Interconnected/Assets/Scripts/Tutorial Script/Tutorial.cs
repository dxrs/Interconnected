using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial tutorial;

    [SerializeField] GameObject spikeWall;
    [SerializeField] GameObject shareLivesWall;
    [SerializeField] GameObject brakingTrigger;
    [SerializeField] GameObject[] wallBlock;
    [SerializeField] GameObject statusPlayerConnect;


    [SerializeField] Rigidbody2D rbPlayer1;
    [SerializeField] Rigidbody2D rbPlayer2;

    [SerializeField] TextMeshProUGUI[] textShareLivesPlayer;
    [SerializeField] TextMeshProUGUI textShareLivesCount;
    [SerializeField] TextMeshProUGUI textPlayerConnected;
    [SerializeField] TextMeshProUGUI textGarbageCollected;

    public bool isPlayerCanShareLives;

    public int playerBrakingValue;
    public int cameraMoveValue;
    public int shareLiveProgress;

    private void Awake()
    {
        tutorial = this;
    }
    private void Start()
    {
        cameraMoveValue = 1;
        for(int j = 0; j < wallBlock.Length; j++) 
        {
            wallBlock[j].SetActive(false);
        }
    }

    private void Update()
    {
        textGarbageCollected.text = GarbageCollector.garbageCollector.currentGarbageStored.ToString() + "/5";
        if (LinkRay.linkRay.isPlayerLinkedEachOther)
        {
            textPlayerConnected.text = "Connected";
        }
        else { textPlayerConnected.text = "Not Connect"; }
        
        if (playerBrakingValue >= 2) 
        {
            if(rbPlayer1 && rbPlayer2 != null) 
            {
                if (rbPlayer1.drag > 4.9f && rbPlayer2.drag > 4.9f)
                {
                    Destroy(spikeWall);
                    Destroy(brakingTrigger,0.5f);
                }
            }
           
        }

        if (GlobalVariable.globalVariable.isPlayerDestroyed) 
        {
            playerBrakingValue = 0;
        }

        
        if (shareLiveProgress == 1)
        {
            Destroy(shareLivesWall, 1);
        }

        if (isPlayerCanShareLives) 
        {
            textShareLivesCount.text = Mathf.RoundToInt(GlobalVariable.globalVariable.waitTimeToShareLives).ToString();
            textShareLivesCount.enabled = true;
            if (Player1Health.player1Health.curPlayer1Health == 1) 
            {
                if (GlobalVariable.globalVariable.waitTimeToShareLives <= 0) 
                {
                    textShareLivesPlayer[0].enabled = false;
                    textShareLivesPlayer[1].enabled = true;
                }
               
            }
            if (Player2Health.player2Health.curPlayer2Health == 1)
            {
                if (GlobalVariable.globalVariable.waitTimeToShareLives <= 0)
                {
                    textShareLivesPlayer[1].enabled = false;
                    textShareLivesPlayer[0].enabled = true;
                }
               
            }
        }

        /*
  if (shareLiveProgress == 2) 
  {

      textShareLivesPlayer2.enabled = true;
      textShareLivesCount.enabled = true;
      textShareLivesCount.text = "Share Lives available in " +
          Mathf.RoundToInt(GlobalVariable.globalVariable.waitTimeToShareLives).ToString();
  }
  else { textShareLivesCount.enabled = false; }
      */


        if (cameraMoveValue >= 2) 
        {

            StartCoroutine(wallBlocker1Active());

        }
        if (cameraMoveValue >= 3) 
        {
            wallBlock[1].SetActive(true);
            //statusPlayerConnect.transform.localPosition = new Vector2(57, -15.71f);
        }
        if (cameraMoveValue >= 4) 
        {
            wallBlock[2].SetActive(true);
        }

        
    }

    public void onClickNextScene() 
    {
        SceneSystem.sceneSystem.isNextScene = true;
    }

    IEnumerator wallBlocker1Active() 
    {
        yield return new WaitForSeconds(1f);
        wallBlock[0].SetActive(true);
    }
}
