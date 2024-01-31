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
            globalVariable.isTrashBinOverlapWithPlayers[0] = true;
        }
        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag=="Player 2 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[1] = true;
        }
        if (collision.gameObject.tag=="Circle Door Trigger") 
        {
            MovingCircle.movingCircle.circleTriggertWithDoor = true; 
        }
        if(collision.gameObject.tag=="Circle Target") 
        {
            GlobalVariable.globalVariable.isGameFinish = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag=="Player 1 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[0] = false;
        }

        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag=="Player 2 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[1] = false;
        }
        if (collision.gameObject.tag == "Circle Door Trigger")
        {
            MovingCircle.movingCircle.circleTriggertWithDoor = false;
            
        }
    }
}
