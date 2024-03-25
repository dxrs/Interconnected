using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageWhirlpool : MonoBehaviour
{
    private void Update()
    {
        if (Timer.timerInstance.isTimerLevel) 
        {
            if (GarbageCenterPoint.garbageCenterPoint.buttonGarbageStoreValue == 2)
            {
                transform.Rotate(Vector3.forward, 150 * Time.deltaTime);
            }
        }
     
        
    }
}
