using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Bullet : MonoBehaviour
{

    [SerializeField] float bulletSpeed;

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
                        && Player1Health.player1Health.curPlayer1Health > 0
                        && Player2Health.player2Health.curPlayer2Health > 0
                        && !GarbageCenterPoint.garbageCenterPoint.isGarbageIsReadyToDestroy)
        {
            float distanceToPlayer2 = Vector2.Distance(player1.transform.position, player2.transform.position);

            // Ubah warna berdasarkan jarak
            if (distanceToPlayer2 >= LinkRay.linkRay.maxLinkDistance - 2)
            {
                float t = Mathf.InverseLerp(LinkRay.linkRay.maxLinkDistance - 2, LinkRay.linkRay.maxLinkDistance, distanceToPlayer2);
                Color newColor = sr.color; // Salin warna saat ini
                newColor.a = Mathf.Lerp(1f, 0f, t); // Ubah komponen alpha (transparansi)
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
            if (Player2Animation.player2Animation.isFacingRight && Player1Animation.player1Animation.isFacingRight
                || !Player2Animation.player2Animation.isFacingRight && Player1Animation.player1Animation.isFacingRight
                || Player2Animation.player2Animation.isFacingRight && !Player1Animation.player1Animation.isFacingRight)
            {
                sr.enabled = true;
            }
            else { sr.enabled = false; }
            
        }
        else 
        {
            sr.enabled = false;
        }
       
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Obstacle") 
        {
            Destroy(gameObject);
        }
        if (collision.gameObject.CompareTag("Player 2"))
        {
            Destroy(gameObject);
        }
        
    }
}


