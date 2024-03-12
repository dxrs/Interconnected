using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class CameraSelectLevel : MonoBehaviour
{
    [SerializeField] SelectLevel selectLevel;

    [SerializeField] float[] camPosX;
    [SerializeField] float camMoveSpeed;

    [SerializeField] bool isReadyToMove;

    private void Update()
    {
        if (isReadyToMove) 
        {
            compareCamPosX();
        }
        else 
        {
            for (int j = 0; j < camPosX.Length; j++)
            {
                if (selectLevel.curLevelSectionValue == j)
                {
                    transform.position = new Vector3(camPosX[j], transform.position.y, transform.position.z);
                }
            }

            StartCoroutine(waitToMove());
        }
        
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

    IEnumerator waitToMove() 
    {
        if (!isReadyToMove) 
        {
            yield return new WaitForSeconds(.1f);
            isReadyToMove = true;
        }
    }
}
