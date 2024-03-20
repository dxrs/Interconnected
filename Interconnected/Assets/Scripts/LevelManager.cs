using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public int totalLevel;
    public int totalLevelUnlocked; // dipanggil menggunakan save data
    public int levelChoosed; // apabila pengen maen level sebelum totalLevelUnlocked;
    public int currentTotalLevelSection;

    public bool isLevelGame;

    private void Awake()
    {
        levelManager = this;
        
    }

    private void Start()
    {
        currentTotalLevelSection = (totalLevelUnlocked) / 3;
        //totalLevelUnlocked = PlayerPrefs.GetInt(SaveDataManager.saveDataManager.listDataManager[0]); // memanggil data level yang kebuka melalui array string yang ke 0 khusus level unlock data
    }
    private void Update()
    {
        

        if (levelChoosed == Mathf.Clamp(levelChoosed, 0, 10)) 
        {
            ChapterManager.chapterManager.chapterChoosed = 1;
        }
    }

    public void saveDataCurrentLevel() 
    {
        totalLevelUnlocked++;
        //PlayerPrefs.SetInt(SaveDataManager.saveDataManager.listDataManager[0], totalLevelUnlocked);
        // save di sini
        // di panggil di game finish
    }
}
