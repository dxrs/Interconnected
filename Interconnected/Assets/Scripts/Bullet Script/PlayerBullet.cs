using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed;
    [SerializeField] float colorChangeDistanceThreshold;

    GameObject player1, player2;

    SpriteRenderer sr;

    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        player2 = GameObject.FindGameObjectWithTag("Player 2");
        player1 = GameObject.FindGameObjectWithTag("Player 1");
    }

    private void Update()
    {
        if (player2 && player1 != null && !GlobalVariable.globalVariable.isPlayerDestroyed
                        && !SceneSystem.sceneSystem.isExitScene
                        && !SceneSystem.sceneSystem.isRestartScene
                        && !GameOver.gameOver.isGameOver)
        {
            float distanceToPlayer2 = Vector2.Distance(player1.transform.position, player2.transform.position);

            // Ubah warna berdasarkan jarak
            if (distanceToPlayer2 >= 10f)
            {
                float t = Mathf.InverseLerp(10f, LinkRay.linkRay.maxLinkDistance, distanceToPlayer2);
                Color newColor = Color.Lerp(Color.white, Color.red, t);
                sr.color = newColor;
            }

            transform.position = Vector2.MoveTowards(transform.position, player2.transform.position, bulletSpeed * Time.deltaTime);
        }
        else
        {
            Destroy(gameObject);
        }

        if (GlobalVariable.globalVariable.isRopeVisible) 
        {
            sr.enabled = true;
        }
        else 
        {
            sr.enabled = false;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Obstacle"||
            collision.gameObject.tag=="Obstacle P1"||
            collision.gameObject.tag == "Obstacle P2") 
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag == "Player 2 Shield")
        {
            Destroy(gameObject);
        }
        
    }
}


