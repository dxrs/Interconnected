using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public int totalLevel;
    public int totalLevelUnlocked;
    public int[] totalLevelPerChapter;


    private void Awake()
    {
        levelManager = this;
        if (totalLevelPerChapter.Length > 0)
        {
            totalLevel = CalculateTotalLevel();
        }
    }

    private void Start()
    {
       
    }
    int CalculateTotalLevel()
    {
        int total = 0;

        for (int i = 0; i < totalLevelPerChapter.Length; i++)
        {
            total += totalLevelPerChapter[i];
        }

        return total;
    }
}
