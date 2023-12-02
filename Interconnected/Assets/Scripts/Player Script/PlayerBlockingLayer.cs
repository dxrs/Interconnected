using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBlockingLayer : MonoBehaviour
{
    [SerializeField] LinkRay linkRay;

    private void Update()
    {
        if (linkRay.isChangeLinkMethod) 
        {
            //gameObject.layer = LayerMask.NameToLayer("Obstacle");
        }
        else 
        {
            //gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
}
