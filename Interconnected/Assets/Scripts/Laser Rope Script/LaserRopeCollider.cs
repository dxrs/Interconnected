using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRopeCollider : MonoBehaviour
{

    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage")) 
        {
            if (GarbageCollector.garbageCollector.garbageCollected < GarbageCollector.garbageCollector.limitGarbageCollected) 
            {
                GarbageCollector.garbageCollector.garbageCollected++;
            }
                
        }
    }
   
}
