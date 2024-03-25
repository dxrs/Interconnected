using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int id;

    [SerializeField] GameObject[] doorButton;
    [SerializeField] Transform[] doorButtonScale;

    [SerializeField] float doorMoveSpeed;

    [SerializeField] Vector2 doorMoveTarget;

    [SerializeField] bool isDoorOpen;

    private void Update()
    {
        for (int j = 1; j <= GlobalVariable.globalVariable.maxDoor; j++)
        {
            if (id == j)
            {

                if (isDoorOpen)
                {
                    transform.localPosition = Vector2.MoveTowards(transform.localPosition,
                     doorMoveTarget, doorMoveSpeed * Time.deltaTime);
                    if (doorButton[0] && doorButton[1] != null)
                    {
                        for (int k = 0; k < doorButton.Length; k++)
                        {
                            doorButton[k].SetActive(false);
                            
                        }
                    }

                    if (Mathf.Approximately(transform.localPosition.x, doorMoveTarget.x) && Mathf.Approximately(transform.localPosition.y, doorMoveTarget.y)) 
                    {
                        if (doorButton[0] && doorButton[1] != null) 
                        {
                            for(int k = 0; k < doorButton.Length; k++) 
                            {
                             
                                doorButton[k].transform.position = transform.position;
                            }
                        }

                        Destroy(gameObject);
                        for (int k = 0; k < doorButton.Length; k++)
                        {
                            Destroy(doorButton[k], 5);
                        }
                    }
                }
                if (doorButton[0] && doorButton[1] != null) 
                {
                    if (doorButtonScale[0].localScale.x <= 0.02f && doorButtonScale[1].localScale.x <= 0.02f)
                    {
                        isDoorOpen = true;
                    }
                }
               
            }
        }
    }
    private void OnDestroy()
    {
        if (LevelStatus.levelStatus.levelID == 4) 
        {
            Tutorial.tutorial.tutorialProgress++;
        }
        
    }
}
