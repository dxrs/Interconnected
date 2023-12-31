using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SurvivalArea : MonoBehaviour
{
    public GameObject enemySpawner;
    public GameObject[] doorBlocker;

    public bool isStartingToSurvive;

    [SerializeField] GameObject doorEnter;
    [SerializeField] GameObject doorExit;
    [SerializeField] GameObject[] enterDoorButton;

    [SerializeField] bool isExitDoorOpen;
    [SerializeField] bool isEnterMovePosX;
    [SerializeField] bool isEnterMovePosY;
    [SerializeField] bool isExitMovePosX;
    [SerializeField] bool isExitMovePosY;

    [SerializeField] float countDownTimer;
    [SerializeField] Vector2[] moveX;
    [SerializeField] Vector2[] moveY;

    private void Update()
    {
        if (enterDoorButton[0] == null && enterDoorButton[1] == null) 
        {
            if (isEnterMovePosY) 
            {
                doorEnter.transform.localPosition = Vector2.Lerp(doorEnter.transform.localPosition, moveY[0], 10 * Time.deltaTime);
            }
            //Destroy(doorEnter, 0.5f)
        }
        if (isExitDoorOpen) 
        {
            if (isExitMovePosX)
            {
                doorExit.transform.localPosition = Vector2.Lerp(doorExit.transform.localPosition, moveX[0], 10 * Time.deltaTime);
            }
        }
        if (isStartingToSurvive) 
        {
            countDownTimer -= 1 * Time.deltaTime;
            if (countDownTimer < 0) 
            { 
                countDownTimer = 0;
                isStartingToSurvive = false;
                Destroy(enemySpawner);
                Destroy(GameObject.FindGameObjectWithTag("Enemy"));
                isExitDoorOpen = true;
                
                //Destroy(doorExit);
            }
        }
    }
}
