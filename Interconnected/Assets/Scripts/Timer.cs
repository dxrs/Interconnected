using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] SceneSystem sceneSystem;

    [SerializeField] TextMeshProUGUI textCurrentTimer;
    [SerializeField] TextMeshProUGUI textPlayerTimer;
    [SerializeField] TextMeshProUGUI[] textTargetTimer;

    [SerializeField] float curTimerValue;
    [SerializeField] float[] timerTargetValue;

    bool isTimerStop = false;

    TimeSpan timerCount;

    private void Start()
    {
        StartCoroutine(timerCountUp());
        StartCoroutine(timerCountDown());
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID == 1)
        {
            for (int i = 0; i < textTargetTimer.Length && i < timerTargetValue.Length; i++)
            {
                float targetTimer = timerTargetValue[i];
                TimeSpan targetTimeSpan = TimeSpan.FromSeconds(targetTimer);
                string targetTimerString = targetTimeSpan.ToString("mm':'ss':'ff");
                textTargetTimer[i].text = targetTimerString;
            }
            if (globalVariable.isGameFinish)
            {
                float playerTimerValue = curTimerValue;
                TimeSpan playerTimeSpan = TimeSpan.FromSeconds(playerTimerValue);
                string playerTimer = playerTimeSpan.ToString("mm':'ss':'ff");
                textPlayerTimer.text = playerTimer;
            }
            if (curTimerValue >= 86400) //24jam
            {
                curTimerValue = 0;
                isTimerStop = true;
                globalVariable.isGameOver = true;
            }
        }
       
        
    }
    IEnumerator timerCountUp() 
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
                        curTimerValue += Time.deltaTime;
                        timerCount = TimeSpan.FromSeconds(curTimerValue);
                        string timer = timerCount.ToString("mm':'ss':'ff");
                        textCurrentTimer.text = timer;

                    }
                }
            }
            yield return null;
        }
    }
    IEnumerator timerCountDown()
    {
        while (true)
        {
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
