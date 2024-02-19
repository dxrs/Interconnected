using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponBullet;
    [SerializeField] GameObject bulletSpawner;

    [SerializeField] bool isBurstSpawn;

    [SerializeField] float burstDelayObjectSpawn;
    [SerializeField] float delayTime;
    [SerializeField] float distanceBurstBulletSpawn;
    [SerializeField] int burstObjectCount;

    private void Start()
    {
        StartCoroutine(setWeaponSpawn());
    }

    IEnumerator setWeaponSpawn() 
    {
        if (isBurstSpawn)
        {
            yield return new WaitForSeconds(burstDelayObjectSpawn);
        }
        while (true) 
        {
            float waitForNextSpawn = distanceBurstBulletSpawn / burstObjectCount;
            if (isBurstSpawn) 
            {
                for(int j=0; j < burstObjectCount; j++) 
                {
                    Instantiate(weaponBullet, bulletSpawner.transform.position, Quaternion.identity);
                    yield return new WaitForSeconds(waitForNextSpawn);
                }
            }
            else 
            {
                Instantiate(weaponBullet, bulletSpawner.transform.position, Quaternion.identity);
            }

            yield return new WaitForSeconds(delayTime);
        }
    }
}
