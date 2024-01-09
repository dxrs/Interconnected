using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleGear : MonoBehaviour
{
    [SerializeField] float rotationSpeed;

    PolygonCollider2D pc;

    private void Start()
    {
        pc = GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        if (SceneSystem.sceneSystem.isExitScene || SceneSystem.sceneSystem.isRestartScene) 
        {
            pc.enabled = false;
        }
    }
}
