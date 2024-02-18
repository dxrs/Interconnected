using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public static Checkpoint checkpoint;

    public int curCheckpointValue;

    [SerializeField] int maxCheckpointValue;

    [SerializeField] Transform[] player1CheckpintPos;
    [SerializeField] Transform[] player2CheckpintPos;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    private void Awake()
    {
        checkpoint = this;
    }
    private void Start()
    {
        curCheckpointValue = 1;

        player1.transform.position = player1CheckpintPos[0].position;
        player2.transform.position = player2CheckpintPos[0].position;
    }

    private void Update()
    {

        if (LevelStatus.levelStatus.levelID != 3)
        {
            if (GlobalVariable.globalVariable.isPlayerDestroyed && !GameOver.gameOver.isGameOver)
            {
                StartCoroutine(moveToPos());
            }
        }
        if (LevelStatus.levelStatus.levelID == 4)
        {
            if (Tutorial.tutorial.cameraMoveValue == 3)
            {
                player1CheckpintPos[0].position = new Vector2(51.4f, -2.29f);
                player2CheckpintPos[0].position = new Vector2(70.1f, -12.25f);
            }
        }
    }

    IEnumerator moveToPos()
    {
        if (!GameOver.gameOver.isGameOver) 
        {
            yield return new WaitForSeconds(.5f);
            int index = curCheckpointValue - 1;

            if (index >= 0 && index < player1CheckpintPos.Length)
            {
                if (player1 && player2 != null)
                {
                    player1.transform.position = player1CheckpintPos[index].position;
                    player2.transform.position = player2CheckpintPos[index].position;
                }

            }
        }
       


    }

}
