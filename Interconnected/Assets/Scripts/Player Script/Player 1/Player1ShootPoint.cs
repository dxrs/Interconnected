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
            if (linkRay.isPlayerLinkedEachOther
                    && GlobalVariable.globalVariable.isRopeVisible
                    && !SceneSystem.sceneSystem.isExitScene
                    && !SceneSystem.sceneSystem.isRestartScene
                    && !GameOver.gameOver.isGameOver)
            {
                Instantiate(player1Bullet, shootPoint.transform.position, Quaternion.identity);
            }
           
            
            yield return new WaitForSeconds(waitToSpawn);
        }


    }
}
