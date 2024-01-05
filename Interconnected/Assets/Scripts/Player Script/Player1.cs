using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Mathematics;
using Unity.VisualScripting;

public class Player1 : MonoBehaviour
{
    public static Player1 player1;

    public int player1DoorValue;

    [SerializeField] LinkRay linkRay;
    [SerializeField] GlobalVariable globalVariable;

    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;

    [SerializeField] bool isMovePosition;

    [SerializeField] Vector2 playerRespawnPos;

    #region variable knocked out
    [Header("Player 1 Knocked Out")]
    public bool isKnockedOut;
    public int reviveCountValue;
    [SerializeField] float knockTimer;
    float curKnockTimer;
    #endregion

    #region  variable basic ability
    [Header("Player 1 Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [Header("Player 1 Ghost")]
    public bool isGhosting;
    [SerializeField] float ghostDuration;
    [SerializeField] Color curPlayerTransparentColor;
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
        if (player1 == null) { player1 = this; }
    }

    private void Start()
    {
        cc = GetComponent<CircleCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isBreaking = true;
        curPlayer1Health = maxPlayerHealth;
        curPlayerTransparentColor.a = curColorA;
        spriteRenderer.color = curPlayerTransparentColor;
        curKnockTimer = knockTimer;
        
    }

    private void Update()
    {
        if (isKnockedOut)
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
        changeLayer();
        player1KnockedOut();
        player1Destroy();
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
            curStamina += staminaRegenRate / 10;
            if (curStamina > maxStamina) { curStamina = maxStamina; }
            staminaImg.fillAmount = curStamina / maxStamina;
            yield return new WaitForSeconds(.1f); //rate regenate x/ms
        } while (curStamina < maxStamina);
    }

    public void player1SetPos() 
    {
        if (isMovePosition)
        {


            transform.position = playerRespawnPos;
            /*
            for (int i = 0; i < SpawnerValue.spawnerValue.spawnerValuerIndex.Length; i++)
            {
                if (SpawnerValue.spawnerValue.spawnerValuerIndex[i] == 1)
                {
                    transform.position = SpawnerValue.spawnerValue.player1SpawnPos[i];
                    break;  // Keluar dari loop setelah menemukan indeks yang sesuai
                }
            }
            */
        }

        isMovePosition = globalVariable.isTriggeredWithObstacle;
    }

    #region player 1 health and destroy
    private void player1KnockedOut() 
    {
        if (curPlayer1Health <= 0 && globalVariable.isEnteringSurvivalArea) 
        {
            isKnockedOut = true;
        }
        if (isKnockedOut) 
        {
            cc.isTrigger = true;
            if (knockTimer > 0)
            {
                knockTimer -= 1 * Time.deltaTime;
            }
            if (knockTimer <= 0) { Destroy(gameObject); }
        }
        else 
        {
            reviveCountValue = 3;
            knockTimer = curKnockTimer;
            cc.isTrigger = false;
        }
    }

    private void player1Destroy() 
    {
        if (curPlayer1Health <= 0 && !globalVariable.isEnteringSurvivalArea) 
        {
            Destroy(gameObject);
        }
    }

    #endregion

    #region player 1 share lives

    public void p1ShareLives(InputAction.CallbackContext context)
    {
        if (!isKnockedOut)
        {
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
        if (!isKnockedOut) 
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
            rb.drag = math.lerp(rb.drag, 3, 1 * Time.deltaTime);
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
        if (!isKnockedOut) 
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

    public void player1Ghosting(InputAction.CallbackContext context) 
    {
        if (!isKnockedOut) 
        {
            if (context.performed && !isShielding)
            {
                if (curStamina > shieldStaminaCost)
                {
                    isShielding = true;
                    //linkRay.player1LinkedToObstacle = false;
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
            //cc.enabled = false;
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
            //cc.enabled = true;
            player1Shield.SetActive(false);
        }
    }
    private void Ghosting() 
    {
        if (isGhosting)
        {
            cc.enabled = false;
            curColorA = math.lerp(curColorA, colorGhostA, 1.5f * Time.deltaTime);
            curPlayerTransparentColor.a = curColorA;
            spriteRenderer.color = curPlayerTransparentColor;
            if (ghostDuration > 0)
            {
                ghostDuration -= 1 * Time.deltaTime;
            }
        }
        if (isGhosting && ghostDuration <= 0)
        {
            isGhosting = false;
            ghostDuration = 10;
        }
        if (!isGhosting)
        {
            cc.enabled = true;
            curColorA = math.lerp(curColorA, 1, 1.5f * Time.deltaTime);
            curPlayerTransparentColor.a = curColorA;
            spriteRenderer.color = curPlayerTransparentColor;
        }
     
       
    }

    #endregion

    #region change link and change layer player 1
    private void changeLayer()
    {
        if (!linkRay.isLinkedToPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Player 1");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }
    public void changeLinkMethod(InputAction.CallbackContext context)
    {
        if (!isKnockedOut) 
        {
            if (context.performed && !isGhosting)
            {

                if (!linkRay.isLinkedToPlayer)
                {
                    linkRay.isLinkedToPlayer = true;
                }
                else
                {
                    linkRay.playerLinkedEachOther = false;
                    linkRay.isLinkedToPlayer = false;
                }
            }
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
                StartCoroutine(backToFalse());
            }
        }
        
        

        if(collision.gameObject.tag=="Player 2") 
        {
            if (Player2.player2.isKnockedOut) 
            {
                if (Player2.player2.reviveCountValue > 0) 
                {
                    Player2.player2.reviveCountValue--;
                }
                if (Player2.player2.reviveCountValue <= 0) 
                {
                    Player2.player2.curPlayer2Health += 1;
                    Player2.player2.isKnockedOut = false;
                }
            }
        }

        if(collision.gameObject.tag=="Player 2 Shield") 
        {
            isShielding = false;
            Player2.player2.isShielding = false;
        }

        if (!linkRay.isLinkedToPlayer && !isGhosting) 
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
            curPlayer1Health--;
            if (curPlayer1Health <= 0) { curPlayer1Health = 0; }
        }

       if(collision.gameObject.tag=="Door Button P1") 
        {
            //player1DoorValue = 1;
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        
    }

    IEnumerator backToFalse() 
    {
        yield return new WaitForSeconds(1);
        globalVariable.isTriggeredWithObstacle = false;
    }
}
