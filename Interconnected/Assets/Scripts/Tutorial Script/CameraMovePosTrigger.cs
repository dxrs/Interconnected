using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovePosTrigger : MonoBehaviour
{
    bool player1Collision = false;
    bool player2Collision = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.tag == "Player 1")
        {
            player1Collision = true;
        }

        if (collision.gameObject.tag == "Player 2")
        {
            player2Collision = true;
        }

        if (player1Collision && player2Collision)
        {
            //Tutorial.tutorial.cameraMoveValue++;
            player1Collision = false;
            player2Collision = false;
            StartCoroutine(destroyed());
        }
    }

    IEnumerator destroyed() 
    {
        yield return new WaitForSeconds(.1f);
        Destroy(gameObject);
    }
}
