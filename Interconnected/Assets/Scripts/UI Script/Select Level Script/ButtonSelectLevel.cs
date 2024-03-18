using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class ButtonSelectLevel : MonoBehaviour
{

    [SerializeField] SelectLevel selectLevel;

    [SerializeField] int idLevel;

    [SerializeField] bool isButtonLocked;

    [SerializeField] string levelType;

    [SerializeField] Button buttonSelectLevel;

    [SerializeField] TextMeshProUGUI textStatusLevel;
    [SerializeField] TextMeshProUGUI textLevelType;


    private void Update()
    {
        textLevelType.text = levelType;
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

        if (idLevel > 1) 
        {
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
    public void inputNavigationConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (selectLevel.isInputKeyboardChoose && selectLevel.isCameraNotMoving)
            {
                SceneSystem.sceneSystem.goingToLevelSelected(idLevel + 1);
            }
        }
    }
    public void onClickLevelButton() 
    {
        SceneSystem.sceneSystem.goingToLevelSelected(idLevel + 1);
    }
}
