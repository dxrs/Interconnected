using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkRay : MonoBehaviour
{
    // this script contains link point for each player only

    public bool isTouchObstacle;
    public bool isLinkedToPlayer;

    [SerializeField] float linkDistance;

    [SerializeField] GameObject[] player; // array for link point player 1 and player 2

    [SerializeField] LayerMask layerMask;

    [SerializeField] string[] playerObstacleTag;

 
    private GameObject[] obstacleP1;
    private GameObject[] obstacleP2;

    private void Start()
    {
        isLinkedToPlayer = true;
    }

    private void Update()
    {
        obstacleP1 = GameObject.FindGameObjectsWithTag(playerObstacleTag[0]);
        obstacleP2 = GameObject.FindGameObjectsWithTag(playerObstacleTag[1]);
        linkPointPlayerDetection();
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
                if (hit.collider.gameObject.tag == "Obstacle" || 
                    hit.collider.gameObject.tag=="Obstacle P1"||
                    hit.collider.gameObject.tag=="Obstacle P2"||
                    Vector2.Distance(player[0].transform.position,
                    player[1].transform.position) >= linkDistance)
                {

                    Debug.DrawLine(player[0].transform.position,
                        player[1].transform.position,
                        Color.red);
                }
                else
                {
                    Debug.DrawLine(player[0].transform.position,
                        player[1].transform.position,
                        Color.green);

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

     
        float distanceBetweenPlayers = Vector2.Distance(player[1].transform.position, player[0].transform.position);
        for (int i = 0; i < obstacleP1.Length; i++)
        {

            float distance = Vector2.Distance(player[0].transform.position, obstacleP1[i].transform.position);

            
            if (distance < linkDistance && distance < nearestDistance)
            {
                RaycastHit2D hitBetweenPlayers = Physics2D.Linecast(player[0].transform.position,
                    player[1].transform.position, layerMask);

                RaycastHit2D hitP1 = Physics2D.Linecast(player[0].transform.position,
                   obstacleP1[i].transform.position, layerMask);
                if (hitP1.collider != null && hitP1.collider.tag == playerObstacleTag[0] && hitP1.collider.tag != "Obstacle")
                {
                    nearestDistance = distance;
                    nearestObstacle = obstacleP1[i];
                }
                if (hitBetweenPlayers.collider == null)
                {
                   
                }


            }
        }
        if (distanceBetweenPlayers > 5)
        {
            
        }

     
        if (nearestObstacle != null)
        {
            Debug.DrawLine(player[0].transform.position, nearestObstacle.transform.position, Color.green);
        }

       
    }

    private void linkObstacleP2() 
    {
        float nearestDistance = float.MaxValue;
        GameObject nearestObstacle = null;

        //periksa apakah player 1 berada di antara player 2 dan obstacleP2
        float distanceBetweenPlayers = Vector2.Distance(player[1].transform.position, player[0].transform.position);
        for (int i = 0; i < obstacleP2.Length; i++)
        {
            float distance = Vector2.Distance(player[1].transform.position, obstacleP2[i].transform.position);
            //apakah objek saat ini berada dalam rentang dan lebih dekat dari objek terdekat sebelumnya
            if (distance < linkDistance && distance < nearestDistance)
            {
                RaycastHit2D hitBetweenPlayers = Physics2D.Linecast(player[0].transform.position,
                    player[1].transform.position, layerMask);
                RaycastHit2D hitP2 = Physics2D.Linecast(player[1].transform.position, obstacleP2[i].transform.position, layerMask);

                if (hitBetweenPlayers.collider == null)
                {

                }
                if (hitP2.collider != null && hitP2.collider.tag == playerObstacleTag[1] && hitP2.collider.tag != "Obstacle")
                {
                    nearestDistance = distance;
                    nearestObstacle = obstacleP2[i];
                }
            }
        }
        if (distanceBetweenPlayers > 5)
        {
            
        }

        // trigger sesuatu dengan objek penghalang terdekat
        // sementara membuat draw line untuk memeriksa apakah player 2 sudah terhubung dengan obstacleP2
        if (nearestObstacle != null)
        {
            Debug.DrawLine(player[1].transform.position, nearestObstacle.transform.position, Color.green);
        }
       
    }
}
