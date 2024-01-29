using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public int curCheckpointValue;

    [SerializeField] int maxCheckpointValue;

    [SerializeField] Vector2[] player1CheckpointPos;
    [SerializeField] Vector2[] player2CheckpointPos;

    [SerializeField] GameObject player1;
    [SerializeField] GameObject player2;

    private void Start()
    {
        curCheckpointValue = maxCheckpointValue;
    }
}
