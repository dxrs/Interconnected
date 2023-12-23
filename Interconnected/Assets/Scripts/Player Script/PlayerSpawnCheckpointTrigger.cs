using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawnCheckpointTrigger : MonoBehaviour
{
    public static PlayerSpawnCheckpointTrigger playerSpawn;

    public int checkpointValue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (checkpointValue == 1) 
        {
            if(collision.gameObject.tag=="Player 1" || collision.gameObject.tag=="Player 2") 
            {
                SpawnerValue.spawnerValue.spawnerValuerIndex[0] = 1;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (checkpointValue == 1)
        {
            if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 2")
            {
                SpawnerValue.spawnerValue.spawnerValuerIndex[0] = 0;
            }
        }
    }
}
