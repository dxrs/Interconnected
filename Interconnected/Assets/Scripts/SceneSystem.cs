using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
            // restart scne nya di siniaja
        }
        if (isExitScene) 
        {
            // exit scene di sini aja
        }
    }
}
