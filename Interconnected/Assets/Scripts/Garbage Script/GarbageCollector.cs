using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GarbageCollector : MonoBehaviour
{
    public static GarbageCollector garbageCollector;

    public float radius;

    public bool isGarbageStored;

    public int[] playerReadyToStoreValue;

    public int garbageCollected;
    public int currentGarbageStored;
    public int targetGarbageStored;

    [SerializeField] Transform player1;
    [SerializeField] Transform player2;

    private void Start()
    {
        garbageCollector = this;
        GameObject[] currentGarbageFloating = GameObject.FindGameObjectsWithTag("Garbage");
        targetGarbageStored = currentGarbageFloating.Length;
    }
    private void Update()
    {
        
        if (playerReadyToStoreValue.Length != 0) 
        {
            if (playerReadyToStoreValue[0] == 1 && playerReadyToStoreValue[1] == 1)
            {
                StartCoroutine(setIsGarbageStoredTtrue());
            }
            else
            {
                isGarbageStored = false;
            }
        }
        
        if(player1 && player2 != null) 
        {
            Vector2 posA = player1.position;
            Vector2 posB = player2.position;
            Vector2 midPos = (posA + posB) / 2f;
            transform.position = midPos;
        }

        if(!LinkRay.linkRay.isPlayerLinkedEachOther || GlobalVariable.globalVariable.isPlayerDestroyed) 
        {
            garbageCollected = 0;
        }
    }

    

    IEnumerator setIsGarbageStoredTtrue() 
    {
        yield return new WaitForSeconds(.3f);
        isGarbageStored = true;
    }
}
