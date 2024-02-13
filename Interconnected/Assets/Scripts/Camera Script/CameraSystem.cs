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

    private void Start()
    {
        if (LevelStatus.levelStatus.levelID == 4) 
        {
            transform.position = new Vector2(-3, transform.position.y);
        }
    }
    private void LateUpdate()
    {
        if (cameraTargetObject == null) { return; }

        if (LevelStatus.levelStatus.levelID == 1) 
        {
            if (!GameOver.gameOver.isGameOver
            && !GlobalVariable.globalVariable.isTriggeredWithObstacle
            && ReadyToStart.readyToStart.isGameStart)
            {
                if (cameraTargetObject[0] != null && cameraTargetObject[1] != null)
                {
                    if (Vector2.Distance(cameraTargetObject[0].transform.position, cameraTargetObject[1].transform.position) < distanceBetween2Player)
                    {
                        cameraZoom();
                        cameraMovement();
                        UpdateCameraCollider();
                    }
                }

            }
            if (GameOver.gameOver.isGameOver) 
            {
                if (Player1Health.player1Health.curPlayer1Health <= 0) 
                {
                    // zoom in dan fokus ke cameraTargetObject[0]
                }
                if (Player2Health.player2Health.curPlayer2Health <= 0) 
                {
                    // zoom in dan fokus ke cameraTargetObject[1]
                }
            }
        }
        if (LevelStatus.levelStatus.levelID == 4) 
        {
            if (Tutorial.tutorial.cameraMoveValue == 2) 
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(30,transform.position.y ), 1 * Time.deltaTime);
            }
            if (Tutorial.tutorial.cameraMoveValue == 3)
            {
                transform.position = Vector2.Lerp(transform.position, new Vector2(63, transform.position.y), 1 * Time.deltaTime);
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
