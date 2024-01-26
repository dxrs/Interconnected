using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
 
    public float resizeAmount;

    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + (resizeAmount / 2));
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y + resizeAmount);
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - (resizeAmount / 2));
            transform.localScale = new Vector2(transform.localScale.x, transform.localScale.y- resizeAmount);

        }
    }
  

    
}
