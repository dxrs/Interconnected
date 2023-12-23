using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnExitTrigger : MonoBehaviour
{
    [SerializeField] SurvivalArea survivalArea;

    [SerializeField] int id;

    [SerializeField] bool isTriggered;

    private void Update()
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2"))
        {
            for (int i = 1; i <= SpawnerValue.spawnerValue.spawnerTriggerMaxValueExit; i++)
            {
                if (id == i)
                {
                    GlobalVariable.globalVariable.isEnteringSurvivalArea = false;
                    isTriggered = true;
                    break; // Keluar dari loop setelah menemukan id yang sesuai
                }
            }
        }
    }
}
