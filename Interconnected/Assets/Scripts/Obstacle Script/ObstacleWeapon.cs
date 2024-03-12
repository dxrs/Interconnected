using System.Collections;
using UnityEngine;

public class ObstacleWeapon : MonoBehaviour
{
    [SerializeField] GameObject weaponBullet;
    [SerializeField] GameObject bulletSpawner;
    [SerializeField] GameObject gunCore;

    [SerializeField] bool isBurstSpawn;

    [Range(0.02f,0.5f)] [SerializeField] float delayTime;
    [SerializeField] float distanceBurstBulletSpawn;
    [SerializeField] int burstObjectCount;

    private void Start()
    {
        StartCoroutine(setWeaponSpawn());
    }

    private void Update()
    {
        if (gunCore.transform.localScale.x >= 0.4f) 
        {
            gunCore.transform.localScale = Vector2.zero;

        }
        if(gunCore.transform.localScale.x >= 0) 
        {
            gunCore.transform.localScale = Vector2.MoveTowards(gunCore.transform.localScale, new Vector2(0.4f, 0.4f), delayTime * Time.deltaTime);
        }
    }
    IEnumerator setWeaponSpawn()
    {

        while (true)
        {
            float waitForNextSpawn = distanceBurstBulletSpawn / burstObjectCount;

            if (gunCore.transform.localScale.x >= 0.4f)
            {
                if (isBurstSpawn)
                {
                    for (int j = 0; j < burstObjectCount; j++)
                    {
                        spawnBullet();
                        yield return new WaitForSeconds(waitForNextSpawn);
                    }
                }
                else
                {
                    spawnBullet();
                }
            }

            yield return null;
        }
    }

    private void spawnBullet()
    {
        Instantiate(weaponBullet, bulletSpawner.transform.position, Quaternion.identity);
    }
}
