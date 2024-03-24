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

    [Tooltip("menentukan value id dari index scene untuk masuk ke setiap level")] [SerializeField] int idLevel; // untuk memanggil ke scene setiap level dengan idLevel tertentu tidak menampilkan string level number
    [Tooltip("informasi angka level")] [SerializeField] int levelButtonNumber; // untuk menampilkan string angka level saja

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

        if (idLevel > 1) // artinya setelah scene tutorial, karena indexnya 1
        {
            if (isButtonLocked)
            {
                textStatusLevel.text = "Locked";
            }
            else
            {
                
                textStatusLevel.text = levelButtonNumber.ToString();
            }
        }
       
        
    }
    public void inputNavigationConfirm(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (selectLevel.isInputKeyboardChoose && selectLevel.isCameraNotMoving)
            {
                SceneSystem.sceneSystem.goingToLevelSelected(idLevel);
            }
        }
    }
    public void onClickLevelButton() 
    {
        SceneSystem.sceneSystem.goingToLevelSelected(idLevel);
    }
}
