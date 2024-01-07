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
    [SerializeField] Transform pathWall;
    [SerializeField] float moveSpeed;
    [SerializeField] float delayTime;

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
            Vector2[] waypoint = new Vector2[pathWall.childCount];
            for(int i = 0; i < waypoint.Length; i++) 
            {
                waypoint[i] = pathWall.GetChild(i).position;
            }
            StartCoroutine(wallFollowWaypoint(waypoint));
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
    }

    IEnumerator wallFollowWaypoint(Vector2[] waypoint) 
    {
        transform.position = waypoint[0];
        int targetWaypointIndex = 0;
        Vector3 targetWaypoint = waypoint[targetWaypointIndex];

        while (true) 
        {
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint, moveSpeed * Time.deltaTime);
            if (transform.position == targetWaypoint) 
            {
                targetWaypointIndex = (targetWaypointIndex + 1) % waypoint.Length;
                targetWaypoint = waypoint[targetWaypointIndex];
                yield return new WaitForSeconds(delayTime);
            }
            yield return null;
        }
    }

    private void OnDrawGizmos()
    {
        if(wallType=="Move Towards") 
        {
            Vector2 startPos = pathWall.GetChild(0).position;
            Vector2 prevPos = startPos;
            foreach (Transform waypoint in pathWall)
            {
                Gizmos.DrawSphere(waypoint.position, .5f);
                Gizmos.DrawLine(prevPos, waypoint.position);
                prevPos = waypoint.position;
            }
            Gizmos.DrawLine(prevPos, startPos);
        }
        
    }
}
