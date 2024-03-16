using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageStoreButton : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2")) 
        {
            Destroy(gameObject);
        }
    }
}
