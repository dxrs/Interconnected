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

    [SerializeField] Rigidbody2D rbPlayer1;
    [SerializeField] Rigidbody2D rbPlayer2;

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
        if (playerBrakingValue >= 2) 
        {
            if(rbPlayer1 && rbPlayer2 != null) 
            {
                if (rbPlayer1.drag > 3 && rbPlayer2.drag > 3)
                {
                    Destroy(spikeWall, 1);
                    Destroy(brakingTrigger);
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
        

        if (cameraMoveValue == 2) 
        {
            wallBlock[0].SetActive(true);
        }
        if (cameraMoveValue == 3) 
        {
            wallBlock[1].SetActive(true);
        }
    }




}
