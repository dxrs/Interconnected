using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public int id;
    public int ValueButton;
    public int coba;

    [SerializeField] string doorPlayer;

    [SerializeField] bool isPlayer1SetPosToDoor;
    [SerializeField] bool isPlayer2SetPosToDoor;
    [SerializeField] bool doorButtonIsDestroyed;


    private void Update()
    {
        coba = ValueButton/2;
        if(id==coba)
        {
            print("asd");
            Destroy(this.gameObject);
        }
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
            if (Player1.player1 != null && Player2.player2!=null) 
            {
                Player1.player1.player1SetPos();
                Player2.player2.player2SetPos();
            }

        }


        

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player 1"||collision.gameObject.tag == "Player 2")
        {
                ValueButton ++;

        }
        if (doorPlayer == "Player 1")
        {
            if (collision.gameObject.tag == "Player 1")
            {
                isPlayer1SetPosToDoor = true;
                // ValueButton ++;
                // doorButtonIsDestroyed = true;
            }
        }
        if (doorPlayer == "Player 2")
        {
            if (collision.gameObject.tag == "Player 2")
            {
                isPlayer2SetPosToDoor = true;
                // ValueButton ++;
                // doorButtonIsDestroyed = true;
            }
        }
       
    }
}
