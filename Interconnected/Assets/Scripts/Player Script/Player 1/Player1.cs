using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Player1 : MonoBehaviour
{
    //public static Player1 player1;

    public int player1DoorValue;
    public ParticleSystem deathParticle;

    [SerializeField] LinkRay linkRay;
    [SerializeField] GlobalVariable globalVariable;

    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;
    [SerializeField] float playerCrashForceValue;

    [SerializeField] bool isMovePosition;


    [SerializeField] Vector2 playerRespawnPos;



    #region  variable basic ability
    [Header("Player 1 Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [Header("Player 1 Shield")]
    public bool isShielding;
    [SerializeField] GameObject player1Shield;
    [SerializeField] float shieldDuration;
    float colorGhostA = 0.4f;
    float curColorA = 1f;
    SpriteRenderer spriteRenderer;
    bool isDashing;
    #endregion

    #region variable stamina
    [Header("Player 1 Stamina")]
    [SerializeField] Image staminaImg;
    [SerializeField] float maxStamina;
    [SerializeField] float curStamina;
    [SerializeField] float dashStaminaCost;
    [SerializeField] float ghostStaminaCost;
    [SerializeField] float shieldStaminaCost;
    [SerializeField] float staminaRegenRate;
    Coroutine staminaRegen;
    #endregion

    #region variable share lives
    [Header("Player 1 Share Lives")]
    public int curPlayer1Health;
    public bool isSharingLivesToP2;
    [SerializeField] Image[] playerHealthImg;
    int maxPlayerHealth = 4;
    #endregion

    bool isBreaking;

    Vector2 inputDir;

    Rigidbody2D rb;

    CircleCollider2D cc;

    private void Awake()
    {
        //if (player1 == null) { player1 = this; }
    }

    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isBreaking = true;
        curPlayer1Health = maxPlayerHealth;


        
    }

    private void Update()
    {
        if (globalVariable.isTriggeredWithObstacle || globalVariable.isGameFinish)
        {
            maxSpeed = 0;
        }
        else
        {
            maxSpeed = curSpeed;
        }

        player1Shield.transform.position = transform.position;

        player1SetPos();
        Shielding();
        shareLives();
   
    }

    private void FixedUpdate()
    {
        if (isDashing) 
        {
            return;
        }
        rb.AddForce(inputDir * curSpeed);
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, maxSpeed);
        player1IsBreaking();
    }

    IEnumerator staminaRegenerating()
    {
        yield return new WaitForSeconds(1f); //tunggu 1 detik untuk regenerate stamina

        do
        {
            if (!isShielding) 
            {
                curStamina += staminaRegenRate / 10;
                if (curStamina > maxStamina) { curStamina = maxStamina; }
                staminaImg.fillAmount = curStamina / maxStamina;
               
            }

            yield return new WaitForSeconds(.1f); //rate regenate x/ms
        } while (curStamina < maxStamina);
    }

    public void player1SetPos() 
    {
        if (isMovePosition)
        {

            StartCoroutine(waitToSetPos());
           
        }

        isMovePosition = globalVariable.isTriggeredWithObstacle;
    }
    IEnumerator waitToSetPos() 
    {
        yield return new WaitForSeconds(1);
        transform.position = playerRespawnPos;
    }


    #region player 1 share lives

    public void p1ShareLives(InputAction.CallbackContext context)
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart)
        {
            /*
            if (context.started && !Player2.player2.isSharingLivesToP1 && linkRay.playerLinkedEachOther)
            {

                if (curPlayer1Health > 1 && Player2.player2.curPlayer2Health < maxPlayerHealth)
                {
                    Debug.Log(context.phase);
                    isSharingLivesToP2 = true;
                }


            }
            if (context.performed && linkRay.playerLinkedEachOther && isSharingLivesToP2)
            {
                Debug.Log("share lives to p2!!!" + context.phase);
                if (curPlayer1Health > 1 && Player2.player2.curPlayer2Health < maxPlayerHealth)
                {
                    curPlayer1Health--;
                    if (Player2.player2.curPlayer2Health < maxPlayerHealth && Player2.player2 != null)
                    {
                        Player2.player2.curPlayer2Health++;
                    }
                }

            }
            */
            if (context.canceled && isSharingLivesToP2)
            {
                Debug.Log(context.phase);
                isSharingLivesToP2 = false;
            }
        }
        

    }
    private void shareLives() 
    {
        for (int i = 0; i < playerHealthImg.Length; i++)
        {

            int clampedIndex = Mathf.Max(0, Mathf.Min(i, maxPlayerHealth - 1)); //hitung indeks yang dibatasi dalam rentang 0 hingga maxHealth - 1

            bool shouldEnable = curPlayer1Health >= i + 1;
            playerHealthImg[clampedIndex].enabled = shouldEnable;
        }
       
    }
    #endregion

    #region player 1 movement 
    public void p1Move(InputAction.CallbackContext context) 
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart) 
        {
            if (context.performed)
            {
                isBreaking = false;
            }
            else
            {
                isBreaking = true;
            }

            inputDir = context.ReadValue<Vector2>();
        }
        
    }
    #endregion

    #region player 1 breaking 
    void player1IsBreaking() 
    {
        if (isBreaking)
        {
            rb.drag = math.lerp(rb.drag, 5, 1 * Time.deltaTime);
        }
        else
        {
            rb.drag = 0;
        }
    }
    #endregion

    #region player 1 basic ability

    public void player1Dashing(InputAction.CallbackContext context) 
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart)
        {
            if (context.performed && !isBreaking)
            {
                if (curStamina > dashStaminaCost)
                {
                    StartCoroutine(Dashing());
                    curStamina -= dashStaminaCost; // laporan
                    if (curStamina < 0) { curStamina = 0; }
                    staminaImg.fillAmount = curStamina / maxStamina;

                    if (staminaRegen != null) { StopCoroutine(staminaRegen); }
                    staminaRegen = StartCoroutine(staminaRegenerating());
                }


            }
        }
        
    }

    public void player1Shielding(InputAction.CallbackContext context) 
    {
        if (!globalVariable.isTriggeredWithObstacle
            && !globalVariable.isGameFinish
            && !globalVariable.isGameOver
            && !Pause.pause.isGamePaused
            && ReadyToStart.readyToStart.isGameStart) 
        {
            if (context.performed && !isShielding)
            {
                if (curStamina > shieldStaminaCost)
                {
                    isShielding = true;
                    curStamina -= shieldStaminaCost; //-> nanti di perbaiki //laporan
                    if (curStamina < 0) { curStamina = 0; }
                    staminaImg.fillAmount = curStamina / maxStamina;

                    if (staminaRegen != null) { StopCoroutine(staminaRegen); }
                    staminaRegen = StartCoroutine(staminaRegenerating());
                }



            }
        }
       
    }

    IEnumerator Dashing()
    {
        isDashing = true;
        rb.velocity = new Vector2(inputDir.x * dashSpeed, inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private void Shielding() 
    {
        if (isShielding) 
        {

            player1Shield.SetActive(true);
            if (shieldDuration > 0) 
            {
                shieldDuration -= 1 * Time.deltaTime;
            }
        }

        if(isShielding && shieldDuration <= 0) 
        {
            isShielding = false;
            shieldDuration = 10;
        }

        if (!isShielding) 
        {
            shieldDuration = 10;
            player1Shield.SetActive(false);
        }
    }

    #endregion

   

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Gear")
        {
            if (!isShielding)
            {
                curPlayer1Health--;
                Instantiate(deathParticle,this.transform.position,Quaternion.identity);
                globalVariable.isTriggeredWithObstacle = true;
                StartCoroutine(backToFalse());

            }
        
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Spike" || collision.gameObject.tag == "Gear")
        {
            if (!isShielding) 
            {
                curPlayer1Health--;
                globalVariable.isTriggeredWithObstacle = true;
                Instantiate(deathParticle,this.transform.position,Quaternion.identity);
                StartCoroutine(backToFalse());
            }
        }
        
        

        if(collision.gameObject.tag=="Player 2") 
        {
           // Vector2 crashForcePos = (transform.position - collision.transform.position).normalized;
           // rb.AddForce(crashForcePos * playerCrashForceValue, ForceMode2D.Impulse);

           
        }

        if(collision.gameObject.tag=="Player 2 Shield") 
        {
            isShielding = false;
            //Player2.player2.isShielding = false;
        }

        if (!linkRay.isLinkedToPlayer) 
        {
            if(collision.gameObject.tag=="Bullet P2") 
            {
                curStamina -= .5f; //-> nanti di perbaiki //laporan
                if (curStamina < 0) { curStamina = 0; }
                staminaImg.fillAmount = curStamina / maxStamina;

                if (staminaRegen != null) { StopCoroutine(staminaRegen); }
                staminaRegen = StartCoroutine(staminaRegenerating());
            }
        }

        if (collision.gameObject.tag == "Enemy") 
        {

            if (!isShielding)
            {
                //curPlayer1Health--;
                Instantiate(deathParticle, this.transform.position, Quaternion.identity);

            }
        }

       
        
    }


    IEnumerator backToFalse() 
    {
        yield return new WaitForSeconds(1);
        globalVariable.isTriggeredWithObstacle = false;
    }
}
