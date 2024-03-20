using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveDataManager : MonoBehaviour
{
    public static SaveDataManager saveDataManager;

    [Tooltip("0 untuk level unlock data\n" +
        "1 untuk nilai b\n" +
        "2 untuk nilai c")]
    public string[] listDataManager;

    private void Awake()
    {
        saveDataManager = this;
    }

    private void Start()
    {
        Debug.Log("Nilai listDataManager[0]: " + PlayerPrefs.GetInt(listDataManager[0]));
        PlayerPrefs.DeleteAll();
        if (!PlayerPrefs.HasKey(listDataManager[0])) 
        {
            //PlayerPrefs.SetInt(listDataManager[0], 0); // data level mulai dari 0, 0 adalah tutorial
        }
    }

    private void Update()
    {
        
    }
}
