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

    [SerializeField] GameObject panelTimer;
    [SerializeField] GameObject panelTargetEnemyDestroy;
    [SerializeField] GameObject panelStar;

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
        StartCoroutine(waitToCount());

        if (timerCountToStart <= 0) 
        {
            textTimerCountToStart.text = "Let's go !";
        }
        if (LevelStatus.levelStatus.levelID == 1)
        {
            //panelTimer.SetActive(true);
            //panelStar.SetActive(true);
        }
        if (LevelStatus.levelStatus.levelID == 2) 
        {
           // panelStar.SetActive(true);
           // panelTargetEnemyDestroy.SetActive(true);
        }
        if (isGameStart && !GlobalVariable.globalVariable.isGameFinish) 
        {
            inGameUI.SetActive(true);
            startUI.SetActive(false);
        }
    }

    IEnumerator waitToCount() 
    {
        yield return new WaitForSeconds(1);
        if (timerCountToStart > 0)
        {
            textTimerCountToStart.text = Mathf.RoundToInt(timerCountToStart).ToString();
            timerCountToStart -= 1 * Time.deltaTime;
        }
        if (timerCountToStart <= 0) 
        {
            yield return new WaitForSeconds(.5f);
            isGameStart = true;
        }
    }

    public void onClickStart() 
    {
        //isGameStart = true;
    }
}
