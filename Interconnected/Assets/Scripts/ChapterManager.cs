using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChapterManager : MonoBehaviour
{
    public static ChapterManager chapterManager;

    public int currentChapter; // dipanggil menggunakan save data
    public int chapterChoosed; // apabila pengen main level di chapter sebelum currentChapter
    public int totalChapter;
    public int[] totalLevelPerChapter;

    private void Awake()
    {
        chapterManager = this;
        
    }
    private void Start()
    {
        totalChapter = totalLevelPerChapter.Length;
    }
    private void Update()
    {
        if (totalLevelPerChapter.Length > 0)
        {
            LevelManager.levelManager.totalLevel = CalculateTotalLevel();
        }
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
