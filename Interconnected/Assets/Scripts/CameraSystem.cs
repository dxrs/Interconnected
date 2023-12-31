using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraSystem : MonoBehaviour
{
    [SerializeField] Camera cam;

    [SerializeField] List<Transform> cameraTargetObject;

    [SerializeField] Vector3 cameraOffset;

    [SerializeField] float distanceBetween2Player;
    [SerializeField] float smoothCameraTimeMovemement;
    [SerializeField] float cameraMinZoom;
    [SerializeField] float cameraMaxZoom;
    [SerializeField] float cameraZoomLimiter;

    [SerializeField] BoxCollider2D cameraCollider;

    Vector3 cameraVelocity;

    private void LateUpdate()
    {
        if (cameraTargetObject == null) { return; }

        if (!GlobalVariable.globalVariable.isGameOver) 
        {
            if (cameraTargetObject[0] != null && cameraTargetObject[1]!=null) 
            {
                if (Vector2.Distance(cameraTargetObject[0].transform.position, cameraTargetObject[1].transform.position) < distanceBetween2Player)
                {
                    cameraZoom();
                    cameraMovement();
                    UpdateCameraCollider();
                }
            }
            
        }
        
       
    }

    private void cameraMovement() 
    {
        Vector3 camCenterPoint = getCenterPointOfCam();
        Vector3 newPos = camCenterPoint + cameraOffset;
        transform.position = Vector3.SmoothDamp(transform.position, newPos, ref cameraVelocity, smoothCameraTimeMovemement);
    }

    private void cameraZoom() 
    {
        float newZoom = Mathf.Lerp(cameraMaxZoom, cameraMinZoom, camBoundDistance() / cameraZoomLimiter);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.deltaTime);
        
        
        
    }

    float camBoundDistance() 
    {
        var bounds = new Bounds(cameraTargetObject[0].position, Vector3.zero);
        for (int j = 0; j < cameraTargetObject.Count; j++)
        {
            bounds.Encapsulate(cameraTargetObject[j].position);
        }
        return bounds.size.x + bounds.size.y;
    }

    Vector3 getCenterPointOfCam() 
    {
        if (cameraTargetObject.Count == 1)
        {
            return cameraTargetObject[0].position;
        }
        var bounds = new Bounds(cameraTargetObject[0].position, Vector2.zero);
        for (int j = 0; j < cameraTargetObject.Count; j++)
        {
            bounds.Encapsulate(cameraTargetObject[j].position);
        }
        return bounds.center;
    }

    private void UpdateCameraCollider()
    {
        // Sesuaikan ukuran collider sesuai dengan orthographic size
        cameraCollider.size = new Vector2(cam.orthographicSize * cam.aspect * 2f, cam.orthographicSize * 2f);
    }

    
}
