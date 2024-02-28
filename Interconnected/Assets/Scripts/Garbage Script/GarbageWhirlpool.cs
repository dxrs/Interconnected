using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarbageWhirlpool : MonoBehaviour
{
    private void Update()
    {
        transform.Rotate(Vector3.forward, 50 * Time.deltaTime);
    }
}
