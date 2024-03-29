using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFloating : MonoBehaviour
{
    [SerializeField] Garbage garbage;

    [SerializeField] GameObject garbageParent;

    [SerializeField] Rigidbody2D garbageRb;

    [SerializeField] float sineSpeed;
    [SerializeField] float sinePower;

    float randomOffsetSinePos;

    Vector2 garbageFloatingPos;

    private void Start()
    {
        garbageFloatingPos = transform.localPosition;
        randomOffsetSinePos = Random.Range(0, 2);
    }

    private void Update()
    {
        
        if (garbage.isGarbageCollected) 
        {
            transform.position = garbageParent.transform.position;
        }
        else
        {
            transform.localPosition = new Vector2(garbageFloatingPos.x + (Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower),
                    garbageFloatingPos.y + (Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower));
            if (garbageRb.drag > 2) 
            {
               
            }
            //transform.position = garbageParent.transform.position;
        }
    }
}
