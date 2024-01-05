using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Circle : MonoBehaviour
{
    [SerializeField] GlobalVariable globalVariable;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag=="Player 1 Shield")
        {
            globalVariable.circleIsTriggeredWithPlayers[0] = true;
        }
        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag=="Player 2 Shield")
        {
            globalVariable.circleIsTriggeredWithPlayers[1] = true;
        }
        if (collision.gameObject.tag=="Circle Door Trigger") 
        {
            MovingCircle.movingCircle.circleTriggertWithDoor = true; 
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag=="Player 1 Shield")
        {
            globalVariable.circleIsTriggeredWithPlayers[0] = false;
        }

        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag=="Player 2 Shield")
        {
            globalVariable.circleIsTriggeredWithPlayers[1] = false;
        }
        if (collision.gameObject.tag == "Circle Door Trigger")
        {
            MovingCircle.movingCircle.circleTriggertWithDoor = false;
            
        }
    }
}
