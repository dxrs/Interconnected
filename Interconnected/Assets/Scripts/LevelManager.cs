using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager levelManager;

    public int totalLevel;
    public int totalLevelUnlocked; // dipanggil menggunakan save data
    public int indexSceneLevelValue; // apabila pengen maen level sebelum totalLevelUnlocked;
    public int currentTotalLevelSection;
    
    public bool isLevelGame;

    [SerializeField] int maxSection = 3;
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
        if (currentTotalLevelSection > maxSection) 
        {
            currentTotalLevelSection = 3;
        }
        compareCurrentIndexLevel();
       
    }

    private void compareCurrentIndexLevel() 
    {
        if (indexSceneLevelValue == Mathf.Clamp(indexSceneLevelValue, 1, 12))
        {
            ChapterManager.chapterManager.chapterChoosed = 1;
        }

        if (indexSceneLevelValue == Mathf.Clamp(indexSceneLevelValue, 13, 21))
        {
            ChapterManager.chapterManager.chapterChoosed = 2;
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
