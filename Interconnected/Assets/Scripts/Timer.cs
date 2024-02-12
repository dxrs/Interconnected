using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    public static Timer timerInstance;


    public float curTimerValue;

    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] TextMeshProUGUI textCurrentTimer;

    bool isTimerStop = false;

    TimeSpan timerCount;

    private void Awake()
    {
        timerInstance = this;
    }

    private void Start()
    {
        
        StartCoroutine(timerCountDown());
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            if (curTimerValue <= 0)
            {
                curTimerValue = 0;
                //globalVariable.isGameFinish = true;
            }
        }
      
        
       
        
    }
    
    IEnumerator timerCountDown()
    {
        while (true)
        {
            if (LevelStatus.levelStatus.levelID == 1) 
            {
                if (ReadyToStart.readyToStart.isGameStart
                && !globalVariable.isGameFinish
                && !globalVariable.isGameOver)
                {
                    if (!isTimerStop)
                    {
                        if (curTimerValue > 0)
                        {
                            curTimerValue -= Time.deltaTime;
                        }

                        timerCount = TimeSpan.FromSeconds(curTimerValue);
                        string timer = timerCount.ToString("mm':'ss':'ff");
                        textCurrentTimer.text = timer;

                    }
                }
            }
            if (LevelStatus.levelStatus.levelID == 2) 
            {
                if (ReadyToStart.readyToStart.isGameStart
               && !globalVariable.isGameFinish
               && !globalVariable.isGameOver
               && globalVariable.isTimerStart)
                {
                    if (!isTimerStop)
                    {
                        if (curTimerValue > 0)
                        {
                            curTimerValue -= Time.deltaTime;
                        }

                        timerCount = TimeSpan.FromSeconds(curTimerValue);
                        string timer = timerCount.ToString("mm':'ss':'ff");
                        textCurrentTimer.text = timer;

                    }
                }
            }
            
            
            yield return null;
        }
    }
}
