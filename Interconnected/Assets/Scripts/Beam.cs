using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beam : MonoBehaviour
{
    public Transform objekTarget;
    [SerializeField] float maxDistanceBeam;
    [SerializeField] float currentScaleYbeam;

    Vector2 beamScale;

    private void Start()
    {

        //currentScaleYbeam = transform.localScale.y;
    }
    private void Update()
    {
        
        
        float test = Vector2.Distance(transform.position, objekTarget.position);
        maxDistanceBeam = test;
        transform.localScale = new Vector3(1, maxDistanceBeam, 1);
        if (currentScaleYbeam < maxDistanceBeam) 
        {
            currentScaleYbeam++;
           
        }
        /*
        if (currentScaleYbeam <= maxDistanceBeam) 
        {
            currentScaleYbeam++;
            beamScale.y = currentScaleYbeam;
            transform.localScale = beamScale;
        }
        if (currentScaleYbeam > maxDistanceBeam) 
        {
            currentScaleYbeam = maxDistanceBeam;
            beamScale.y = currentScaleYbeam;
            transform.localScale = beamScale;
        }
         */
    }



}
