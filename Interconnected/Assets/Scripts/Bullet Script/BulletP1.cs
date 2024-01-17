using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletP1 : MonoBehaviour
{

    [SerializeField] float bulletSpeed;
    [SerializeField] float colorChangeDistanceThreshold;

    GameObject targetToPlayer2;
    GameObject[] targetToObstacleP1;

    [SerializeField] Color defaultBulletColor;
    [SerializeField] Color targetColor;

    SpriteRenderer sr;

    CircleCollider2D cc;
    Color currentColor;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = defaultBulletColor;
        targetToPlayer2 = GameObject.FindGameObjectWithTag("Player 2");
        targetToObstacleP1 = GameObject.FindGameObjectsWithTag("Obstacle P1");
    }

    private void Update()
    {
       
        if (LinkRay.linkRay.isLinkedToPlayer) 
        {
            if (targetToPlayer2 != null && !GlobalVariable.globalVariable.isTriggeredWithObstacle
                        && !GlobalVariable.globalVariable.isNotShoot
                        && !SceneSystem.sceneSystem.isExitScene
                        && !SceneSystem.sceneSystem.isRestartScene) 
            {
                transform.position = Vector2.MoveTowards(transform.position, targetToPlayer2.transform.position, bulletSpeed * Time.deltaTime);

                float distanceToPlayer2 = Vector2.Distance(transform.position, targetToPlayer2.transform.position);

                float normalizedDistance = Mathf.Clamp01(distanceToPlayer2 / colorChangeDistanceThreshold);

                Color currentColor = Color.Lerp(targetColor, defaultBulletColor, normalizedDistance);
                sr.color = currentColor;
            }
            else
            {
                Destroy(gameObject);
            }

        }
        else 
        {
    
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

    private float GetDistanceToPlayer2()
    {
        return Vector2.Distance(transform.position, targetToPlayer2.transform.position);
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


