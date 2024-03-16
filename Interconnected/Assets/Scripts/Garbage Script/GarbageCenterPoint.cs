using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageCenterPoint : MonoBehaviour
{
    public static GarbageCenterPoint garbageCenterPoint;

    public int buttonGarbageStoreValue;

    public bool isGarbageIsReadyToDestroy;

    [SerializeField] GameObject[] buttonGarbageStore;
    [SerializeField] GameObject garbageHole;

    private void Awake()
    {
        garbageCenterPoint = this;
    }

    private void Start()
    {
        for (int j = 0; j < buttonGarbageStore.Length; j++)
        {
            if (buttonGarbageStore[j] != null)
            {
                buttonGarbageStore[j].SetActive(false);
            }

        }
        garbageHole.transform.localScale = Vector2.zero;
    }

    private void Update()
    {
        if (garbageHole.transform.localScale.x >= 4.9f)
        {
            isGarbageIsReadyToDestroy = true;
        }
        if (buttonGarbageStoreValue == 2 && GarbageCollector.garbageCollector.garbageDestroyedValue < GarbageCollector.garbageCollector.targetGarbageStored) 
        {
            garbageHole.transform.localScale = Vector2.MoveTowards(garbageHole.transform.localScale, new Vector2(6, 6), 5f * Time.deltaTime);
            
        }
        if (GarbageCollector.garbageCollector.garbageDestroyedValue >= GarbageCollector.garbageCollector.targetGarbageStored) 
        {
            garbageHole.transform.localScale = Vector2.MoveTowards(garbageHole.transform.localScale, Vector2.zero, 8f * Time.deltaTime);
        }
        if(LevelStatus.levelStatus.levelID != 4) 
        {
            if (GarbageCollector.garbageCollector.currentGarbageStored >= GarbageCollector.garbageCollector.targetGarbageStored)
            {
                for (int j = 0; j < buttonGarbageStore.Length; j++)
                {
                    if (buttonGarbageStore[j] != null)
                    {
                        buttonGarbageStore[j].SetActive(true);
                    }

                }
            }
        }
        else 
        {
            if(Tutorial.tutorial.tutorialProgress >= 3 && DialogueManager.dialogueManager.curTextValue >= 22) 
            {
                if (GarbageCollector.garbageCollector.currentGarbageStored >= GarbageCollector.garbageCollector.targetGarbageStored)
                {
                    for (int j = 0; j < buttonGarbageStore.Length; j++)
                    {
                        if (buttonGarbageStore[j] != null)
                        {
                            buttonGarbageStore[j].SetActive(true);
                        }

                    }
                }
            }
        }
       
        
        
    }
}
