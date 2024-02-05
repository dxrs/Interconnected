using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    [SerializeField] bool isGarbageCollected;

    GameObject garbageSpawner;

    [SerializeField] float blastForce = 5f;  
    [SerializeField] float blastRadius = 3f;  

    float lerpSpeed;
    float posX;
    float posY;
    float angle;

    Rigidbody2D rb;

    Vector2 garbagePosition;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        garbageSpawner = GameObject.FindGameObjectWithTag("Garbage Spawner");
        angle = Random.Range(0f, 360f);
        lerpSpeed = Random.Range(3.5f, 5.5f);
        transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
    }

    private void Update()
    {
        if (isGarbageCollected && LinkRay.linkRay.playerLinkedEachOther && !GlobalVariable.globalVariable.isTriggeredWithObstacle) 
        {
            garbagePosition = garbageSpawner.transform.position;

            posX = garbagePosition.x + GarbageCollector.garbageCollector.radius * Mathf.Cos(Mathf.Deg2Rad * angle);
            posY = garbagePosition.y + GarbageCollector.garbageCollector.radius * Mathf.Sin(Mathf.Deg2Rad * angle);

            transform.position = Vector2.Lerp(transform.position, new Vector2(posX, posY), lerpSpeed * Time.deltaTime);
        }
        if (isGarbageCollected && !LinkRay.linkRay.playerLinkedEachOther) 
        {
            StartCoroutine(garbageRigidBrake(1f));
            Vector2 directionToCenter = (Vector2)transform.position - (Vector2)garbageSpawner.transform.position;
            float distanceToCenter = directionToCenter.magnitude;

          
            if (distanceToCenter > 0 && distanceToCenter < blastRadius)
            {
                Vector2 blastForceVector = directionToCenter.normalized * blastForce;
                rb.AddForce(blastForceVector, ForceMode2D.Impulse);
            }

            

        }
        if (!LinkRay.linkRay.playerLinkedEachOther || GlobalVariable.globalVariable.isTriggeredWithObstacle) 
        {
            isGarbageCollected = false;
        }
        if (!isGarbageCollected) 
        {
            // sedikit ada efek blast
            // object sampah mengambang menggunakan sin/cos
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

       
    }
    IEnumerator garbageRigidBrake(float delay)
    {
        yield return new WaitForSeconds(delay);

        
        rb.drag = 5;
       
    }
}
