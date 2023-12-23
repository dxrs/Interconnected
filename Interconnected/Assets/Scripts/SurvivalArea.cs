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

    [SerializeField] float countDownTimer;

    private void Update()
    {
        if (enterDoorButton[0] == null && enterDoorButton[1] == null) 
        {
            Destroy(doorEnter, 0.5f);
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
                Destroy(doorExit);
            }
        }
    }
}
