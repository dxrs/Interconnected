using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpike : MonoBehaviour
{
    PolygonCollider2D pc;

    private void Start()
    {
        pc = gameObject.GetComponent<PolygonCollider2D>();
    }

    private void Update()
    {
        if (SceneSystem.sceneSystem.isExitScene || SceneSystem.sceneSystem.isRestartScene)
        {
            pc.enabled = false;
        }
    }
}
