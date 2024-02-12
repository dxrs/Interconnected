using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int curCheckpointValue;

    [SerializeField] int maxCheckpointValue;

    [SerializeField] Transform[] player1CheckpintPos;
    [SerializeField] Transform[] player2CheckpintPos;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

   
    private void Start()
    {
        curCheckpointValue = 1;

        player1.transform.position = player1CheckpintPos[0].position;
        player2.transform.position = player2CheckpintPos[0].position;
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID != 2) 
        {
            if (GlobalVariable.globalVariable.isTriggeredWithObstacle)
            {
                StartCoroutine(moveToPos());
            }
        }
       
    }

    IEnumerator moveToPos()
    {
        yield return new WaitForSeconds(.5f);
        int index = curCheckpointValue - 1;

        if (index >= 0 && index < player1CheckpintPos.Length)
        {
            player1.transform.position = player1CheckpintPos[index].position;
            player2.transform.position = player2CheckpintPos[index].position;
        }


    }

}
