using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleFungee : MonoBehaviour
{
    [SerializeField] GameObject funggeBody;
    [SerializeField] Vector2 maxBodyScale;

    [SerializeField] float maxDistance;

    float scaleSpeed;

    Vector2 minBodyScale = Vector2.one;

    GameObject player1, player2;

    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        scaleSpeed = Random.Range(4.5f,6f);
    }

    private void Update()
    {
        if (player1 && player2 != null)
        {
            float distanceToPlayer1 = Vector2.Distance(transform.position, player1.transform.position);
            float distanceToPlayer2 = Vector2.Distance(transform.position, player2.transform.position);

            if (distanceToPlayer1 < maxDistance || distanceToPlayer2 < maxDistance)
            {

                float minDistance = Mathf.Min(distanceToPlayer1, distanceToPlayer2);

                // skala target berdasarkan jarak
                float targetScaleFactor = Mathf.InverseLerp(maxDistance, 0, minDistance);
                Vector2 targetScale = Vector2.Lerp(minBodyScale, maxBodyScale, targetScaleFactor);

                funggeBody.transform.localScale = Vector2.Lerp(funggeBody.transform.localScale, targetScale, scaleSpeed * Time.deltaTime);

            }
            else
            {
                funggeBody.transform.localScale = Vector2.Lerp(funggeBody.transform.localScale, minBodyScale, scaleSpeed * Time.deltaTime);
                funggeBody.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
            }
        }
        else
        {
            funggeBody.transform.localScale = Vector2.Lerp(funggeBody.transform.localScale, minBodyScale, scaleSpeed * Time.deltaTime);
            funggeBody.transform.Rotate(Vector3.forward, 100 * Time.deltaTime);
        }

    }
}
