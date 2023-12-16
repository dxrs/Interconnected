using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Movement")]
    [SerializeField] float enemyMovementEnemySpeed;

    [Header("Enemy Status")]
    [SerializeField] float enemyHealth;

    #region enemy hit effecy
    [Header("Enemy Hit Effect")]
    [SerializeField] private Material flashMaterial;
    [SerializeField] private float duration;
    private SpriteRenderer spriteRenderer;
    private Material originalMaterial;
    private Coroutine flashRoutine;
    #endregion

    private void Update()
    {
        if (enemyHealth <= 0) 
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Bullet P1" || collision.gameObject.tag=="Bullet P2") 
        {
            enemyHealth -= 1;
        }
    }
}
