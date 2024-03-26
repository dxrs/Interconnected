using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageFloating : MonoBehaviour
{
    [SerializeField] Garbage garbage;

    [SerializeField] string garbageType;

    [SerializeField] GameObject garbageParent;

    [SerializeField] Rigidbody2D garbageRb;

    [SerializeField] float sineSpeed;
    [SerializeField] float sinePower;

    [SerializeField] Sprite[] garbageImageFrame;

    float randomOffsetSinePos;

    Vector2 garbageFloatingPos;


    SpriteRenderer sr;
    

    private void Start()
    {
        garbageFloatingPos = transform.localPosition;
        randomOffsetSinePos = Random.Range(0, 2);
        sr = GetComponent<SpriteRenderer>();
        //sr.sortingOrder = -1;
        sr.color = new Color(1, 1, 1, 1);
    }

    private void Update()
    {
       
        
        if (garbage.isGarbageCollected) 
        {
            transform.position = garbageParent.transform.position;
            sr.sprite = garbageImageFrame[0];
        }
        else
        {
            if (GarbageCollector.garbageCollector.garbageCollected >= 5)
            {
                sr.color = new Color(1, 1, 1, 0.5f);
                //alphaColor.a = 0;
                //sr.color = alphaColor;
            }
            else
            {
                sr.color = new Color(1, 1, 1, 1);
                //alphaColor.a = 1;
                //sr.color = alphaColor;
            }
            sr.sprite = garbageImageFrame[1];
            transform.localPosition = new Vector2(garbageFloatingPos.x + (Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower),
                    garbageFloatingPos.y + (Mathf.Cos(-sineSpeed * Time.time + randomOffsetSinePos) * sinePower));
            if (garbageRb.drag > 2) 
            {
               
            }
            //transform.position = garbageParent.transform.position;
        }

        if(garbage.isGarbageCollected && garbage.isPlayerBringGarbage) 
        {
            if(LevelStatus.levelStatus.levelID != 4) 
            {
                if (GarbageCenterPoint.garbageCenterPoint.buttonGarbageStoreValue == 2)
                {
                    if (Timer.timerInstance.isTimerLevel)
                    {
                        transform.localScale = new Vector2(.02f, .02f);
                    }

                }
            }
            else 
            {
                if (GarbageCenterPoint.garbageCenterPoint.buttonGarbageStoreValue == 2)
                {
                    transform.localScale = new Vector2(.02f, .02f);
                   

                }
            }
            
            
        }
    }
}
