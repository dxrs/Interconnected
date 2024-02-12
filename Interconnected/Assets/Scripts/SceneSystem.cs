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
    public bool isChangeScene;

    [SerializeField] float delayTime;

    private void Awake()
    {
        sceneSystem = this;
    }

    private void Update()
    {
        if (isRestartScene) 
        {
            isChangeScene = true;
            StartCoroutine(waitToRestartScene());
        }
        if (isExitScene) 
        {
            isChangeScene = true;
            StartCoroutine(waitToExitScene());
        }
        if (isNextScene) 
        {
            isChangeScene = true;
            StartCoroutine(waitToNextScene());

        }
    }

    public void goingToTutorialScene() 
    {
        // dari scene prologue
        // kalau tidak ada data gamenya atau new game
        Debug.Log("ke scane tutorial");
        SceneManager.LoadScene(1);
    }

    public void goingToPrologueScene() 
    {
        //dari main menu
        // kalau tidak ada data gamenya atau new game
    }

    IEnumerator waitToRestartScene() 
    {
        yield return new WaitForSeconds(1);
        int currentRestartScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentRestartScene);
    }

    IEnumerator waitToExitScene() 
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(0); // ke select level nanti
                                   // exit scene di sini aja
    }

    IEnumerator waitToNextScene() 
    {
        yield return new WaitForSeconds(1);
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene + 1);

    }
}
