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
