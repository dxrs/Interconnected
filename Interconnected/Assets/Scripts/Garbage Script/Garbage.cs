using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public bool isGarbageCollected;

    GameObject garbageSpawner;

    [SerializeField] float blastForce;  
    [SerializeField] float blastRadius;

    float lerpSpeed;
    float posX;
    float posY;
    float angle;

    Rigidbody2D rb;

    BoxCollider2D bc;

    Vector2 garbagePosition;

    Vector2 maxGarbageScale = new Vector2(0.6f, 0.6f);
    Vector2 minGarbageScale = new Vector2(0.3f, 0.3f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        garbageSpawner = GameObject.FindGameObjectWithTag("Garbage Spawner");
        rb.drag = 5;
        angle = Random.Range(0f, 360f);
        lerpSpeed = Random.Range(4.5f, 6.5f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        transform.localScale = maxGarbageScale;
    }

    private void Update()
    {
        
        if (isGarbageCollected) 
        {
            if(LinkRay.linkRay.playerLinkedEachOther && !GlobalVariable.globalVariable.isTriggeredWithObstacle) 
            {
                garbagePosition = garbageSpawner.transform.position;

                bc.enabled = false;

                transform.localScale = minGarbageScale;

                posX = garbagePosition.x + GarbageCollector.garbageCollector.radius * Mathf.Cos(Mathf.Deg2Rad * angle);
                posY = garbagePosition.y + GarbageCollector.garbageCollector.radius * Mathf.Sin(Mathf.Deg2Rad * angle);

                transform.position = Vector2.Lerp(transform.position, new Vector2(posX, posY), lerpSpeed * Time.deltaTime);
            }
            if(!LinkRay.linkRay.playerLinkedEachOther || GlobalVariable.globalVariable.isTriggeredWithObstacle || Player2Movement.player2Movement.maxPlayerSpeed<=0) 
            {
                transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                StartCoroutine(garbageRigidBrake(1f));
                Vector2 directionToCenter = (Vector2)transform.position - (Vector2)garbageSpawner.transform.position;
                float distanceToCenter = directionToCenter.magnitude;


                if (distanceToCenter > 0 && distanceToCenter < blastRadius)
                {
                    Vector2 blastForceVector = directionToCenter.normalized * blastForce;
                    rb.AddForce(blastForceVector, ForceMode2D.Impulse);
                }
                isGarbageCollected = false;
            }



        }
        if (!isGarbageCollected) 
        {
            transform.localScale = Vector2.Lerp(transform.localScale, maxGarbageScale, 1f * Time.deltaTime);
            bc.enabled = true;
            StartCoroutine(garbageRigidBrake(1f));
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Gear" || collision.gameObject.tag == "Spike")
        {
            Vector2 blastForceVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(blastForceVector * 10, ForceMode2D.Impulse);
            StartCoroutine(garbageRigidBrake(1f));
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Laser Rope") 
        {
            isGarbageCollected = true;
            rb.drag = 0;
           
        }

        if(collision.gameObject.tag=="Gear" || collision.gameObject.tag == "Spike")
        {
            Vector2 blastForceVector = (transform.position-collision.transform.position).normalized;
            rb.AddForce(blastForceVector*10, ForceMode2D.Impulse);
            StartCoroutine(garbageRigidBrake(1f));
        }

       
    }
    IEnumerator garbageRigidBrake(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        rb.drag = Mathf.Lerp(rb.drag,5,0.1f*Time.deltaTime);
       
    }
}
