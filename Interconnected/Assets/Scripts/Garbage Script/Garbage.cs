using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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

    PolygonCollider2D pc;

    GameObject garbageColldector;
    GameObject garbageWhirlpool;

    Vector2 garbagePosition;

    Vector2 maxGarbageScale = new Vector2(0.6f, 0.6f);
    Vector2 minGarbageScale = new Vector2(0.3f, 0.3f);

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        pc = GetComponent<PolygonCollider2D>();
        garbageColldector = GameObject.FindGameObjectWithTag("Garbage Collector");
        garbageWhirlpool = GameObject.FindGameObjectWithTag("Garbage Whirlpool");
        rb.drag = 5;
        randomValue();
        if (isRotate) 
        {
            transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
        }

        transform.localScale = maxGarbageScale;
    }

    private void Update()
    {
        garbageDestroyingFunction();
        if(!GameFinish.gameFinish.isGameFinish || !GameOver.gameOver.isGameOver) 
        {
            if (isGarbageCollected && !isGarbageDestroying)
            {
                if (!GarbageCollector.garbageCollector.isGarbageStored) 
                {
                    if (LinkRay.linkRay.isPlayerLinkedEachOther && !GlobalVariable.globalVariable.isPlayerDestroyed)
                    {
                        garbagePosition = garbageColldector.transform.position;

                        pc.enabled = false;

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
                    garbageBlast();
                }



            }
            if (!isGarbageCollected)
            {
                transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(.6f,.6f), 1f * Time.deltaTime);
                pc.enabled = true;
                StartCoroutine(garbageRigidBrake(1f));
            }
        }
        if (GameOver.gameOver.isGameOver) 
        {
            if (Player1Health.player1Health.curPlayer1Health <= 0 || Player2Health.player2Health.curPlayer2Health <= 0)
            {
                garbageBlast();
            }
        }
       

    }

    private void garbageBlast() 
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

    private void garbageDestroyingFunction()
    {
        if (isGarbageCollected && isGarbageDestroying) 
        {
            garbagePosition = garbageWhirlpool.transform.position;
            posX = garbagePosition.x + randomRadius * Mathf.Cos(Mathf.Deg2Rad * angle);
            posY = garbagePosition.y + randomRadius * Mathf.Sin(Mathf.Deg2Rad * angle);
            transform.position = Vector2.Lerp(transform.position, new Vector2(posX, posY), 2 * Time.deltaTime);
            StartCoroutine(garbageDestroying());
          
        }
    }

    void randomValue()
    {
        angle = Random.Range(0f, 360f);
        lerpSpeed = Random.Range(4.5f, 6.5f);
        flushLerpSpeed = Random.Range(3.2f, 5f);
        randomRadius = Random.Range(0.3f, 2f);
        randomDestroyTime = Random.Range(.2f, 1f);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Garbage Bounce Collider"))
        {
            Vector2 blastForceVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(blastForceVector * 10, ForceMode2D.Impulse);
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
            Vector2 blastForceVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(blastForceVector * 10, ForceMode2D.Impulse);
        }

        if (collision.gameObject.CompareTag("Gear") || collision.gameObject.CompareTag("Spike") || collision.gameObject.CompareTag("Garbage Bounce Collider"))
        {
            Vector2 blastForceVector = (transform.position - collision.transform.position).normalized;
            rb.AddForce(blastForceVector * 10, ForceMode2D.Impulse);
        }

    }
    
    IEnumerator garbageRigidBrake(float delay)
    {
        yield return new WaitForSeconds(delay);
        rb.drag = Mathf.Lerp(rb.drag,5,.1f*Time.deltaTime);
       
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
