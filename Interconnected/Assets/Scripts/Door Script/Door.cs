using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public int id;

    [SerializeField] GameObject[] doorButton;

    [SerializeField] float doorMoveSpeed;

    [SerializeField] Vector2 doorMoveTarget;

    private void Update()
    {
        for(int j = 1; j <= GlobalVariable.globalVariable.maxDoor; j++) 
        {
            if (id == j) 
            {
                if (doorButton[0] == null && doorButton[1] == null)
                {
                    transform.position = Vector2.MoveTowards(transform.position,
                        doorMoveTarget, doorMoveSpeed * Time.deltaTime);
                   
                }
            }
        }
    }
}
