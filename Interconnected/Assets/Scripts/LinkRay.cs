using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkRay : MonoBehaviour
{
    // this script contains link point for each player only

    public static LinkRay linkRay;

    public bool isPlayerLinkedEachOther;

    public float maxLinkDistance;

    [SerializeField] GameObject[] player; // array for link point player 1 and player 2

    [SerializeField] LayerMask layerMask;

    [SerializeField] string[] obstacleTag;

    private void Awake()
    {
        linkRay = this;
    }
   

    private void Update()
    {
       
        if (player[0] != null && player[1] != null) 
        {
            linkPointPlayerDetection();
        }
       
    }

    private void linkPointPlayerDetection() 
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
                Vector2.Distance(player[0].transform.position, player[1].transform.position) >= maxLinkDistance)
            {
                isPlayerLinkedEachOther = false;
                Debug.DrawLine(player[0].transform.position, player[1].transform.position, Color.red);
                Destroy(GameObject.FindGameObjectWithTag("Bullet P1"));
            }
            else
            {
                isPlayerLinkedEachOther = true;
                Debug.DrawLine(player[0].transform.position, player[1].transform.position, Color.green);
            }



        }

        
        
    }

    
}
