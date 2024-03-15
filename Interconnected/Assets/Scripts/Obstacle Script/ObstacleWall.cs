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

    [Header("Waypoint Movement Wall")]
    [SerializeField] Transform pathWall;
    [SerializeField] float moveSpeed;
    [SerializeField] float delayTime;

    [Header("Move Toward Wall")]
    [SerializeField] int moveTowardID;
    [SerializeField] bool isMovingX;
    [SerializeField] float moveTowardSpeed;
    [SerializeField] float moveXTarget;
    [SerializeField] float moveYTarget;

    [Header("Rotate Wall")]
    [SerializeField] bool isRotateClockwise;
    [SerializeField] float rotationSpeed;

    Vector2 pos;

    GameObject player1, player2;

    
 
    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        randomOffsetSinePos = Random.Range(0, 2);
        if (wallType == "Sine") 
        {
            pos = transform.position;
            xSinePos = transform.position.x;
            ySinePos = transform.position.y;
        }

        if(wallType=="Waypoint Movement") 
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
        wallSine();
        wallMoveTowardTutorial();

        if (wallType == "Rotate") 
        {
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }

        
    }

    private void wallMoveTowardTutorial() 
    {
        if (LevelStatus.levelStatus.levelID == 4)
        {
            if (Tutorial.tutorial.tutorialProgress >= 2)
            {
                if (moveTowardID == 1)
                {
                    if (player1.transform.position.x >= 6 || player2.transform.position.x >= 6)
                    {
                        if (isMovingX)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveXTarget, transform.position.y), moveTowardSpeed * Time.deltaTime);
                        }
                    }
                }
                if (moveTowardID == 3)
                {
                    if (Tutorial.tutorial.shareLivesProgress == 1)
                    {
                        if (isMovingX)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveXTarget, transform.position.y), moveTowardSpeed * Time.deltaTime);
                        }
                    }
                }


            }
            if (Tutorial.tutorial.tutorialProgress >= 3)
            {
                if (moveTowardID == 2)
                {
                    if (Tutorial.tutorial.isPlayersEnterGarbageArea[0] || Tutorial.tutorial.isPlayersEnterGarbageArea[1])
                    {
                        if (isMovingX)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveXTarget, transform.position.y), moveTowardSpeed * Time.deltaTime);
                        }
                    }
                }


            }
        }
    }

    private void wallSine() 
    {
        if (wallType == "Sine")
        {
            if (sineID == 1)
            {
                if (isSineRandomOffset) 
                {
                    transform.position = new Vector2(pos.x + Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower, transform.position.y);
                }
                else 
                {
                    transform.position = new Vector2(pos.x + Mathf.Sin(sineSpeed * Time.time) * sinePower, transform.position.y);

                }
         
            }
            if (sineID == 2)
            {
                if (isSineRandomOffset) 
                {
                    transform.position = new Vector2(transform.position.x, pos.y + (Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower));
                }
                else 
                {
                    transform.position = new Vector2(transform.position.x, pos.y + (Mathf.Sin(sineSpeed * Time.time) * sinePower));
                }
                
            }
            if (sineID == 3) 
            {
                transform.position = pos + new Vector2(Mathf.Sin(-sineSpeed * Time.time) * sinePower,
                     Mathf.Sin(sineSpeed * Time.time) * sinePower);
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
        if(wallType== "Waypoint Movement") 
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
