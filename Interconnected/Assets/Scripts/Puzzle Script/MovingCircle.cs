using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingCircle : MonoBehaviour
{
    public static MovingCircle movingCircle;

    public int id;

    [SerializeField] float movementSpeed;

    [SerializeField] bool isMovingStatic;

    [SerializeField] Transform[] waypoint; //can move using waypoint if have dynamic movement

    [SerializeField] GameObject circle;
    [SerializeField] GameObject circleTarget;

    private void Awake()
    {
        if (movingCircle == null) { movingCircle = this; }
    }

    private void Update()
    {
        for (int i = 0; i < GlobalVariable.globalVariable.circleIsMoving.Length; i++)
        {
            if (GlobalVariable.globalVariable.circleIsMoving[i] && id == i + 1)
            {
                if (isMovingStatic)
                {
                    circle.transform.position = Vector2.MoveTowards(circle.transform.position,
                        circleTarget.transform.position, movementSpeed * Time.deltaTime);
                }
            }
        }
    }
}
