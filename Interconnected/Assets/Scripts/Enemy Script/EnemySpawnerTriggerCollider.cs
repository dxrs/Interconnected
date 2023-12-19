using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTriggerCollider : MonoBehaviour
{
    public int id;
    private int playersInsideCollider = 0;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2"))
        {
            playersInsideCollider++;
            UpdateSpawnerValue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2"))
        {
            playersInsideCollider--;
            UpdateSpawnerValue();
        }
    }

    private void UpdateSpawnerValue()
    {
        SpawnerValue.spawnerValue.spawnerValuerIndex[id - 1] = (playersInsideCollider > 0) ? 1 : 0; // struktur kondisional ternary
    }
}
