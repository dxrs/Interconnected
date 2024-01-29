using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public int id;

    [SerializeField] string doorPlayer;

    [SerializeField] bool isPlayer1SetPosToDoor;
    [SerializeField] bool isPlayer2SetPosToDoor;
    [SerializeField] bool doorButtonIsDestroyed;

    [SerializeField] Rigidbody2D rp1;
    [SerializeField] Rigidbody2D rp2;

    private void Update()
    {
        if (!GlobalVariable.globalVariable.isTriggeredWithObstacle) 
        {
            for (int i = 1; i <= GlobalVariable.globalVariable.maxDoor; i++)
            {
                if (id == i)
                {

                    if (doorPlayer == "Player 1")
                    {
                        if (doorButtonIsDestroyed)
                        {
                            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), 2.5f * Time.deltaTime);
                            if (transform.localScale.x < 0.01f)
                            {
                                Destroy(gameObject);
                            }
                        }
                        if (isPlayer1SetPosToDoor)
                        {
                            GameObject gP1;
                            gP1 = GameObject.FindGameObjectWithTag("Player 1");
                            gP1.transform.position = transform.position;


                        }
                    }

                    if (doorPlayer == "Player 2")
                    {
                        if (doorButtonIsDestroyed)
                        {
                            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), 2.5f * Time.deltaTime);
                            if (transform.localScale.x < 0.01f)
                            {
                                Destroy(gameObject);
                            }
                        }
                        if (isPlayer2SetPosToDoor)
                        {

                            GameObject gP2;
                            gP2 = GameObject.FindGameObjectWithTag("Player 2");
                            gP2.transform.position = transform.position;

                        }

                    }

                    break;
                }
            }
        }
        else 
        {
            isPlayer1SetPosToDoor = false;
            isPlayer2SetPosToDoor = false;
           
            if (Player1Collision.player1Collision != null && Player2Collision.player2Collision != null) 
            {
                //Player1.player1.player1SetPos();
                //Player2.player2.player2SetPos();
            }

        }


        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (doorPlayer == "Player 1")
        {
            if (collision.gameObject.tag == "Player 1")
            {
                isPlayer1SetPosToDoor = true;
                doorButtonIsDestroyed = true;
                rp1.drag = 0;
            }
        }
        if (doorPlayer == "Player 2")
        {
            if (collision.gameObject.tag == "Player 2")
            {
                isPlayer2SetPosToDoor = true;
                doorButtonIsDestroyed = true;
                rp2.drag = 0;
            }
        }
       
    }
}
