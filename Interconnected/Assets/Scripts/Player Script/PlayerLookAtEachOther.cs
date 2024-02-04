using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLookAtEachOther : MonoBehaviour
{
    [SerializeField] GameObject playerLookAt;

    [SerializeField] float speed;
    [SerializeField] float rotationModifier;


    private void FixedUpdate() // player look at each other function
    {
        if (playerLookAt != null)
        {
            Vector3 vectorToTarget = playerLookAt.transform.position - transform.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg - rotationModifier;
            Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, q, Time.deltaTime * speed);
        }

    }
}
