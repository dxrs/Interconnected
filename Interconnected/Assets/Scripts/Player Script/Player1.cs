using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Mathematics;

public class Player1 : MonoBehaviour
{
    public static Player1 player1;

    [SerializeField] LinkRay linkRay;

    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;

    #region  variable basic ability
    [Header("Player 1 Basic Ability Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [Header("Player 1 Basic Ability Ghost")]
    public bool isGhosting;
    [SerializeField] float ghostDuration;
    [SerializeField] Color curPlayerTransparentColor;
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

    private void Awake()
    {
        if (player1 == null) { player1 = this; }
    }

    private void Start()
    {
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isBreaking = true;
        curPlayer1Health = maxPlayerHealth;
        curPlayerTransparentColor.a = curColorA;
        spriteRenderer.color = curPlayerTransparentColor;
        
    }

    private void Update()
    {
        Ghosting();
        shareLives();
        changeLayer();
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

    #region player 1 share lives

    public void p1ShareLives(InputAction.CallbackContext context)
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
        if (context.performed && !isBreaking) 
        {
            if (curStamina > dashStaminaCost) 
            {
                StartCoroutine(dashing());
                curStamina -= dashStaminaCost; // laporan
                if (curStamina < 0) { curStamina = 0; }
                staminaImg.fillAmount = curStamina / maxStamina;

                if (staminaRegen != null) { StopCoroutine(staminaRegen); }
                staminaRegen = StartCoroutine(staminaRegenerating());
            }
            
            
        }
    }

    public void player1Ghosting(InputAction.CallbackContext context) 
    {
        if(context.performed && !isGhosting) 
        {
            if (curStamina > ghostStaminaCost) 
            {
                isGhosting = true;
                linkRay.player1LinkedToObstacle = false;
                curStamina -= ghostStaminaCost; //-> nanti di perbaiki //laporan
                if (curStamina < 0) { curStamina = 0; }
                staminaImg.fillAmount = curStamina / maxStamina;

                if (staminaRegen != null) { StopCoroutine(staminaRegen); }
                staminaRegen = StartCoroutine(staminaRegenerating());
            }

            
           
        }
    }

    IEnumerator dashing()
    {
        isDashing = true;
        rb.velocity = new Vector2(inputDir.x * dashSpeed, inputDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;
    }

    private void Ghosting() 
    {
        if (isGhosting) 
        {
            curColorA = math.lerp(curColorA, colorGhostA, 1.5f * Time.deltaTime);
            curPlayerTransparentColor.a = curColorA;
            spriteRenderer.color = curPlayerTransparentColor;
            if (ghostDuration > 0) 
            {
                ghostDuration -= 1 * Time.deltaTime;
            }
        }
        if(isGhosting && ghostDuration <= 0) 
        {
            isGhosting = false;
            ghostDuration = 10;
        }
        if (!isGhosting) 
        {
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
    #endregion

    private void OnTriggerEnter2D(Collider2D collision)
    {
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

        /*
        if(collision.gameObject.tag=="Enemy Trigger") 
        {
            if (EnemyTrigger.enemyTrigger.id == 1) 
            {
                Debug.Log("ini id = " + EnemyTrigger.enemyTrigger.id);
            }
            if (EnemyTrigger.enemyTrigger.id == 2)
            {
                Debug.Log("ini id = " + EnemyTrigger.enemyTrigger.id);
            }
        }
        */
    }
}
