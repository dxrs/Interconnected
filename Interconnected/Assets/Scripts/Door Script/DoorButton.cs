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

    public int test;

    [SerializeField] Rigidbody2D playerRb;

    private void Update()
    {
        if (!GlobalVariable.globalVariable.isPlayerDestroyed) 
        {
            for (int i = 1; i <= GlobalVariable.globalVariable.maxDoor; i++)
            {
                if (id == i)
                {
                    if (GlobalVariable.globalVariable.curDoorOpenValue == 2) 
                    {
                        Destroy(gameObject, 0.5f);
                    }
                    if (doorPlayer == "Player 1")
                    {
                        if (doorButtonIsDestroyed)
                        {
                            if (GlobalVariable.globalVariable.curDoorOpenValue == 2) 
                            {
                                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), 4.5f * Time.deltaTime);
                                if (transform.localScale.x < 0.01f)
                                {
                                    //Destroy(gameObject);
                                }
                            }
                           
                        }
                        if (isPlayer1SetPosToDoor)
                        {
                            GameObject player1;
                           // player1 = GameObject.FindGameObjectWithTag("Player 1");
                            //player1.transform.position = transform.position;
                            //Player1Movement.player1Movement.isMoving = false;


                        }
                    }

                    if (doorPlayer == "Player 2")
                    {
                        if (doorButtonIsDestroyed)
                        {
                            if (GlobalVariable.globalVariable.curDoorOpenValue == 2) 
                            {
                                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(0, 0), 4.5f * Time.deltaTime);
                                if (transform.localScale.x < 0.01f)
                                {
                                    //Destroy(gameObject);
                                }
                            }
                            
                        }
                        if (isPlayer2SetPosToDoor)
                        {

                            GameObject player2;
                            //player2 = GameObject.FindGameObjectWithTag("Player 2");
                            //player2.transform.position = transform.position;
                            //Player2Movement.player2Movement.isMoving = false;
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
           
          

        }

    }

    private void OnDestroy()
    {
        //Checkpoint.checkpoint.curCheckpointValue++;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player 1"||collision.gameObject.tag=="Player 2") 
        {
            GlobalVariable.globalVariable.curDoorOpenValue++;
        }
        if (doorPlayer == "Player 1")
        {
            if (collision.gameObject.tag == "Player 1")
            {
               
               // isPlayer1SetPosToDoor = true;
                //doorButtonIsDestroyed = true;
                //playerRb.drag = 0;
            }
        }
        if (doorPlayer == "Player 2")
        {
            if (collision.gameObject.tag == "Player 2")
            {
                //GlobalVariable.globalVariable.curDoorOpenValue++;
                //isPlayer2SetPosToDoor = true;
                //doorButtonIsDestroyed = true;
                //playerRb.drag = 0;
            }
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 2")
        {
            GlobalVariable.globalVariable.curDoorOpenValue--;
        }
    }
}
