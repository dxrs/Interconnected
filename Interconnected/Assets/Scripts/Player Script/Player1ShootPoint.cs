using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1ShootPoint : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] GameObject player1Bullet;
    [SerializeField] GameObject shootPoint;

    [SerializeField] GameObject targetBulletToP2;

    [SerializeField] float waitToSpawn;

    GameObject[] targetToObstacleP1;


    private void Start()
    {
        targetToObstacleP1 = GameObject.FindGameObjectsWithTag("Obstacle P1");
        StartCoroutine(bulletP1Spawn());
    }
    IEnumerator bulletP1Spawn() 
    {
        while (true) 
        {
            if (linkRay.isLinkedToPlayer) 
            {
                if (Vector2.Distance(transform.position, targetBulletToP2.transform.position) < 7.5f) 
                {
                    if (linkRay.playerLinkedEachOther)
                    {
                        Instantiate(player1Bullet, shootPoint.transform.position, Quaternion.identity);
                    }
                }

            }
            else 
            {
                for(int j = 0; j < targetToObstacleP1.Length; j++) 
                {
                    if (Vector2.Distance(transform.position, targetToObstacleP1[j].transform.position) < 7.5f)
                    {
                        if (linkRay.player1LinkedToObstacle)
                        {
                            Instantiate(player1Bullet, shootPoint.transform.position, Quaternion.identity);
                        }
                    }
                }
            }
            yield return new WaitForSeconds(waitToSpawn);
        }


    }
}
