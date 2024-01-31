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

    public float linkDistanceToPlayer;
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


               
            }
            if (LevelStatus.levelStatus.levelID == 1) 
            {
                if (hit.collider != null &&
                  hit.collider.tag == "Trash Bin" &&
                  Vector2.Distance(player[0].transform.position,
                  player[1].transform.position) <= linkDistanceToCircle &&
                  !GlobalVariable.globalVariable.circleIsTriggeredWithPlayers[0] &&
                  !GlobalVariable.globalVariable.circleIsTriggeredWithPlayers[1] &&
                  !GlobalVariable.globalVariable.isTriggeredWithObstacle)
                {
                    //MovingCircle.movingCircle.isMoving = true;
                }
                else
                {
                   // MovingCircle.movingCircle.isMoving = false;
                }
            }
           
        }
        if (!isLinkedToPlayer) 
        {
            //linkObstacleP1();
            //linkObstacleP2();
        }
    }

    
}
