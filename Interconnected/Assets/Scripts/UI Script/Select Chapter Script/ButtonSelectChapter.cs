using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Cinemachine.DocumentationSortingAttribute;

public class ButtonSelectChapter : MonoBehaviour
{
    [SerializeField] SelectChapter selectChapter;
    [SerializeField] ChapterManager chapterManager;

    [SerializeField] int idChapter;

    [SerializeField] bool isButtonLocked;

    [SerializeField] Button buttonChapter;

    [SerializeField] TextMeshProUGUI textStatusButtonChapter;

    private void Update()
    {
        if (selectChapter.isSelectChapterActive) 
        {
            if (!isButtonLocked && !SceneSystem.sceneSystem.isChangeScene) 
            {
                buttonChapter.enabled = true;
            }
            else 
            {
                buttonChapter.enabled = false;
            }
        }
        else 
        {
            buttonChapter.enabled = false;
        }

        if (chapterManager.currentChapter >= idChapter) 
        {
            isButtonLocked = false;
        }
        else 
        {
            isButtonLocked = true;
        }

        if (isButtonLocked)
        {
            textStatusButtonChapter.text = "Locked";
        }
        else
        {
            textStatusButtonChapter.text = "Chapter " + idChapter.ToString();
        }
    }
}
