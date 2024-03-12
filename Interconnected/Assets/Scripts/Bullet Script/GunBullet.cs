using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] int idBullet;

    [SerializeField] float bulletMoveSpeed;

    [SerializeField] string moveDirType;

    [SerializeField] string[] bulletCollisionTag;



    private void Update()
    {
        Vector2[] bulletDirections = new Vector2[]
        {
            new Vector2(0, bulletMoveSpeed),
            new Vector2(0, -bulletMoveSpeed),
            new Vector2(bulletMoveSpeed, 0),
            new Vector2(-bulletMoveSpeed, 0),
            new Vector2(bulletMoveSpeed, bulletMoveSpeed),
            new Vector2(-bulletMoveSpeed, bulletMoveSpeed),
            new Vector2(bulletMoveSpeed, -bulletMoveSpeed),
            new Vector2(-bulletMoveSpeed, -bulletMoveSpeed)
        };
        for (int i = 0; i < bulletDirections.Length; i++)
        {
            if (idBullet == i + 1)
            {
                transform.Translate(bulletDirections[i] * Time.deltaTime);
                break; // Exit the loop once the correct direction is found
            }
        }

        Destroy(gameObject, 30);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        for (int j = 0; j < bulletCollisionTag.Length; j++)
        {
            if (collision.gameObject.CompareTag(bulletCollisionTag[j])) 
            {
                Destroy(gameObject);
            }
        }
    }
}
