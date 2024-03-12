using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int id;

    [SerializeField] GameObject[] doorButton;

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
                    

                    if(Mathf.Approximately(transform.localPosition.x, doorMoveTarget.x) && Mathf.Approximately(transform.localPosition.y, doorMoveTarget.y)) 
                    {
                        if (doorButton[0] && doorButton[1] != null) 
                        {
                            for(int k = 0; k < doorButton.Length; k++) 
                            {
                                doorButton[k].transform.position = transform.position;
                            }
                        }

                        Destroy(gameObject,5);
                        for (int k = 0; k < doorButton.Length; k++)
                        {
                            Destroy(doorButton[k], 5);
                        }
                    }
                }
                if (doorButton[0] && doorButton[1] != null) 
                {
                    if (doorButton[0].transform.localScale.x <= 0.8f && doorButton[1].transform.localScale.x <= 0.8f)
                    {
                        isDoorOpen = true;
                    }
                }
               
            }
        }
    }
}
