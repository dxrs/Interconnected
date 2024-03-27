using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallWaypointMove : MonoBehaviour
{
    [SerializeField] Transform pathWall;

    [SerializeField] float moveSpeed;
    [SerializeField] float delayTime;
    [SerializeField] float rotationSpeed;

    [SerializeField] bool isHasRotation;

    private void Start()
    {
        Vector2[] waypoint = new Vector2[pathWall.childCount];
        for (int i = 0; i < waypoint.Length; i++)
        {
            waypoint[i] = pathWall.GetChild(i).position;
        }
        StartCoroutine(wallFollowWaypoint(waypoint));
    }

    IEnumerator wallFollowWaypoint(Vector2[] waypoint)
    {
        transform.position = waypoint[0];
        int targetWaypointIndex = 0;
        Vector3 targetWaypoint = waypoint[targetWaypointIndex];

        while (true)
        {

            if (isHasRotation) 
            {
                Vector2 direction = (targetWaypoint - transform.position).normalized;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.AngleAxis(angle, Vector3.forward), rotationSpeed * Time.deltaTime);
            }
            
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
