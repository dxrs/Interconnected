using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{
    public int id;

    [SerializeField] bool isPlayer1SetPosToDoor;
    [SerializeField] bool isPlayer2SetPosToDoor;
    [SerializeField] bool isButtonPressed;


    private void Update()
    {
      
        if (!GlobalVariable.globalVariable.isPlayerDestroyed) 
        {
            for (int i = 1; i <= GlobalVariable.globalVariable.maxDoor; i++)
            {
                if (id == i)
                {
                    if (isButtonPressed)
                    {
                        transform.localScale = Vector2.MoveTowards(transform.localScale, new Vector2(0.02f, 0.02f), 4.5f * Time.deltaTime);
                    }
                    else
                    {
                        transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(1.5f, 1.5f), 4.5f * Time.deltaTime);
                    }
                  
                    break;
                }
            }
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
            isButtonPressed = true;
           
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1" || collision.gameObject.tag == "Player 2")
        {
            isButtonPressed = false;
            
        }
    }
}
