using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    public static SceneSystem sceneSystem;

    public bool isRestartScene;
    public bool isExitScene;
    public bool isNextScene;
    public bool isPreviousScene;
    public bool isChangeScene;

    [SerializeField] float delayTimeRestart;
    [SerializeField] float delayTimeExit;
    [SerializeField] float delayTimeNextScene;
    [SerializeField] float delayPreviousScene;

    private void Awake()
    {
        sceneSystem = this;
    }

    private void Update()
    {
        if (isRestartScene)  // restart level
        {
            isChangeScene = true;
            StartCoroutine(waitToRestartScene());
        }
        if (isExitScene) // exit level
        {
            isChangeScene = true;
            StartCoroutine(waitToExitScene());
        }
        if (isNextScene) // next level
        {
            isChangeScene = true;
            StartCoroutine(waitToNextScene());

        }
    }

    public void currentLevelSelected() 
    {
        LevelManager.levelManager.levelChoosed = SceneManager.GetActiveScene().buildIndex - 1;
    }

    public void levelNumber(int number) 
    {
        number = SceneManager.GetActiveScene().buildIndex - 1;
    }

    public void goingToMainMenu() 
    {
        isChangeScene = true;
        StartCoroutine(waitToSceneMainMenu());
    }

    public void goingToSelectLevelPerChapter() 
    {
        isChangeScene = true;
        StartCoroutine(waitTSelectLevelScene());
    }

    public void goingToTutorialScene() 
    {
        isChangeScene = true;
        StartCoroutine(waitToSceneTutorialScene());
    }

    public void goingToPrologueScene() // dari main menu ke prologue scene
    {
        isChangeScene = true;
        StartCoroutine(waitToScenePrologueScene());
    }

    public void goingToLevelSelected(int levelIndex) 
    {
        isChangeScene = true;
        StartCoroutine(waitToLevelSelectedScene());
    }

    public void goingToChapterSelect() 
    {
        isChangeScene = true;
        StartCoroutine(waitToSceneSelectChapter());
    }

    #region bagian ienumeratornya
    IEnumerator waitToScenePrologueScene() 
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        SceneManager.LoadScene("Prologue");
    }

    IEnumerator waitToSceneSelectChapter()
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        SceneManager.LoadScene("Select Chapter");
    }

    IEnumerator waitToSceneTutorialScene() 
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        SceneManager.LoadScene(1); // tutorial scene
    }

    IEnumerator waitToSceneMainMenu()
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        //SceneManager.LoadScene(1);
        SceneManager.LoadScene("Main Menu");
    }
    IEnumerator waitToRestartScene() 
    {
        yield return new WaitForSecondsRealtime(delayTimeRestart);
        int currentRestartScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentRestartScene);
    }

    IEnumerator waitTSelectLevelScene() // dari level kembali ke select level
    {
        yield return new WaitForSeconds(delayTimeExit);
        SceneManager.LoadScene("Select Level Chapter " + SelectChapter.selectChapter.curValueButton); 
                                   
    }

    IEnumerator waitToExitScene() 
    {
        yield return new WaitForSecondsRealtime(delayTimeExit);
        SceneManager.LoadScene("Select Level Chapter " + ChapterManager.chapterManager.chapterChoosed);
    }

    IEnumerator waitToNextScene() 
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);

    }

    IEnumerator waitToLevelSelectedScene() 
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        SceneManager.LoadScene(SelectLevel.selectLevel.curSelectLevelValue + 1);
    }
    #endregion
}
