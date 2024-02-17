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

    [SerializeField] TextMeshProUGUI textShareLivesPlayer2;
    [SerializeField] TextMeshProUGUI textShareLivesCount;
    [SerializeField] TextMeshProUGUI textPlayerConnected;
    [SerializeField] TextMeshProUGUI textGarbageCollected;

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
        shareLiveProgress = 1;
    }

    private void Update()
    {
        textGarbageCollected.text = GarbageCollector.garbageCollector.garbageCollected.ToString() + "/5";
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

        if (GlobalVariable.globalVariable.isTriggeredWithObstacle) 
        {
            playerBrakingValue = 0;
        }

        
        if (shareLiveProgress == 3)
        {
            Destroy(shareLivesWall, 1);
        }
        if (shareLiveProgress == 2) 
        {
            textShareLivesPlayer2.enabled = true;
            textShareLivesCount.enabled = true;
            textShareLivesCount.text = "Share Lives available in " +
                Mathf.RoundToInt(GlobalVariable.globalVariable.waitTimeToShareLives).ToString();
        }
        else { textShareLivesCount.enabled = false; }
        

        if (cameraMoveValue >= 2) 
        {
            wallBlock[0].SetActive(true);
            

        }
        if (cameraMoveValue == 3) 
        {
            wallBlock[1].SetActive(true);
            statusPlayerConnect.transform.localPosition = new Vector2(57, -15.71f);
        }

        
    }

    public void onClickNextScene() 
    {
        SceneSystem.sceneSystem.isNextScene = true;
    }


}
