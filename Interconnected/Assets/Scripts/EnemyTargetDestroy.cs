using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyTargetDestroy : MonoBehaviour
{
    public static EnemyTargetDestroy enemyTargetDestroy;

    public int curValueEnemyDestroy;

    [SerializeField] int[] targetEnemyDestroyValue;
    [SerializeField] TextMeshProUGUI[] textTargetEnemyDestroy;

    private void Awake()
    {
        enemyTargetDestroy = this;
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID == 2)
        {
            for (int i = 0; i < textTargetEnemyDestroy.Length && i < targetEnemyDestroyValue.Length; i++)
            {
                textTargetEnemyDestroy[i].text = "Destroy " + targetEnemyDestroyValue[i] + " Enemy";
            }
        }

      
    }
}
