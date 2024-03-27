using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Animation : MonoBehaviour
{
    public static Player2Animation player2Animation;

    public bool isFacingRight;

    [SerializeField] Player2Movement player2Movement;

    [SerializeField] GameObject playerOutlineCollider;


    private void Awake()
    {
        if(player2Animation == null) { player2Animation = this; }
    }
    private void Update()
    {
        playerOutlineCollider.transform.position = transform.position;
        playerOutlineCollider.transform.rotation = transform.rotation;

        if (player2Movement.inputDir.x > 0 && isFacingRight)
        {
            playerFlip();
        }
        if (player2Movement.inputDir.x < 0 && !isFacingRight)
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
