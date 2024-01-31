using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrashBinCollider : MonoBehaviour
{
    [SerializeField] GlobalVariable globalVariable;
    [SerializeField] TrashBin trashBin;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 1 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[0] = true;
        }
        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag == "Player 2 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[1] = true;
        }

        if (collision.gameObject.tag == "Trash Bin Stop Collider")
        {
            trashBin.isTrashBinStopedByDoor = true;
        }

        if (collision.gameObject.tag == "Rubbish") 
        {
            trashBin.trashBinColliderWithRubbish();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 1 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[0] = false;
        }

        if (collision.gameObject.tag == "Player 2" || collision.gameObject.tag == "Player 2 Shield")
        {
            globalVariable.isTrashBinOverlapWithPlayers[1] = false;
        }
        if (collision.gameObject.tag == "Trash Bin Stop Collider")
        {
            trashBin.isTrashBinStopedByDoor = false;
        }
    }
}
