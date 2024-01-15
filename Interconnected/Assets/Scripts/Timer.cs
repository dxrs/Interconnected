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
    [SerializeField] TextMeshProUGUI[] textTargetTimer;

    [SerializeField] float curTimerValue;
    [SerializeField] float[] timerTargetValue;

    bool isTimerStop = false;

    TimeSpan timerCount;

    private void Start()
    {
        for (int i = 0; i < textTargetTimer.Length && i < timerTargetValue.Length; i++)
        {
            float targetTimer = timerTargetValue[i];
            TimeSpan targetTimeSpan = TimeSpan.FromSeconds(targetTimer);
            string targetTimerString = targetTimeSpan.ToString("mm':'ss':'ff");
            textTargetTimer[i].text = targetTimerString;
        }
        StartCoroutine(timerStartCount());
    }

    private void Update()
    {
        if (curTimerValue >= 86400) //24jam
        {
            curTimerValue = 0;
            isTimerStop = true;
            globalVariable.isGameOver = true;
        }
    }
    IEnumerator timerStartCount() 
    {
        while (true) 
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
            
            yield return null;
        }
    }
}
