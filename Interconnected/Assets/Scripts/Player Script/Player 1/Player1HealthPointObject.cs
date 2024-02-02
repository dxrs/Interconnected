using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1HealthPointObject : MonoBehaviour
{
    GameObject player2;

    private void Start()
    {
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player2.transform.position, 25 * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player 2") 
        {
            Destroy(gameObject);
        }
    }
}
