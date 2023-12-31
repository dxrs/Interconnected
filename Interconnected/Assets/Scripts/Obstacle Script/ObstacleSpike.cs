using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpike : MonoBehaviour
{
    [SerializeField] string spikeType;

    [SerializeField] bool isAnimatedSpike;
    [SerializeField] bool isRotatingSpike;

    [SerializeField] float rotationSpeed;

    private void Update()
    {
        if (spikeType == "Gear") 
        {
            if (isAnimatedSpike) 
            {
                if (isRotatingSpike) 
                {
                    // rotate obstacle left or right
                    transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
                }
            }
        }
    }
}
