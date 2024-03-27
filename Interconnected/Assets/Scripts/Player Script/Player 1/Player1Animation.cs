using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Animation : MonoBehaviour
{
    public static Player1Animation player1Animation;

    public bool isFacingRight;

    [SerializeField] Player1Movement player1Movement;

    [SerializeField] GameObject playerOutlineCollider;

    private void Awake()
    {
        if (player1Animation == null) { player1Animation = this; }
    }

    private void Update()
    {
        playerOutlineCollider.transform.position = transform.position;
        playerOutlineCollider.transform.rotation = transform.rotation;

        if (player1Movement.inputDir.x > 0 && isFacingRight) 
        {
            playerFlip();
        }
        if (player1Movement.inputDir.x < 0 && !isFacingRight) 
        {
            playerFlip();
        }
    }

    private void playerFlip() 
    {
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);
    }
}
