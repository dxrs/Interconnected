using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleLinkRay : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    [SerializeField] int obstacleRayID;

    //[SerializeField] LayerMask layerMask;

    private void Update()
    {
        if (!linkRay.isChangeLinkMethod) 
        {
            //gameObject.layer = LayerMask.NameToLayer("Default");


        }
        else 
        {
            if (obstacleRayID == 1) 
            {
                //gameObject.layer = LayerMask.NameToLayer("Obstacle P1");
            }
            if(obstacleRayID==2)
            {
                //gameObject.layer = LayerMask.NameToLayer("Obstacle P2");
            }
        }
    }
}
