using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletP1 : MonoBehaviour
{

    [SerializeField] float bulletSpeed;

    GameObject targetToPlayer2;
    GameObject[] targetToObstacleP1;

    [SerializeField] Color defaultBulletColor;
    [SerializeField] Color obstacleBulletColor;

    SpriteRenderer sr;

    CircleCollider2D cc;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        targetToPlayer2 = GameObject.FindGameObjectWithTag("Player 2");
        targetToObstacleP1 = GameObject.FindGameObjectsWithTag("Obstacle P1");
    }

    private void Update()
    {
        if (LinkRay.linkRay.isLinkedToPlayer) 
        {
            if (targetToPlayer2 != null && !GlobalVariable.globalVariable.isTriggeredWithObstacle) 
            {
                transform.position = Vector2.MoveTowards(transform.position, targetToPlayer2.transform.position, bulletSpeed * Time.deltaTime);
            }
            else { Destroy(gameObject); }
            sr.color = defaultBulletColor;
        }
        else 
        {
            sr.color = obstacleBulletColor;
            float maxDistance = 6.5f;
            GameObject nearestObstacle = null;
            float nearestDistance = float.MaxValue;

            for (int j = 0; j < targetToObstacleP1.Length; j++)
            {
                float distance = Vector2.Distance(transform.position, targetToObstacleP1[j].transform.position);

                if (distance < nearestDistance)
                {
                    nearestObstacle = targetToObstacleP1[j];
                    nearestDistance = distance;
                    
                }
            }

            if (nearestObstacle != null && nearestDistance < maxDistance)
            {
                transform.position = Vector2.MoveTowards(transform.position, nearestObstacle.transform.position, bulletSpeed * Time.deltaTime);
            }
            else { Destroy(gameObject); }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Obstacle"||
            collision.gameObject.tag=="Obstacle P1"||
            collision.gameObject.tag == "Obstacle P2") 
        {
            Destroy(gameObject);
        }
        if (LinkRay.linkRay.isLinkedToPlayer) 
        {
            if(collision.gameObject.tag=="Player 2" || collision.gameObject.tag=="Player 2 Shield") 
            {
                Destroy(gameObject);
            }
            
        }
    }
}


