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

    [SerializeField] Rigidbody2D rb;

    int startWaypoint = 0;

    Vector2 velocity;

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
        //Debug.Log(rb.drag);
        if (isMoving && !circleTriggertWithDoor)
        {
            Vector2 targetPosition = waypoint[startWaypoint].transform.position;

            // Hitung vektor arah dan jarak
            Vector2 moveDirection = (targetPosition - (Vector2)circle.transform.position).normalized;
            float distance = Vector2.Distance(circle.transform.position, targetPosition);

            // Hitung target velocity menggunakan SmoothDamp
            Vector2 targetVelocity = moveDirection * movementSpeed;
            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref velocity, 0.1f);

            circleSR.color = Color.Lerp(circleSR.color, new Color(1, 1, 1), 3 * Time.deltaTime);
        }
        else
        {
            circleSR.color = Color.Lerp(circleSR.color, new Color(.5f, .5f, .5f), 1 * Time.deltaTime);

            // Proses pengereman
            //rb.drag = Mathf.Lerp(rb.drag, 5, 1 * Time.deltaTime);
            rb.drag = 5;
        }

        
        if (Vector2.Distance(circle.transform.position, waypoint[startWaypoint].transform.position) < 0.1f)
        {
            startWaypoint = (startWaypoint + 1) % waypoint.Length;
        }

        if (startWaypoint == waypoint.Length && !globalVariable.isGameFinish)
        {
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
        foreach (Transform waypoint in circlePath)
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevPos, waypoint.position);
            Gizmos.color = Color.blue;
            prevPos = waypoint.position;
        }
    }
}
