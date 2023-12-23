using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerTrigger : MonoBehaviour
{
    [SerializeField] SurvivalArea survivalArea;

    [SerializeField] string doorType;

    [SerializeField] int id;

    [SerializeField] bool isTriggered;

    private void Update()
    {
        if (doorType == "Enter")
        {
            for (int i = 1; i <= SpawnerValue.spawnerValue.spawnerTriggerMaxValueEnter; i++)
            {
                if (isTriggered) 
                {
                    if (id == i && Player1.player1.player1DoorValue == 1 && Player2.player2.player2DoorValue == 1)
                    {
                        survivalArea.enemySpawner.SetActive(true);
                        survivalArea.doorBlocker[0].SetActive(true);
                        Destroy(gameObject);
                        break; // Keluar dari loop jika kondisi terpenuhi
                    }
                }
                
            }
        }
        if (doorType == "Exit")
        {
            for (int i = 1; i <= SpawnerValue.spawnerValue.spawnerTriggerMaxValueExit; i++)
            {
                if (isTriggered)
                {
                    if (id == i && Player1.player1.player1DoorValue == 1 && Player2.player2.player2DoorValue == 1)
                    {
                        survivalArea.doorBlocker[1].SetActive(true);
                        Destroy(gameObject);
                        break; // Keluar dari loop jika kondisi terpenuhi
                    }
                }

            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorType == "Enter")
        {
            if (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2"))
            {
                for (int i = 1; i <= SpawnerValue.spawnerValue.spawnerTriggerMaxValueExit; i++)
                {
                    if (id == i)
                    {
                        survivalArea.isStartingToSurvive = true;
                        isTriggered = true;
                        break; // Keluar dari loop setelah menemukan id yang sesuai
                    }
                }
            }
        }
        if (doorType == "Exit")
        {
            if (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2"))
            {
                for (int i = 1; i <= SpawnerValue.spawnerValue.spawnerTriggerMaxValueExit; i++)
                {
                    if (id == i)
                    {
                        isTriggered = true;
                        break; // Keluar dari loop setelah menemukan id yang sesuai
                    }
                }
            }
        }
        
       
       
    }
}
