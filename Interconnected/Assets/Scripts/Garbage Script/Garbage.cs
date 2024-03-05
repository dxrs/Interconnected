using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    public bool isGarbageCollected;

    [SerializeField] float blastForce;  
    [SerializeField] float blastRadius;

    [SerializeField] bool isGarbageDestroying;
    [SerializeField] bool isRotate;

    float lerpSpeed;
    float posX;
    float posY;
    float angle;
    float flushLerpSpeed;
    float randomRadius;
    float randomDestroyTime;

    Rigidbody2D rb;

    BoxCollider2D bc;

    GameObject garbageColldector;
    GameObject garbageWhirlpool;

    Vector2 garbagePosition;
    Vector2 randomGarbageScale;

    Vector2 maxGarbageScale = new Vector2(0.6f, 0.6f);
    Vector2 minGarbageScale = new Vector2(0.3f, 0.3f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        garbageColldector = GameObject.FindGameObjectWithTag("Garbage Collector");
        garbageWhirlpool = GameObject.FindGameObjectWithTag("Garbage Whirlpool");
        randomGarbageScale = new Vector2(Random.Range(0.45f, maxGarbageScale.x), Random.Range(0.45f, maxGarbageScale.y));
        rb.drag = 5;
        angle = Random.Range(0f, 360f);
        lerpSpeed = Random.Range(4.5f, 6.5f);
        flushLerpSpeed = Random.Range(2.2f, 3f);
        randomRadius = Random.Range(0.3f, 2f);
        randomDestroyTime = Random.Range(1f, 2f);
        if (isRotate) 
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }

        transform.localScale = maxGarbageScale;
    }

    private void Update()
    {
        if (isGarbageCollected && isGarbageDestroying) 
        {
            garbagePosition = garbageWhirlpool.transform.position;
            posX = garbagePosition.x + randomRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            posY = garbagePosition.y + randomRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
            transform.position = Vector2.Lerp(transform.position, new Vector2(posX, posY), 2 * Time.deltaTime);
            StartCoroutine(garbageDestroying());
          
        }
        if(!GameFinish.gameFinish.isGameFinish || !GameOver.gameOver.isGameOver) 
        {
            if (isGarbageCollected && !isGarbageDestroying)
            {
                if (!GarbageCollector.garbageCollector.isGarbageStored) 
                {
                    if (LinkRay.linkRay.isPlayerLinkedEachOther && !GlobalVariable.globalVariable.isPlayerDestroyed)
                    {
                        garbagePosition = garbageColldector.transform.position;

                        bc.enabled = false;

                        transform.localScale = minGarbageScale;

                        posX = garbagePosition.x + GarbageCollector.garbageCollector.radius * Mathf.Cos(Mathf.Deg2Rad * angle);
                        posY = garbagePosition.y + GarbageCollector.garbageCollector.radius * Mathf.Sin(Mathf.Deg2Rad * angle);

                        transform.position = Vector2.Lerp(transform.position, new Vector2(posX, posY), lerpSpeed * Time.deltaTime);
                    }
                }
                else 
                {
                    isGarbageDestroying = true;
                    
                    StartCoroutine(garbageIsStoring());
                }
                
                if (!LinkRay.linkRay.isPlayerLinkedEachOther || GlobalVariable.globalVariable.isPlayerDestroyed)
                {
                    if (isRotate)
                    {
                        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
                    }
                    StartCoroutine(garbageRigidBrake(1f));
                    Vector2 directionToCenter = (Vector2)transform.position - (Vector2)garbageColldector.transform.position;
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
                transform.localScale = Vector2.Lerp(transform.localScale, randomGarbageScale, 1f * Time.deltaTime);
                bc.enabled = true;
                StartCoroutine(garbageRigidBrake(1f));
            }
        }
        if (GameOver.gameOver.isGameOver) 
        {
            StartCoroutine(garbageRigidBrake(1f));
            Vector2 directionToCenter = (Vector2)transform.position - (Vector2)garbageColldector.transform.position;
            float distanceToCenter = directionToCenter.magnitude;


            if (distanceToCenter > 0 && distanceToCenter < blastRadius)
            {
                Vector2 blastForceVector = directionToCenter.normalized * blastForce;
                rb.AddForce(blastForceVector, ForceMode2D.Impulse);
            }
            isGarbageCollected = false;
        }
       

    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Garbage Bounce Collider"))
        {
            Vector2 blastForceVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(blastForceVector * 10, ForceMode2D.Impulse);
            StartCoroutine(garbageRigidBrake(1f));
        }
        if (collision.gameObject.CompareTag("Garbage Center Point"))
        {
            isGarbageDestroying = true;
            isGarbageCollected = true;
            bc.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Laser Rope")) 
        {
            if(Player1Movement.player1Movement.curMaxSpeed > 1 && Player2Movement.player2Movement.curMaxSpeed > 1) 
            {
                isGarbageCollected = true;
                rb.drag = 0;
            }
           
        }

        if(collision.gameObject.CompareTag("Garbage Center Point")) 
        {
            isGarbageDestroying = true;
            isGarbageCollected = true;
            bc.enabled = false;
        }

        if (collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Garbage Bounce Collider"))
        {
            Vector2 blastForceVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(blastForceVector * 10, ForceMode2D.Impulse);
            StartCoroutine(garbageRigidBrake(1f));
        }

    }
    IEnumerator garbageRigidBrake(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        rb.drag = Mathf.Lerp(rb.drag,5,0.1f*Time.deltaTime);
       
    }

    IEnumerator garbageIsStoring() 
    {
        yield return new WaitForSeconds(.1f);
        
        GarbageCollector.garbageCollector.garbageCollected = 0;
    }
    IEnumerator garbageDestroying() 
    {
        yield return new WaitForSeconds(randomDestroyTime);
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, flushLerpSpeed * Time.deltaTime);
        if (transform.localScale.x <= 0.02f)
        {
          Destroy(gameObject);
        }
    }
    private void OnDestroy()
    {
        GarbageCollector.garbageCollector.currentGarbageStored++;
    }
}
