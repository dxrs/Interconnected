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
    [SerializeField] TextMeshProUGUI textEnemyDestroy;

    private void Awake()
    {
        enemyTargetDestroy = this;
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID == 2)
        {
            if (curValueEnemyDestroy == Mathf.Clamp(curValueEnemyDestroy, 0, targetEnemyDestroyValue[2])) 
            {
                textEnemyDestroy.text = "Enemy Destroyed " + curValueEnemyDestroy + " / " + targetEnemyDestroyValue[2].ToString();
            }
            if(curValueEnemyDestroy == Mathf.Clamp(curValueEnemyDestroy, targetEnemyDestroyValue[2], targetEnemyDestroyValue[1])) 
            {
                textEnemyDestroy.text = "Enemy Destroyed " + curValueEnemyDestroy + " / " + targetEnemyDestroyValue[1].ToString();
            }
            if (curValueEnemyDestroy == Mathf.Clamp(curValueEnemyDestroy, targetEnemyDestroyValue[1], targetEnemyDestroyValue[0]) || curValueEnemyDestroy > targetEnemyDestroyValue[0])
            {
                textEnemyDestroy.text = "Enemy Destroyed " + curValueEnemyDestroy + " / " + targetEnemyDestroyValue[0].ToString();
            }
            
            for (int i = 0; i < textTargetEnemyDestroy.Length && i < targetEnemyDestroyValue.Length; i++)
            {
                textTargetEnemyDestroy[i].text = "Destroy " + targetEnemyDestroyValue[i] + " Enemy";
            }
        }

      
    }
}
