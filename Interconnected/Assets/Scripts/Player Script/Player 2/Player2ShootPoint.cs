using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2ShootPoint : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] GameObject playerBullet;
    [SerializeField] GameObject shootPoint;

    [SerializeField] float waitToSpawn;

    private void Start()
    {
        StartCoroutine(bulletP2Spawn());
    }

    IEnumerator bulletP2Spawn()
    {
        while (true)
        {
            


            if (linkRay.isPlayerLinkedEachOther
                  && GlobalVariable.globalVariable.isRopeVisible
                  && !SceneSystem.sceneSystem.isExitScene
                  && !SceneSystem.sceneSystem.isRestartScene
                  && Player1Health.player1Health.curPlayer1Health > 0
                  && Player2Health.player2Health.curPlayer2Health > 0
                  && GarbageCenterPoint.garbageCenterPoint.buttonGarbageStoreValue != 2)
            {
                Instantiate(playerBullet, shootPoint.transform.position, transform.rotation);
            }
            yield return new WaitForSeconds(waitToSpawn);
        }


    }
}
