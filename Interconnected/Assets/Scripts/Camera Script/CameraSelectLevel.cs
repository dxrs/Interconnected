using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraSelectLevel : MonoBehaviour
{
    [SerializeField] SelectLevel selectLevel;

    [SerializeField] float[] camPosX;
    [SerializeField] float camMoveSpeed;

    private void Update()
    {
        compareCamPosX();
    }

    private void compareCamPosX() 
    {
        for (int i = 0; i < camPosX.Length; i++)
        {
            if (selectLevel.curLevelSectionValue == i)
            {
                float targetX = camPosX[i];
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(targetX, transform.position.y, transform.position.z), camMoveSpeed * Time.deltaTime);

                if (Mathf.Approximately(transform.position.x, targetX))
                {
                    selectLevel.isCameraNotMoving = true;
                }
                else
                {
                    selectLevel.isCameraNotMoving = false;
                }
                break;
            }
        }
    }
}
