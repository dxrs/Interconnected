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

    [SerializeField] float curTimerValue;
    [SerializeField] float[] timerTargetValue;

    bool isTimerStop = false;

    TimeSpan timerCount;

    private void Start()
    {
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
            if (!isTimerStop) 
            {
                if (!globalVariable.isGameFinish || !globalVariable.isGameOver
                    || !sceneSystem.isRestartScene || !sceneSystem.isExitScene)
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
