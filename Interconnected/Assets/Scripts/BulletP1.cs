using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletP1 : MonoBehaviour
{
    

    GameObject targetToPlayer2;
    GameObject[] targetToObstacleP1;

    CircleCollider2D cc;

    private void Start()
    {
        targetToPlayer2 = GameObject.FindGameObjectWithTag("Player 2");
        targetToObstacleP1 = GameObject.FindGameObjectsWithTag("Obstacle P1");
    }

    private void Update()
    {

        
        if (LinkRay.linkRay.isLinkedToPlayer) 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetToPlayer2.transform.position, 10 * Time.deltaTime);
        }
        else 
        {
            // Tentukan jarak maksimum yang dianggap sebagai "terdekat"
            float maxDistance = 7.5f;

            // Tentukan variabel untuk menyimpan obstacle terdekat
            GameObject nearestObstacle = null;

            // Tentukan variabel untuk menyimpan jarak terdekat
            float nearestDistance = float.MaxValue;

            // Loop melalui semua targetToObstacleP1
            for (int j = 0; j < targetToObstacleP1.Length; j++)
            {
                // Hitung jarak antara objek saat ini dan targetToObstacleP1[j]
                float distance = Vector2.Distance(transform.position, targetToObstacleP1[j].transform.position);

                // Periksa apakah jaraknya lebih kecil dari jarak terdekat yang saat ini diketahui
                if (distance < nearestDistance)
                {
                    // Jika ya, perbarui obstacle terdekat dan jarak terdekat
                    nearestObstacle = targetToObstacleP1[j];
                    nearestDistance = distance;
                }
            }

            // Periksa apakah ada obstacle terdekat dan apakah jaraknya kurang dari maksimum yang diizinkan
            if (nearestObstacle != null && nearestDistance < maxDistance)
            {
                // Gerakkan objek menuju obstacle terdekat
                transform.position = Vector2.MoveTowards(transform.position, nearestObstacle.transform.position, 10 * Time.deltaTime);
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Obstacle"||
            collision.gameObject.tag=="Obstacle P1"||
            collision.gameObject.tag == "Obstacle P2"||
            collision.gameObject.tag=="Player 2") 
        {
            Destroy(gameObject);
        }
    }
}


