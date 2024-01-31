using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBin : MonoBehaviour
{
    public static TrashBin trashBin;

    public bool isTrashBinMoving;
    public bool isTrashBinStopedByDoor;

    [SerializeField] float curTrashBinScale;

    [SerializeField] GlobalVariable globalVariable;

    [SerializeField] float curTrashBinMovementSpeed;

    [SerializeField] Transform[] pathWaypoint;
    [SerializeField] Transform trashBinPathTransform;

    [SerializeField] GameObject trashBinObject;
    [SerializeField] GameObject trashBinFinishObject;

    [SerializeField] SpriteRenderer trashBinSR;

    [SerializeField] Rigidbody2D trashBinRB;

    float maxTrashBinMovementSpeed = 0.5f;
    float maxTrashBinScale = 10;                                                                         

    int startWaypoint = 0;

    Vector2 velocity;

    private void Awake()
    {
        trashBin = this;
    }

    private void Start()
    {
        trashBinObject.transform.position = pathWaypoint[startWaypoint].position;
        curTrashBinScale = 0.5f;
    }

    private void Update()
    {
        trashBinMovementPath();
        if (curTrashBinScale > maxTrashBinScale)
        {
            curTrashBinScale = maxTrashBinScale;
        }
    }

    private void trashBinMovementPath() 
    {
        
        if(isTrashBinMoving && !isTrashBinStopedByDoor) 
        {
            Vector2 targetPosition = pathWaypoint[startWaypoint].transform.position;
            Vector2 moveDir = (targetPosition - (Vector2)trashBinObject.transform.position);

            float distance = Vector2.Distance(trashBinObject.transform.position, targetPosition);

            if (distance > 0.1f)
            {
                Vector2 targetVelocity = moveDir.normalized * curTrashBinMovementSpeed;
                trashBinRB.velocity = Vector2.SmoothDamp(trashBinRB.velocity, targetVelocity, ref velocity, 0.1f);
            }
            else
            {
                trashBinRB.velocity = Vector2.zero;
            }
            trashBinSR.color = Color.Lerp(trashBinSR.color, new Color(1, 1, 1), 5 * Time.deltaTime);
        }
        else 
        {
            trashBinSR.color = Color.Lerp(trashBinSR.color, new Color(.5f, .5f, .5f), 1 * Time.deltaTime);

            trashBinRB.drag = 5;
        }

        if (Vector2.Distance(trashBinObject.transform.position, pathWaypoint[startWaypoint].transform.position) < 0.1f) 
        {
            startWaypoint = (startWaypoint + 1) % pathWaypoint.Length;
        }
        if(startWaypoint == pathWaypoint.Length && !globalVariable.isGameFinish) 
        {
            startWaypoint = pathWaypoint.Length-1;
  
        }
        if (globalVariable.isGameFinish) 
        {
            //setpos ke dekat kapal
        }
    }

    public void trashBinColliderWithRubbish() 
    {
        if (curTrashBinScale <= maxTrashBinScale) 
        {
            curTrashBinScale += 0.05f;
            curTrashBinMovementSpeed -= 0.01f;
            trashBinObject.transform.localScale = new Vector3(curTrashBinScale, curTrashBinScale, curTrashBinScale);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Vector2 startPos = trashBinPathTransform.GetChild(0).position;
        Vector2 prevPos = startPos;
        foreach(Transform waypoint in trashBinPathTransform) 
        {
            Gizmos.DrawSphere(waypoint.position, .3f);
            Gizmos.DrawLine(prevPos, waypoint.position);
            prevPos = waypoint.position;
        }
    }
}
