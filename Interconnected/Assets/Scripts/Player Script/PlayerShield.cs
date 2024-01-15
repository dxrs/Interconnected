using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        if (GlobalVariable.globalVariable.isTriggeredWithObstacle) 
        {
            sr.enabled = false;
        }
        else 
        {
            sr.enabled = true;
        }
    }
}
