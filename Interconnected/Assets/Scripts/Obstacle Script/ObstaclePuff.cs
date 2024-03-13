using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclePuff : MonoBehaviour
{
    [SerializeField] GameObject puffBody;
    [SerializeField] Vector2 maxBodyScale;
    [SerializeField] Vector2 minBodyScale;
    [SerializeField] float maxDistance;
    float scaleSpeed; 

    GameObject player1, player2;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        scaleSpeed = 5;
    }

    private void Update()
    {
        if(player1 && player2 != null) 
        {
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.transform.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.transform.position);

            if (distanceToPlayer1 < maxDistance || distanceToPlayer2 < maxDistance)
            {

                float minDistance = Mathf.Min(distanceToPlayer1, distanceToPlayer2);

                // skala target berdasarkan jarak
                float targetScaleFactor = Mathf.InverseLerp(maxDistance, 0, minDistance);
                Vector2 targetScale = Vector2.Lerp(minBodyScale, maxBodyScale, targetScaleFactor);

                puffBody.transform.localScale = Vector2.Lerp(puffBody.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

            }
            else
            {
                puffBody.transform.localScale = Vector2.Lerp(puffBody.transform.localScale, minBodyScale, scaleSpeed * Time.deltaTime);
                puffBody.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
            }
        }
        else 
        {
            puffBody.transform.localScale = Vector2.Lerp(puffBody.transform.localScale, minBodyScale, scaleSpeed * Time.deltaTime);
            puffBody.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
        }
        
    }
}
