using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour
{
    public static LevelStatus levelStatus;

    public int levelID;
   public enum statusLevel { Adventure, Survival, Boss }
    public statusLevel levelStats;

    private void Awake()
    {
        levelStatus = this;
    }
    private void Start()
    {
        string levelString = levelStats.ToString();
        Debug.Log("level status: " + levelString);

        if (levelStats == statusLevel.Adventure)
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
        }
        else { Debug.Log("error"); }
    }
}

