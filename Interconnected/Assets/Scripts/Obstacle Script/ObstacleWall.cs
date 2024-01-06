using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWall : MonoBehaviour
{
    [SerializeField] string wallType;

    [Header("Sine Wall")]
    [SerializeField] int sineID;
    [SerializeField] float sineSpeed;
    [SerializeField] float sinePower;
    [SerializeField] bool isSineRandomOffset;
    float randomOffsetSinePos;
    float xSinePos;
    float ySinePos;

    [Header("Move Towards Wall")]
    [SerializeField] Transform[] wallWaypoint;
    [SerializeField] int startWaypoint;
    [SerializeField] float moveSpeed;

    Vector2 pos;

    private void Start()
    {
        if (wallType == "Sine") 
        {
            pos = transform.position;
            xSinePos = transform.position.x;
            ySinePos = transform.position.y;
        }

        if(wallType=="Move Towards") 
        {
            transform.position = wallWaypoint[startWaypoint].transform.position;
        }
    }
    private void Update()
    {
        if (wallType == "Sine") 
        {
            if (sineID == 1) 
            {
                transform.position = new Vector2(pos.x + Mathf.Sin(sineSpeed * Time.time) * sinePower,
       transform.position.y);
            }
            if (sineID == 2) 
            {
                transform.position = new Vector2(transform.position.x, pos.y + (Mathf.Sin(sineSpeed * Time.time) * sinePower));
            }
        
        }
        if(wallType=="Move Towards") 
        {
            transform.position = Vector2.MoveTowards(transform.position,
                wallWaypoint[startWaypoint].transform.position,
                moveSpeed * Time.deltaTime);

            if (transform.position == wallWaypoint[startWaypoint].transform.position) 
            {
                startWaypoint += 1;
            }

            if (startWaypoint == wallWaypoint.Length) 
            {
                startWaypoint = 0;
            }
        }
    }
}
