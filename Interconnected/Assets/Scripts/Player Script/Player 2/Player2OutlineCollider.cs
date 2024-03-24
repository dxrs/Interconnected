using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2OutlineCollider : MonoBehaviour
{
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage"))
        {
            if (LinkRay.linkRay.isPlayerLinkedEachOther)
            {
                if (GarbageCollector.garbageCollector.garbageCollected < GarbageCollector.garbageCollector.limitGarbageCollected)
                {
                    //GarbageCollector.garbageCollector.garbageCollected++;

                }

            }


        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Garbage"))
        {
            if (LinkRay.linkRay.isPlayerLinkedEachOther)
            {
                if (GarbageCollector.garbageCollector.garbageCollected < GarbageCollector.garbageCollector.limitGarbageCollected)
                {
                   // GarbageCollector.garbageCollector.garbageCollected++;

                }

            }


        }
    }
}
