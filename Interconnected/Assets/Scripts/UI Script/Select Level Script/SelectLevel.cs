using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectLevel : MonoBehaviour
{
    public static SelectLevel selectLevel;

    public int curSelectLevelValue;

    public bool isCameraNotMoving;

    [SerializeField] Button[] listLevelButton;
    private void Awake()
    {
        selectLevel = this;
    }

    private void Update()
    {
        
      
    }


    public void onClickChooseLevelLeft() 
    {
        curSelectLevelValue--;
    }

    public void onClickChooseLevelRight() 
    {
        curSelectLevelValue++;
    }

    public void onClickLevelButton() 
    {

    }
}
