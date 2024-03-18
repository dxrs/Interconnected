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
    


    private void Awake()
    {
        levelManager = this;
        
    }

    private void Start()
    {
        currentTotalLevelSection = (totalLevelUnlocked) / 3;
        
    }
    private void Update()
    {
        //SceneSystem.sceneSystem.currentLevelSelected();

        if (levelChoosed == Mathf.Clamp(levelChoosed, 0, 9)) 
        {
            ChapterManager.chapterManager.chapterChoosed = 1;
        }
    }

    public void saveDataCurrentLevel() 
    {
        totalLevelUnlocked++;
        // save di sini
        // di panggil di game finish
    }
}
