using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int id;

    [SerializeField] float waitTimeToSpawn;
    [SerializeField] float delayTimerSpawn;
    [SerializeField] float enemyRadiusValue;

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
            enemySpawnPos = enemySpawnRadius.transform.position;
            enemySpawnPos += Random.insideUnitCircle.normalized * enemyRadiusValue;
            Instantiate(enemy, enemySpawnPos, Quaternion.identity);
            yield return new WaitForSeconds(waitTimeToSpawn);
        }
    }
}
