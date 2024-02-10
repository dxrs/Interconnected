using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletP2 : MonoBehaviour
{
    [SerializeField] float bulletSpeed;

    GameObject[] targetToObstacleP2;

    CircleCollider2D cc;

    private void Start()
    {
        targetToObstacleP2 = GameObject.FindGameObjectsWithTag("Obstacle P2");
    }
    private void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Obstacle" ||
            collision.gameObject.tag == "Obstacle P1" ||
            collision.gameObject.tag == "Obstacle P2")
        {
            Destroy(gameObject);
        }
       
    }
}
