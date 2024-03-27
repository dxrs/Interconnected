using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorButton : MonoBehaviour
{

    [SerializeField] bool isPlayer1SetPosToDoor;
    [SerializeField] bool isPlayer2SetPosToDoor;
    [SerializeField] bool isButtonPressed;

    [SerializeField] Transform buttonSpriteScale;

    private void Update()
    {
      
        if (!GlobalVariable.globalVariable.isPlayerDestroyed) 
        {
            if (buttonSpriteScale != null)
            {
                if (isButtonPressed)
                {
                    buttonSpriteScale.localScale = Vector2.MoveTowards(buttonSpriteScale.localScale, new Vector2(0.02f, 0.02f), 4.5f * Time.deltaTime);
                }
                else
                {
                    buttonSpriteScale.localScale = Vector2.Lerp(buttonSpriteScale.localScale, new Vector2(1.5f, 1.5f), 4.5f * Time.deltaTime);
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
