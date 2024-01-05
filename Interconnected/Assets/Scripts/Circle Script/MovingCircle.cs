using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCircle : MonoBehaviour
{
    public static MovingCircle movingCircle;

    public bool isMoving;
    public bool circleTriggertWithDoor;

    [SerializeField] float movementSpeed;

    [SerializeField] Transform[] waypoint;

    [SerializeField] GameObject circle;

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

        if (startWaypoint == waypoint.Length) 
        {
            // starWaypoint = index terakhir dari waypoint.length
            startWaypoint = waypoint.Length - 1;
        }
    }

    
}
