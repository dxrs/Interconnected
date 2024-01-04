using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public int id;

    [SerializeField] string doorPlayer;

    [SerializeField] bool isPlayer1SetPosToDoor;
    [SerializeField] bool isPlayer2SetPosToDoor;


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
                        if (isPlayer1SetPosToDoor)
                        {
                            GameObject gP1;
                            gP1 = GameObject.FindGameObjectWithTag("Player 1");
                            gP1.transform.position = transform.position;
                            Destroy(gameObject, 2);
                        }
                    }

                    if (doorPlayer == "Player 2")
                    {
                        if (isPlayer2SetPosToDoor)
                        {
                            GameObject gP2;
                            gP2 = GameObject.FindGameObjectWithTag("Player 2");
                            gP2.transform.position = transform.position;
                            Destroy(gameObject, 2);
                        }
                    }

                    break;
                }
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
                
            }
        }
        if (doorPlayer == "Player 2")
        {
            if (collision.gameObject.tag == "Player 2")
            {
                isPlayer2SetPosToDoor = true;

            }
        }
       
    }
}
