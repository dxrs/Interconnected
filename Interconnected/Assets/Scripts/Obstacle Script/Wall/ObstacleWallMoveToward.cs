using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleWallMoveToward : MonoBehaviour
{


    [SerializeField] int moveTowardID;
    [SerializeField] bool isMovingX;
    [SerializeField] float moveTowardSpeed;
    [SerializeField] float moveXTarget;
    [SerializeField] float moveYTarget;
   


    GameObject player1, player2;

    
 
    private void Start()
    {
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
       

       
    }
    private void Update()
    {

        wallMoveTowardTutorial();

      

        
    }

    private void wallMoveTowardTutorial() 
    {
        if (LevelStatus.levelStatus.levelID == 4)
        {
            if (Tutorial.tutorial.tutorialProgress >= 2 )
            {
                if (moveTowardID == 1)
                {
                   
                    if (player1.transform.position.x >= 6 || player2.transform.position.x >= 6)
                    {

                        if (isMovingX)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveXTarget, transform.position.y), moveTowardSpeed * Time.deltaTime);
                            

                        }
                    }
                }
                if (moveTowardID == 3)
                {
                    if (Tutorial.tutorial.shareLivesProgress == 1)
                    {
                        if (isMovingX)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveXTarget, transform.position.y), moveTowardSpeed * Time.deltaTime);
                        }
                    }
                }


            }
            if (Tutorial.tutorial.tutorialProgress >= 3)
            {
                if (moveTowardID == 2)
                {
                    if (Tutorial.tutorial.isPlayersEnterGarbageArea[0] || Tutorial.tutorial.isPlayersEnterGarbageArea[1])
                    {
                        if (isMovingX)
                        {
                            transform.position = Vector2.MoveTowards(transform.position, new Vector2(moveXTarget, transform.position.y), moveTowardSpeed * Time.deltaTime);
                        }
                    }
                }


            }
        }
    }

   

    

   
}
