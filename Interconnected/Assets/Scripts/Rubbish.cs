using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rubbish : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Rubbish Trigger" ) 
        {
            Destroy(gameObject);
        }
    }
}
