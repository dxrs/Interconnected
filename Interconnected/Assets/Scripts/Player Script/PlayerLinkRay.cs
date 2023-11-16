using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLinkRay : MonoBehaviour
{
    // this script contains link point for each player only

    public bool isTouchObstacle;

    [SerializeField] float linkDistance;

    [SerializeField] GameObject[] linePointTarget; // array for link point player 1 and player 2

    [SerializeField] LayerMask layerMask;

    private void Update()
    {
        linkPointPlayerDetection();
    }

    private void linkPointPlayerDetection() 
    {
        RaycastHit2D hit = Physics2D.Linecast(linePointTarget[0].transform.position,
            linePointTarget[1].transform.position,
            layerMask);

        if (hit.collider != null) 
        {
            if (hit.collider.gameObject.tag == "Obstacle" ||
                Vector2.Distance(linePointTarget[0].transform.position, 
                linePointTarget[1].transform.position) >= linkDistance) 
            {
                
                Debug.DrawLine(linePointTarget[0].transform.position,
                    linePointTarget[1].transform.position,
                    Color.red);
            }
            else 
            {
                Debug.DrawLine(linePointTarget[0].transform.position,
                    linePointTarget[1].transform.position,
                    Color.green);

            }
        }
    }
}
