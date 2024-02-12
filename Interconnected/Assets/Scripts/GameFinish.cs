using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{
    public static GameFinish gameFinish;

    [SerializeField] GlobalVariable globalVariable;

    public int finishValue;

    private void Awake()
    {
        gameFinish = this;
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID == 4) 
        {
            if (finishValue == 2 && GarbageCollector.garbageCollector.garbageCollected >= 5) 
            {
                //Debug.Log("finish tutorial");
            }
        }
    }
}
