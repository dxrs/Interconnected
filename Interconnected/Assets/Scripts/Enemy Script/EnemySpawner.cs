using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] float waitTimeToSpawn;
    [SerializeField] float delayTimerSpawn;
    [SerializeField] float enemyRadiusValue;

    [SerializeField] bool isEnemyDelayToSpawn;

    [SerializeField] GameObject enemySpawnRadius;
    [SerializeField] GameObject enemy;

    Vector2 enemySpawnPos;

    private void Start()
    {
        StartCoroutine(spawneEnemy());
    }
    private void Update()
    {
        
    }

    IEnumerator spawneEnemy()
    {
       
        while (true)
        {
            if (ReadyToStart.readyToStart.isGameStart
                && !GameFinish.gameFinish.isGameFinish
                && !GameOver.gameOver.isGameOver
                && !Pause.pause.isGamePaused) 
            {
                if (!isEnemyDelayToSpawn) 
                {
                    if (GlobalVariable.globalVariable.curEnemySpawn < GlobalVariable.globalVariable.maxEnemySpawn) 
                    {
                        enemySpawnPos = enemySpawnRadius.transform.position;
                        enemySpawnPos += Random.insideUnitCircle.normalized * enemyRadiusValue;
                        Instantiate(enemy, enemySpawnPos, Quaternion.identity);
                        GlobalVariable.globalVariable.curEnemySpawn++;
                    }
                    
                }
                if (isEnemyDelayToSpawn) 
                {
                    if (Timer.timerInstance.curTimerValue <= delayTimerSpawn && GlobalVariable.globalVariable.curEnemySpawn < GlobalVariable.globalVariable.maxEnemySpawn) 
                    {
                        enemySpawnPos = enemySpawnRadius.transform.position;
                        enemySpawnPos += Random.insideUnitCircle.normalized * enemyRadiusValue;
                        Instantiate(enemy, enemySpawnPos, Quaternion.identity);
                        GlobalVariable.globalVariable.curEnemySpawn++;
                    }
                }
               
            }
           
            yield return new WaitForSeconds(waitTimeToSpawn);
        }
    }
}
