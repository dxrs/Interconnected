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

   [SerializeField] Rigidbody2D rb2D;
    int startWaypoint = 0;

    private void Awake()
    {
        if (movingCircle == null) { movingCircle = this; }
       // rb2D = circle.GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        circle.transform.position = waypoint[startWaypoint].transform.position;
    }

    private void Update()
    {
        if (isMoving && !circleTriggertWithDoor)
        {
            // Batasi kecepatan maksimum
            if (rb2D.velocity.magnitude < movementSpeed)
            {
                Vector2 targetPosition = waypoint[startWaypoint].transform.position;

                // Batasi kecepatan maksimum
                Vector2 moveDirection = (targetPosition - (Vector2)circle.transform.position).normalized;
                rb2D.MovePosition(rb2D.position + moveDirection * movementSpeed * Time.deltaTime);
            }

            circleSR.color = Color.Lerp(circleSR.color, new Color(1, 1, 1), 3 * Time.deltaTime);
        }
        else
        {
            circleSR.color = Color.Lerp(circleSR.color, new Color(.5f, .5f, .5f), 1 * Time.deltaTime);

            // Berhenti ketika mencapai waypoint
            rb2D.drag = 5f;
        }

        // Jika posisi objek mendekati waypoint, pindah ke waypoint berikutnya
        if (Vector2.Distance(circle.transform.position, waypoint[startWaypoint].transform.position) < 0.1f)
        {
            startWaypoint = (startWaypoint + 1) % waypoint.Length;
            rb2D.drag = 0f;
        }

        if (startWaypoint == waypoint.Length && !globalVariable.isGameFinish)
        {
            // startWaypoint = index terakhir dari waypoint.length
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
