using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1ShootPoint : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] GameObject playerBullet;
    [SerializeField] GameObject shootPoint;

    [SerializeField] float waitToSpawn;

    private void Start()
    {
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
                Instantiate(playerBullet, shootPoint.transform.position, transform.rotation);
            }
           
            
            yield return new WaitForSeconds(waitToSpawn);
        }


    }
}
