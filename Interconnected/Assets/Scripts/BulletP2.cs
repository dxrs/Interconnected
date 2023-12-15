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
        if (!LinkRay.linkRay.isLinkedToPlayer) 
        {
            float maxDistance = 6.5f;
            GameObject nearestObstacle = null;
            float nearestDistance = float.MaxValue;

            for (int j = 0; j < targetToObstacleP2.Length; j++)
            {
                float distance = Vector2.Distance(transform.position, targetToObstacleP2[j].transform.position);

                if (distance < nearestDistance)
                {
                    nearestObstacle = targetToObstacleP2[j];
                    nearestDistance = distance;

                }
            }

            if (nearestObstacle != null && nearestDistance < maxDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, nearestObstacle.transform.position, bulletSpeed * Time.deltaTime);
            }
            else { Destroy(gameObject); }
        }
        else 
        {
            Destroy(gameObject);
        }
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
