using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunBullet : MonoBehaviour
{
    [SerializeField] float bulletMoveSpeed;

    [SerializeField] string moveDirType;

    [SerializeField] string[] bulletCollisionTag;

    private void Update()
    {
        transform.Translate(new Vector2(0, bulletMoveSpeed * Time.deltaTime));
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
