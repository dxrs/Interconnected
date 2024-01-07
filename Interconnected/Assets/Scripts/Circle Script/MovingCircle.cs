using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCircle : MonoBehaviour
{
    public static MovingCircle movingCircle;

    public bool isMoving;
    public bool circleTriggertWithDoor;

    [SerializeField] GlobalVariable globalVariable;

    [SerializeField] float movementSpeed;

    [SerializeField] Transform[] waypoint;
    [SerializeField] Transform circlePath;

    [SerializeField] GameObject circle;
    [SerializeField] GameObject circleTarget;

    [SerializeField] SpriteRenderer circleSR;

    int startWaypoint = 0;

    private void Awake()
    {
        if (movingCircle == null) { movingCircle = this; }
    }

    private void Start()
    {
        circle.transform.position = waypoint[startWaypoint].transform.position;
    }

    private void Update()
    {
        if (isMoving && !circleTriggertWithDoor) 
        {
            circle.transform.position = Vector2.MoveTowards(circle.transform.position,
                waypoint[startWaypoint].transform.position,
                movementSpeed * Time.deltaTime);

            circleSR.color = Color.Lerp(circleSR.color, new Color(1, 1, 1), 3 * Time.deltaTime);
        }
        else 
        {
            circleSR.color = Color.Lerp(circleSR.color, new Color(.5f, .5f, .5f), 1 * Time.deltaTime);
        }
        if (circle.transform.position == waypoint[startWaypoint].transform.position)
        {
            startWaypoint += 1;
        }

        if (startWaypoint == waypoint.Length && !globalVariable.isGameFinish) 
        {
            // starWaypoint = index terakhir dari waypoint.length
            startWaypoint = waypoint.Length - 1;
        }
        if (globalVariable.isGameFinish) 
        {
            circle.transform.position = circleTarget.transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Vector2 startPos = circlePath.GetChild(0).position;
        Vector2 prevPos = startPos;
        foreach(Transform waypoint in circlePath) 
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevPos, waypoint.position);
            Gizmos.color = Color.blue;
            prevPos = waypoint.position;
        }
    }
}
