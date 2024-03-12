using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameFinish : MonoBehaviour
{
    public static GameFinish gameFinish;

    public bool isGameFinish;

    [SerializeField] GlobalVariable globalVariable;

    public int finishValue;

    private void Awake()
    {
        gameFinish = this;
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID != 4) 
        {
            if (GarbageCollector.garbageCollector.currentGarbageStored >= GarbageCollector.garbageCollector.targetGarbageStored) 
            {
                isGameFinish = true;
            }
            
        }
        else 
        {
            if (Tutorial.tutorial.tutorialProgress >= 3) 
            {
                if (DialogueManager.dialogueManager.curTextValue > 20 && !DialogueManager.dialogueManager.isDialogueActive) 
                {
                    if (GarbageCollector.garbageCollector.currentGarbageStored >= GarbageCollector.garbageCollector.targetGarbageStored)
                    {
                        isGameFinish = true;
                    }
                }
            } 
           
        }
        if (isGameFinish) 
        {
            globalVariable.colliderInactive();
        }
    }
}
