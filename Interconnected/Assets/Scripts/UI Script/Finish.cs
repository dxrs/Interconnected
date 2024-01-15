using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] GameObject finishUI;

    private void Update()
    {
        if (GlobalVariable.globalVariable.isGameFinish) 
        {
            finishUI.SetActive(true);
        }
    }
}
