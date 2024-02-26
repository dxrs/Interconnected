using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Tutorial : MonoBehaviour
{
    public static Tutorial tutorial;

    public int tutorialProgress;
    public int shareLivesProgress;

    public bool isEnemyReadyToShoot;
    public bool isReadyToShareLives;

    [SerializeField] GameObject wallBlocker;
    [SerializeField] GameObject wallStatic;

    GameObject player1, player2;

    private void Awake()
    {
        tutorial = this;
    }

    private void Start()
    {
        tutorialProgress = 1;
        player1 = GameObject.FindGameObjectWithTag("Player 1");
        player2 = GameObject.FindGameObjectWithTag("Player 2");
    }

    private void Update()
    {
        if (tutorialProgress >= 2) 
        {
            if(player1.transform.position.x >= 7.5f && player2.transform.position.x >= 7.5f) 
            {
                isEnemyReadyToShoot = true;
            }
            if (shareLivesProgress >= 2) 
            {
                Destroy(wallStatic);
            }
        }
        if (tutorialProgress >= 3) 
        {
            wallBlocker.SetActive(true);
        }
    }
    
}
