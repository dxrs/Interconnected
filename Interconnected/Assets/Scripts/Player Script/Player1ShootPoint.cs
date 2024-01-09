using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1ShootPoint : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] GameObject player1Bullet;
    [SerializeField] GameObject shootPoint;

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
            if (!Player1.player1.isGhosting) 
            {
                if (linkRay.isLinkedToPlayer)
                {
                    if (linkRay.playerLinkedEachOther 
                        && !GlobalVariable.globalVariable.isTriggeredWithObstacle
                        && !GlobalVariable.globalVariable.isNotShoot
                        && !SceneSystem.sceneSystem.isExitScene
                        && !SceneSystem.sceneSystem.isRestartScene)
                    {
                        Instantiate(player1Bullet, shootPoint.transform.position, Quaternion.identity);
                    }
                }
                else
                {
                    for (int j = 0; j < targetToObstacleP1.Length; j++)
                    {
                        if (linkRay.player1LinkedToObstacle && !GlobalVariable.globalVariable.isTriggeredWithObstacle
                        && !GlobalVariable.globalVariable.isNotShoot
                        && !SceneSystem.sceneSystem.isExitScene
                        && !SceneSystem.sceneSystem.isRestartScene)
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
