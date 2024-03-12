using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour
{
    public static LevelStatus levelStatus;

    public int levelID;
   public enum statusLevel { Garbage_Collector, Survival, Boss, Tutorial }
    public statusLevel levelStats;

    private void Awake()
    {
        levelStatus = this;
    }
    private void Start()
    {
        string levelString = levelStats.ToString();
        Debug.Log("level status: " + levelString);

        if (levelStats == statusLevel.Garbage_Collector)
        {
            levelID = 1;
        }
        else if (levelStats == statusLevel.Survival)
        {
            levelID = 2;
        }
        else if (levelStats == statusLevel.Boss)
        {
            levelID = 3;
        } else if (levelStats == statusLevel.Tutorial)
        {
            levelID = 4;
        }
        else { Debug.Log("error"); }
    }
}

