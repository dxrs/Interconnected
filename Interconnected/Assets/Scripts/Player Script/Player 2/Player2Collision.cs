using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player2Collision : MonoBehaviour
{
    public static Player2Collision player2Collision;

    public bool isCrashToOtherBoat;

    [SerializeField] Player2Movement player2Movement;

    [SerializeField] ParticleSystem playerHitParticle;

    [SerializeField] float crashForceValue;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player2Collision == null) { player2Collision = this; }
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player 1 Crash Trigger")
        {
            player2Movement.isBraking = false;
            player2Movement.isBrakingWithInput = false;
            StartCoroutine(playerCrash());
            Vector2 backwardMovePos = (transform.position - collision.transform.position).normalized;

            rb.AddForce(backwardMovePos * crashForceValue, ForceMode2D.Impulse);
        }
    }
    IEnumerator playerCrash()
    {
        isCrashToOtherBoat = true;
        yield return new WaitForSeconds(.1f);
        if (!player2Movement.isBraking)
        {
            player2Movement.isBraking = true;
        }
        isCrashToOtherBoat = false;
    }
}
