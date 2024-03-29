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

    [Header("Tutorial Only")]
    [SerializeField] float[] camPosX;

    [SerializeField] GameObject camBoundaries;

    Vector3 cameraVelocity;

    Vector2 camBoundariesScale;

    private void Start()
    {
        if (LevelStatus.levelStatus.levelID == 4) 
        {
            transform.position = new Vector3(camPosX[0], transform.position.y,-10);
        }
        camBoundariesScale = camBoundaries.transform.localScale;
        camBoundaries.SetActive(false);
    }

    private void Update()
    {
        if (LevelStatus.levelStatus.levelID == 1) 
        {
            if (GlobalVariable.globalVariable.isCameraBoundariesActive)
            {
                StartCoroutine(waitCamBoundActive());
            }
            else
            {
                camBoundaries.SetActive(false);
            }
        }
        else { camBoundaries.SetActive(false); }
       
    }
    private void LateUpdate()
    {
        if (LevelStatus.levelStatus.levelID == 1) 
        {
            float newScaleX = cam.orthographicSize * (cam.aspect / 2f);
            float newScaleY = cam.orthographicSize / 2f;

            transform.localScale = new Vector3(newScaleX / camBoundariesScale.x, newScaleY / camBoundariesScale.y, 1f);
        }
       
        if (cameraTargetObject == null) { return; }
        if (LevelStatus.levelStatus.levelID == 1)
        {
            if (!GameOver.gameOver.isGameOver
            && !GlobalVariable.globalVariable.isPlayerDestroyed)
            {
                if (cameraTargetObject[0] != null && cameraTargetObject[1] != null)
                {
                    if (Vector2.Distance(cameraTargetObject[0].transform.position, cameraTargetObject[1].transform.position) < distanceBetween2Player)
                    {
                        cameraZoom();
                        cameraMovement();
                    }
                }

            }
            if (GameOver.gameOver.isGameOver)
            {
                if (Timer.timerInstance.isTimerLevel)
                {
                    if (Timer.timerInstance.curTimerValue > 0)
                    {
                        for (int k = 0; k < cameraTargetObject.Count; k++)
                        {
                            if (cameraTargetObject[k] != null)
                            {
                                if (Player1Health.player1Health.curPlayer1Health <= 0)
                                {
                                    focusOnNoobPlayer(cameraTargetObject[0]);
                                }
                                if (Player2Health.player2Health.curPlayer2Health <= 0)
                                {
                                    focusOnNoobPlayer(cameraTargetObject[1]);
                                }
                            }
                        }
                    }
                }
                else
                {
                    for (int k = 0; k < cameraTargetObject.Count; k++)
                    {
                        if (cameraTargetObject[k] != null)
                        {
                            if (Player1Health.player1Health.curPlayer1Health <= 0)
                            {
                                focusOnNoobPlayer(cameraTargetObject[0]);
                            }
                            if (Player2Health.player2Health.curPlayer2Health <= 0)
                            {
                                focusOnNoobPlayer(cameraTargetObject[1]);
                            }
                        }
                    }
                }


            }
        }
        if (LevelStatus.levelStatus.levelID == 4) 
        {

            if (Tutorial.tutorial.tutorialProgress == 2) 
            {
                if (cameraTargetObject[0].position.x > 6 || cameraTargetObject[1].position.x > 6) 
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(camPosX[1], transform.position.y,-10), 2 * Time.deltaTime);
                }
                
            }
            if (Tutorial.tutorial.tutorialProgress == 3)
            {
                if (Tutorial.tutorial.isPlayersEnterGarbageArea[0] || Tutorial.tutorial.isPlayersEnterGarbageArea[1]) 
                {
                    transform.position = Vector3.Lerp(transform.position, new Vector3(camPosX[2], transform.position.y, -10), 2 * Time.deltaTime);
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

    private void focusOnNoobPlayer(Transform playerTransform)
    {
        Vector3 newCameraPosition = playerTransform.position + cameraOffset;
        float newZoom = Mathf.Lerp(cameraMaxZoom, 1f, camBoundDistance() / cameraZoomLimiter);

        transform.position = Vector3.SmoothDamp(transform.position, newCameraPosition, ref cameraVelocity, smoothCameraTimeMovemement);
        cam.orthographicSize = Mathf.Lerp(cam.orthographicSize, newZoom, Time.unscaledDeltaTime);
    }

    IEnumerator waitCamBoundActive() 
    {
        yield return new WaitForSeconds(3);
        camBoundaries.SetActive(true);
    }
}
