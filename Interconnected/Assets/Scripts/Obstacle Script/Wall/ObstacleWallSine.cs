using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallSine : MonoBehaviour
{
    [Tooltip("1 = x\n" +
        "2 = y\n" +
        "3 = -x+y\n" +
        "4 = +x+y\n" +
        "5 = +x-y\n" +
        "6 = -x-y")]
    [Range(1,5)][SerializeField] int sineID;


    [SerializeField] float sineSpeed;
    [SerializeField] float sinePower;
    [SerializeField] bool isSineRandomOffset;

    float randomOffsetSinePos;
    float xSinePos;
    float ySinePos;
    Vector2 pos;

    private void Start()
    {
        randomOffsetSinePos = Random.Range(0, 2);

        pos = transform.position;
        xSinePos = transform.position.x;
        ySinePos = transform.position.y;
    }

    private void Update()
    {
        if (sineID == 1)
        {
            if (isSineRandomOffset)
            {
                transform.position = new Vector2(pos.x + Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower, transform.position.y);
            }
            else
            {
                transform.position = new Vector2(pos.x + Mathf.Sin(sineSpeed * Time.time) * sinePower, transform.position.y);

            }

        }
        if (sineID == 2)
        {
            if (isSineRandomOffset)
            {
                transform.position = new Vector2(transform.position.x, pos.y + (Mathf.Sin(sineSpeed * Time.time + randomOffsetSinePos) * sinePower));
            }
            else
            {
                transform.position = new Vector2(transform.position.x, pos.y + (Mathf.Sin(sineSpeed * Time.time) * sinePower));
            }

        }
        if (sineID == 3)
        {
            transform.position = pos + new Vector2(Mathf.Sin(-sineSpeed * Time.time) * sinePower,
                 Mathf.Sin(sineSpeed * Time.time) * sinePower);
        }
        if (sineID == 4)
        {
            transform.position = pos + new Vector2(Mathf.Sin(sineSpeed * Time.time) * sinePower,
                 Mathf.Sin(sineSpeed * Time.time) * sinePower);
        }
        if (sineID == 5)
        {
            transform.position = pos + new Vector2(Mathf.Sin(sineSpeed * Time.time) * sinePower,
                 Mathf.Sin(-sineSpeed * Time.time) * sinePower);
        }
        if (sineID == 6)
        {
            transform.position = pos + new Vector2(Mathf.Sin(-sineSpeed * Time.time) * sinePower,
                 Mathf.Sin(-sineSpeed * Time.time) * sinePower);
        }
    }
}
