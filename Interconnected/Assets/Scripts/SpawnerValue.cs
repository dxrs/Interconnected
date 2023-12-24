using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerValue : MonoBehaviour
{

    public static SpawnerValue spawnerValue;

    public int[] spawnerValuerIndex;

    public int spawnerTriggerMaxValueEnter;
    public int spawnerTriggerMaxValueExit;

    public Vector2[] player1SpawnPos;
    public Vector2[] player2SpawnPos;

    private void Awake()
    {
        spawnerValue = this;
    }
   
}
