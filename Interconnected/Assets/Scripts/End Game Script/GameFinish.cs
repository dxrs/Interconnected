using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{
    public static GameFinish gameFinish;

    public bool isGameFinish;

    [SerializeField] GlobalVariable globalVariable;

    public int finishValue;

    private void Awake()
    {
        gameFinish = this;
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID != 3) 
        {
            if (GarbageCollector.garbageCollector.currentGarbageStored >= GarbageCollector.garbageCollector.targetGarbageStored) 
            {
                //isGameFinish = true;
            }
            if (finishValue == 2 && GarbageCollector.garbageCollector.garbageCollected >= 5) 
            {
                //StartCoroutine(waitToFinish());
                //Debug.Log("finish tutorial");
            }
        }
    }

    IEnumerator waitToFinish() 
    {
        yield return new WaitForSeconds(1);
        isGameFinish = true;
    }
}
