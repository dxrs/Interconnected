using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyToStart : MonoBehaviour
{
    public static ReadyToStart readyToStart;

    public bool isGameStart;

    [SerializeField] float timerCountToStart;

    [SerializeField] TextMeshProUGUI textTimerCountToStart;

    [SerializeField] GameObject startUI;
    [SerializeField] GameObject inGameUI;

 
    private void Awake()
    {
        readyToStart = this;
    }

    private void Start()
    {
        inGameUI.SetActive(false);
        startUI.SetActive(true);
        timerCountToStart = 3;
       
    }

    private void Update()
    {
        
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            StartCoroutine(waitToCount());
            StartCoroutine(setTextTimer());

            if (timerCountToStart > 1)
            {
                textTimerCountToStart.text = Mathf.RoundToInt(timerCountToStart).ToString();
            }
            if (isGameStart && !GlobalVariable.globalVariable.isGameFinish)
            {
                inGameUI.SetActive(true);
                startUI.SetActive(false);
            }
        }
        else 
        {
            isGameStart = true;
            inGameUI.SetActive(true);
            startUI.SetActive(false);
        }
      
    }

    IEnumerator waitToCount() 
    {
        yield return new WaitForSeconds(1);
        
        if (timerCountToStart > 0)
        {

            timerCountToStart -= 1 * Time.deltaTime;
        }
        if (timerCountToStart <= 0) 
        {
            yield return new WaitForSeconds(1f);
            isGameStart = true;
        }
    }
    IEnumerator setTextTimer() 
    {
        
        if (timerCountToStart <= 1)
        {
            yield return new WaitForSeconds(1);
            textTimerCountToStart.text = "Let's go !";
        }
    }

    
}
