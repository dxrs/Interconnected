using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2HealthPointObject : MonoBehaviour
{
    GameObject player1;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player1.transform.position, 25 * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1") 
        {
            Destroy(gameObject);
        }
    }
}
