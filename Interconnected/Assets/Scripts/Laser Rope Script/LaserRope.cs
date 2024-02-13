using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserRope : MonoBehaviour
{
    [SerializeField] GameObject laerRope;
    [SerializeField] GameObject playerObjectRotation;

    GameObject player1, player2;

    float maxDistanceBeam;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }



    private void Update()
    {
        if (playerObjectRotation != null) 
        {
            transform.position = playerObjectRotation.transform.position;
            transform.rotation = playerObjectRotation.transform.rotation;
        }
       
        if(player1 && player2 != null) 
        {
            if (LinkRay.linkRay.isPlayerLinkedEachOther)
            {
                laerRope.SetActive(true);
                float beamDistance = Vector2.Distance(player1.transform.position, player2.transform.position);
                maxDistanceBeam = beamDistance;
                transform.localScale = new Vector3(.1f, maxDistanceBeam, transform.localScale.z);
            }
            else
            {
                laerRope.SetActive(false);
            }
        }
      

    }
}
