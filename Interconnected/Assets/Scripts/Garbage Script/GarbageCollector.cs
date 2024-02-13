using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public static GarbageCollector garbageCollector;

    public float radius;

    public int garbageCollected;
    public int currentGarbageStored;
    public int maxGarbageStored;

    [SerializeField] Transform player1;
    [SerializeField] Transform player2;


    private void Start()
    {
        garbageCollector = this;
    }
    private void Update()
    {
        if(player1 && player2 != null) 
        {
            Vector2 posA = player1.position;
            Vector2 posB = player2.position;
            Vector2 midPos = (posA + posB) / 2f;
            transform.position = midPos;
        }

        if(!LinkRay.linkRay.isPlayerLinkedEachOther || GlobalVariable.globalVariable.isTriggeredWithObstacle) 
        {
            garbageCollected = 0;
        }
    }
}
