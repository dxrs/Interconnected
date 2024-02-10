using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSystem : MonoBehaviour
{
    public static SceneSystem sceneSystem;

    public bool isRestartScene;
    public bool isExitScene;
    public bool isChangeScene;

    private void Awake()
    {
        sceneSystem = this;
    }

    private void Update()
    {
        if (isRestartScene) 
        {
            string currentRestartLevel = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(currentRestartLevel);
        }
        if (isExitScene) 
        {
            SceneManager.LoadScene(0);
            // exit scene di sini aja
        }
    }

    public void goingToTutorialScene() 
    {
        Debug.Log("ke scane tutorial");
    }

    public void goingToPrologueScene() 
    {

    }
}
