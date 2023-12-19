using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerValue : MonoBehaviour
{

    public static SpawnerValue spawnerValue;

    //public int spawnerValueId;
    // public int maxSpawnerId;

    public int[] spawnerValuerIndex;

    private void Awake()
    {
        spawnerValue = this;
    }
   
}
