using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexagonTrigger : MonoBehaviour
{
    [SerializeField] HexagonValueSystem hexagonValueSystem;

    [SerializeField] int id;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Player 1") 
        {
            if (id == 1) 
            {
                Destroy(gameObject);
                hexagonValueSystem.hexaValueP1++;
            }
        }
        if (collision.gameObject.tag == "Player 2")
        {
            if (id == 2)
            {
                Destroy(gameObject);
                hexagonValueSystem.hexaValueP2++;
            }
        }
    }
}
