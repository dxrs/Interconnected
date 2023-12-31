using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSine : MonoBehaviour
{
    [SerializeField] string sineType;

    [SerializeField] float sineSpeed;
    [SerializeField] float sinePower;

    [SerializeField] bool isRandomPosSineOffset;

    float randomOffsetSinePos;

    private float xPos;
    private float yPos;
    private float time;

    Vector2 pos;

    private void Start()
    {
        randomOffsetSinePos = Random.Range(0, 2);
        pos = transform.position;
        xPos = transform.position.x;
        yPos = transform.position.y;
    }
    private void Update()
    {
        transform.position =  new Vector2(pos.x + Mathf.Sin(sineSpeed * Time.time) * sinePower, 
            transform.position.y);
    }
}
