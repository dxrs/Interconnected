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

    public void goingToLevelSelected() 
    {
        isChangeScene = true;
        StartCoroutine(waitToLevelSelectedScene());
    }

    IEnumerator waitToScenePrologueScene() 
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        SceneManager.LoadScene("Prologue");
    }

    IEnumerator waitToSceneTutorialScene() 
    {
        yield return new WaitForSeconds(delayTimeNextScene);
        SceneManager.LoadScene(0);
    }

    IEnumerator waitToRestartScene() 
    {
        yield return new WaitForSecondsRealtime(delayTimeRestart);
        int currentRestartScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentRestartScene);
    }

    IEnumerator waitToExitScene() // dari level kembali ke select level
    {
        yield return new WaitForSecondsRealtime(delayTimeExit);
        SceneManager.LoadScene("Select Level"); 
                                   
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
        SceneManager.LoadScene(SelectLevel.selectLevel.curSelectLevelValue);
    }
}
