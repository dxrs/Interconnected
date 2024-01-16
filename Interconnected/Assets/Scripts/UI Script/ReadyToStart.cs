using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReadyToStart : MonoBehaviour
{
    public static ReadyToStart readyToStart;

    public bool isGameStart;

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

       
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID == 1)
        {
            panelTimer.SetActive(true);
            panelStar.SetActive(true);
        }
        if (LevelStatus.levelStatus.levelID == 2) 
        {
            panelStar.SetActive(true);
            panelTargetEnemyDestroy.SetActive(true);
        }
        if (isGameStart) 
        {
            inGameUI.SetActive(true);
            startUI.SetActive(false);
        }
    }

    public void onClickStart() 
    {
        isGameStart = true;
    }
}
