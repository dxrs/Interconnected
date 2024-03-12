using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSelectLevel : MonoBehaviour
{

    [SerializeField] SelectLevel selectLevel;

    [SerializeField] int idLevel;

    [SerializeField] bool isButtonLocked;

    [SerializeField] Button buttonSelectLevel;

    [SerializeField] TextMeshProUGUI textStatusLevel;


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

        if (LevelManager.levelManager.totalLevelUnlocked >= idLevel) 
        {
            isButtonLocked = false;
        }
        else 
        {
            isButtonLocked = true;
        }

        if (isButtonLocked)
        {
            textStatusLevel.text = "Locked";
        }
        else 
        {
            textStatusLevel.text = idLevel.ToString();
        }
        
    }
}
