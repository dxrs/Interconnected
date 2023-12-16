using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1ShootPoint : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] LayerMask layerMask;

    [SerializeField] bool isRaycastingToPlayer2;

    [SerializeField] GameObject player1Bullet;
    [SerializeField] GameObject shootPoint;
    [SerializeField] GameObject targetBulletToP2;
    [SerializeField] GameObject player;

    [SerializeField] float waitToSpawn;

    GameObject[] targetToObstacleP1;


    private void Start()
    {
        targetToObstacleP1 = GameObject.FindGameObjectsWithTag("Obstacle P1");
        StartCoroutine(bulletP1Spawn());
    }

    private void Update()
    {
        rayToPlayer2();
    }

    private void rayToPlayer2() 
    {
        bool test = false;
        if (!linkRay.isLinkedToPlayer) 
        {
            for(int k = 0; k < targetToObstacleP1.Length; k++) 
            {

                RaycastHit2D rayToPlayer2 = Physics2D.Linecast(player.transform.position,
                    targetToObstacleP1[k].transform.position, layerMask);
                if (rayToPlayer2.collider != null) 
                {
                    if(rayToPlayer2.collider.tag=="Player 2") 
                    {
                        Debug.Log("terhalang");
                        test = true;
                        break;
                    }
                    else 
                    {
                        //Debug.Log("tidak terhalang");
                    }

                }
                //else { //Debug.Log("tidak terhalang"); }
            }

            if (!test) 
            {
                Debug.Log("tidak terhalang");

            }
        }
    }
    IEnumerator bulletP1Spawn() 
    {
        while (true) 
        {
            if (!Player1.player1.isGhosting) 
            {
                if (linkRay.isLinkedToPlayer)
                {
                    if (linkRay.playerLinkedEachOther)
                    {
                        Instantiate(player1Bullet, shootPoint.transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    for (int j = 0; j < targetToObstacleP1.Length; j++)
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
