using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStatus : MonoBehaviour
{
   public enum statusLevel { Adventure, Survival, Boss }
    public statusLevel levelstats;

    private void Start()
    {
        string levelString = levelstats.ToString();
        Debug.Log("Level as String: " + levelString);
    }
}

