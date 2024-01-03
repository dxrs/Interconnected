using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] GameObject door;

    [SerializeField] Vector2 doorMoveTarget;

    [SerializeField] bool isDoorOpen;

    private void Update()
    {
        StartCoroutine(waitToOpen());
    }

    IEnumerator waitToOpen() 
    {
        
        if (isDoorOpen)
        {
            yield return new WaitForSeconds(1);
            door.transform.position = Vector2.MoveTowards(door.transform.position,
                doorMoveTarget, 10 * Time.deltaTime);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Circle Target") 
        {
            isDoorOpen = true;
        }
    }
}
