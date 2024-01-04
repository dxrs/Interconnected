using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkRay : MonoBehaviour
{
    // this script contains link point for each player only

    public static LinkRay linkRay;

    public bool isTouchObstacle;
    public bool playerLinkedEachOther;
    public bool player1LinkedToObstacle, player2LinkedToObstacle;
    public bool isLinkedToPlayer;

    [SerializeField] float linkDistanceToPlayer;
    [SerializeField] float linkDistanceToCircle;

    [SerializeField] GameObject[] player; // array for link point player 1 and player 2

    [SerializeField] LayerMask layerMask;

    [SerializeField] string[] playerObstacleTag;
    [SerializeField] string[] obstacleTag;
 
    private GameObject[] obstacleP1;
    private GameObject[] obstacleP2;

    private void Awake()
    {
        linkRay = this;
    }
    private void Start()
    {
        isLinkedToPlayer = true;
    }

    private void Update()
    {
        obstacleP1 = GameObject.FindGameObjectsWithTag(playerObstacleTag[0]);
        obstacleP2 = GameObject.FindGameObjectsWithTag(playerObstacleTag[1]);
        if (player[0] != null && player[1] != null) 
        {
            linkPointPlayerDetection();
        }
       
    }

    private void linkPointPlayerDetection() 
    {
        if (isLinkedToPlayer)
        {

            RaycastHit2D hit = Physics2D.Linecast(player[0].transform.position,
            player[1].transform.position,
            layerMask);

            if (hit.collider != null)
            {
                bool isObstacleHit = false;
                for (int x = 0; x < obstacleTag.Length; x++)
                {
                    if (hit.collider.gameObject.tag == obstacleTag[x])
                    {
                        isObstacleHit = true;
                        break;
                    }
                }
                if (isObstacleHit ||
                    Player1.player1.isGhosting ||
                    Player2.player2.isGhosting ||
                    Vector2.Distance(player[0].transform.position, player[1].transform.position) >= linkDistanceToPlayer)
                {
                    playerLinkedEachOther = false;
                    Debug.DrawLine(player[0].transform.position, player[1].transform.position, Color.red);
                    Destroy(GameObject.FindGameObjectWithTag("Bullet P1"));
                }
                else
                {
                    playerLinkedEachOther = true;
                    Debug.DrawLine(player[0].transform.position, player[1].transform.position, Color.green);
                }


                if (hit.collider != null && 
                    hit.collider.tag == "Moving Circle" &&
                    Vector2.Distance(player[0].transform.position,
                    player[1].transform.position) <= linkDistanceToCircle &&
                    !GlobalVariable.globalVariable.circleIsTriggeredWithPlayers[0] &&
                    !GlobalVariable.globalVariable.circleIsTriggeredWithPlayers[1])
                {
                    MovingCircle.movingCircle.isMoving = true;
                }
                else
                {
                    MovingCircle.movingCircle.isMoving = false;
                }
            }
        }
        if (!isLinkedToPlayer) 
        {
            linkObstacleP1();
            linkObstacleP2();
        }
    }

    private void linkObstacleP1()
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestObstacle = null;
        player1LinkedToObstacle = false;

        for (int i = 0; i < obstacleP1.Length; i++)
        {

            float distance = Vector2.Distance(player[0].transform.position, obstacleP1[i].transform.position);

            if (distance < linkDistanceToPlayer && distance < nearestDistance)
            {
                RaycastHit2D hitBetweenPlayers = Physics2D.Linecast(player[0].transform.position,
                    player[1].transform.position, layerMask);

                RaycastHit2D hitP1 = Physics2D.Linecast(player[0].transform.position,
                   obstacleP1[i].transform.position, layerMask);
                if (hitP1.collider != null 
                    && hitP1.collider.tag == playerObstacleTag[0] 
                    && hitP1.collider.tag != "Obstacle"
                    && !Player1.player1.isGhosting)
                {
                    player1LinkedToObstacle = true;
                    nearestDistance = distance;
                    nearestObstacle = obstacleP1[i];
                }
             
            }
        }
      
        if (nearestObstacle != null && !Player1.player1.isGhosting)
        {
            Debug.DrawLine(player[0].transform.position, nearestObstacle.transform.position, Color.green);
        }
    }

    private void linkObstacleP2() 
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestObstacle = null;
        player2LinkedToObstacle = false;

        //periksa apakah player 1 berada di antara player 2 dan obstacleP2
        float distanceBetweenPlayers = Vector2.Distance(player[1].transform.position, player[0].transform.position);
        for (int i = 0; i < obstacleP2.Length; i++)
        {
            float distance = Vector2.Distance(player[1].transform.position, obstacleP2[i].transform.position);
            //apakah objek saat ini berada dalam rentang dan lebih dekat dari objek terdekat sebelumnya
            if (distance < linkDistanceToPlayer && distance < nearestDistance)
            {
                RaycastHit2D hitBetweenPlayers = Physics2D.Linecast(player[0].transform.position,
                    player[1].transform.position, layerMask);
                RaycastHit2D hitP2 = Physics2D.Linecast(player[1].transform.position, obstacleP2[i].transform.position, layerMask);

                if (hitP2.collider != null 
                    && hitP2.collider.tag == playerObstacleTag[1] 
                    && hitP2.collider.tag != "Obstacle"
                    && !Player2.player2.isGhosting)
                {
                    player2LinkedToObstacle = true;
                    nearestDistance = distance;
                    nearestObstacle = obstacleP2[i];
                }

            }
        }

        // trigger sesuatu dengan objek penghalang terdekat
        // sementara membuat draw line untuk memeriksa apakah player 2 sudah terhubung dengan obstacleP2
        if (nearestObstacle != null && !Player2.player2.isGhosting)
        {
            Debug.DrawLine(player[1].transform.position, nearestObstacle.transform.position, Color.green);
        }
       
    }
}
