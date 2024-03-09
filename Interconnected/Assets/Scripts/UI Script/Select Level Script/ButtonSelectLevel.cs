using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectLevel : MonoBehaviour
{
    [SerializeField] SelectLevel selectLevel;

    [SerializeField] bool isButtonLocked;

    [SerializeField] Button buttonSelectLevel;

    private void Update()
    {
        if (!selectLevel.isCameraNotMoving) 
        {
            buttonSelectLevel.enabled = false;
        }
        else 
        {
            if (!isButtonLocked && !SceneSystem.sceneSystem.isChangeScene) 
            {
                buttonSelectLevel.enabled = true;
            }
            else 
            {
                buttonSelectLevel.enabled = false;
            }
            
        }
    }
}
