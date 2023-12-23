using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariable : MonoBehaviour
{
    public static GlobalVariable globalVariable;

    public bool isEnteringSurvivalArea;

    private void Awake()
    {
        globalVariable = this;
    }
}
