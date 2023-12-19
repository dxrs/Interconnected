using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] int id;

    [SerializeField] float waitTimeToSpawn;

    [SerializeField] GameObject enemySpawnerPoint;
    [SerializeField] GameObject enemy;


    private void Start()
    {
        StartCoroutine(spawneEnemy());
    }
    private void Update()
    {
        if (id == 1) 
        {
            if (SpawnerValue.spawnerValue.spawnerValuerIndex[0] == 1) 
            {
                Debug.Log("musuh keluar di id yang " + id);
            }
        }
        if (id == 2)
        {
            if (SpawnerValue.spawnerValue.spawnerValuerIndex[1] == 1)
            {
                Debug.Log("musuh keluar di id yang " + id);
            }
        }
    }

    IEnumerator spawneEnemy() 
    {
        while (true)
        {
            for (int i = 0; i < SpawnerValue.spawnerValue.spawnerValuerIndex.Length; i++)
            {
                if (id == i + 1 && SpawnerValue.spawnerValue.spawnerValuerIndex[i] == 1)
                {
                    Instantiate(enemy, enemySpawnerPoint.transform.position, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(waitTimeToSpawn);
        }
    }
}
