using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    [SerializeField] int id;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (id == 1) 
        {
            if(collision.gameObject.tag=="Player 1") 
            {
                Destroy(gameObject);
            }
        }
        if (id == 2)
        {
            if (collision.gameObject.tag == "Player 2")
            {
                Destroy(gameObject);
            }
        }
    }
}
