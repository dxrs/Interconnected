using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public int totalLevel;
    public int totalLevelUnlocked; // dipanggil menggunakan save data
    public int currentTotalLevelSection;
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
        currentTotalLevelSection = (totalLevelUnlocked) / 3;
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

    public void saveDataCurrentLevel() 
    {
        totalLevelUnlocked++;
        // save di sini
        // di panggil di game finish
    }
}
