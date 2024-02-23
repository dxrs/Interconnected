using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTutorial : MonoBehaviour
{
    [SerializeField] float moveSpeed;

    int randomTargetValue;

    GameObject player1, player2;

    private void Start()
    {
        randomTargetValue = Random.Range(0, 2);
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    private void Update()
    {
        if (Tutorial.tutorial.cameraMoveValue == 3) 
        {
            StartCoroutine(waitToMoveToward());
        }
       
    }

    IEnumerator waitToMoveToward() 
    {
        yield return new WaitForSeconds(1f);
        if (randomTargetValue == 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, player1.transform.position, moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, player2.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player 1") || collision.gameObject.CompareTag("Player 2")) 
        {
            Tutorial.tutorial.isPlayerCanShareLives = true;
            Destroy(gameObject);
        }
    }
}
