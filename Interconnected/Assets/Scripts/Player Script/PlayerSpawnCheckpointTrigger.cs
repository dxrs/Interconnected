using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnCheckpointTrigger : MonoBehaviour
{
    public static PlayerSpawnCheckpointTrigger playerSpawn;

    [SerializeField] int checkpointValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        int index = checkpointValue - 1;

        if (index >= 0 && index < SpawnerValue.spawnerValue.spawnerValuerIndex.Length)
        {
            if (collision.CompareTag("Player 1") || collision.CompareTag("Player 2"))
            {
                SpawnerValue.spawnerValue.spawnerValuerIndex[index] = 1;
                GlobalVariable.globalVariable.isEnteringTrapArea = true;
            }
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        int index = checkpointValue - 1;

        if (index >= 0 && index < SpawnerValue.spawnerValue.spawnerValuerIndex.Length)
        {
            if (collision.CompareTag("Player 1") || collision.CompareTag("Player 2"))
            {
                SpawnerValue.spawnerValue.spawnerValuerIndex[index] = 0;
                GlobalVariable.globalVariable.isEnteringTrapArea = false;
            }
        }
    }
}
