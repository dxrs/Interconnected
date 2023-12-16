using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player2ShootPoint : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] GameObject player2Bullet;
    [SerializeField] GameObject shootPoint;

    [SerializeField] float waitToSpawn;

    GameObject[] targetToObstacleP2;

    private void Start()
    {
        targetToObstacleP2 = GameObject.FindGameObjectsWithTag("Obstacle P2");
        StartCoroutine(bulletP2Spawn());
    }

    IEnumerator bulletP2Spawn() 
    {
        while (true) 
        {
            if (!Player2.player2.isGhosting) 
            {
                if (!linkRay.isLinkedToPlayer) 
                {
                    for(int x = 0; x < targetToObstacleP2.Length; x++) 
                    {
                        if (linkRay.player2LinkedToObstacle)
                        {
                            Instantiate(player2Bullet, shootPoint.transform.position, Quaternion.identity);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(waitToSpawn);
        }
    }
}
