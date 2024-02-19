using System.Collections;
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
        Camera mainCamera = Camera.main;

        if (isBurstSpawn)
        {
            yield return new WaitForSeconds(burstDelayObjectSpawn);
        }

        while (true)
        {
            float waitForNextSpawn = distanceBurstBulletSpawn / burstObjectCount;

            if (isBurstSpawn)
            {
                for (int j = 0; j < burstObjectCount; j++)
                {
                    GameObject bullet = Instantiate(weaponBullet, bulletSpawner.transform.position, Quaternion.identity);

                    // Periksa apakah peluru yang diinstansiasi terlihat oleh kamera
                    if (IsObjectVisible(bullet, mainCamera))
                    {
                        yield return new WaitForSeconds(waitForNextSpawn);
                    }
                    else
                    {
                        // Jika tidak terlihat, hancurkan peluru
                        Destroy(bullet);
                    }
                }
            }
            else
            {
                GameObject bullet = Instantiate(weaponBullet, bulletSpawner.transform.position, Quaternion.identity);

                if (!IsObjectVisible(bullet, mainCamera))
                {
                    Destroy(bullet);
                }
            }

            yield return new WaitForSeconds(delayTime);
        }
    }

    bool IsObjectVisible(GameObject obj, Camera camera)
    {
        Renderer renderer = obj.GetComponent<Renderer>();

        if (renderer != null)
        {
            Plane[] planes = GeometryUtility.CalculateFrustumPlanes(camera);
            return GeometryUtility.TestPlanesAABB(planes, renderer.bounds);
        }

        // Jika objek tidak memiliki renderer, anggap objek tersebut terlihat
        return true;
    }
}
