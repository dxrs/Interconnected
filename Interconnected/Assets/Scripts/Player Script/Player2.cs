using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Unity.Mathematics;

public class Player2 : MonoBehaviour
{
    public static Player2 player2;

    [SerializeField] LinkRay linkRay;

    [SerializeField] float curSpeed;
    [SerializeField] float maxSpeed;

    #region basic ability variable
    [Header("Player 2 Basic Ability Dash")]
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration;
    [Header("Player 2 Basic Ability Ghost")]
    public bool isGhosting;
    [SerializeField] float ghostDuration;
    [SerializeField] Color curPlayerTransparentColor;
    float colorGhostA = 0.4f;
    float curColorA = 1f;
    SpriteRenderer spriteRenderer;
    bool isDashing;
    #endregion

    #region variable stamina
    [Header("Player 2 Stamina")]
    [SerializeField] Image staminaImg;
    [SerializeField] float maxStamina;
    [SerializeField] float curStamina;
    [SerializeField] float dashStaminaCost;
    [SerializeField] float ghostStaminaCost;
    [SerializeField] float staminaRegenRate;
    Coroutine staminaRegen;
    #endregion

    #region variable share lives
    [Header("Player 2 Share Lives")]
    public int curPlayer2Health;
    public bool isSharingLivesToP1;
    [SerializeField] Image[] playerHealthImg;
    int maxPlayerHealth = 4;
    #endregion

    bool isBreaking;

    Vector2 inputDir;

    Rigidbody2D rb;

    private void Awake()
    {
        if (player2 == null) { player2 = this; }
    }

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        isBreaking = true;
        curPlayer2Health = maxPlayerHealth;
        curPlayerTransparentColor.a = curColorA;
        spriteRenderer.color = curPlayerTransparentColor;
    }
    private void Update()
    {
        Ghosting();
        changeLayer();
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
        player2IsBreaking();
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

    #region player 2 share lives
    public void p2ShareLives(InputAction.CallbackContext context)
    {
        if (context.started && !Player1.player1.isSharingLivesToP2 && linkRay.playerLinkedEachOther) 
        {
            if (curPlayer2Health > 1 && Player1.player1.curPlayer1Health < maxPlayerHealth)
            {
                Debug.Log(context.phase);
                isSharingLivesToP1 = true;
            }
            
        }
        if (context.performed && linkRay.playerLinkedEachOther && isSharingLivesToP1)
        {
            Debug.Log("share lives to p1!!!" + context.phase);
            if (curPlayer2Health > 1 && Player1.player1.curPlayer1Health < maxPlayerHealth)
            {
                curPlayer2Health--;
                if (Player1.player1.curPlayer1Health < maxPlayerHealth && Player1.player1 != null)
                {
                    Player1.player1.curPlayer1Health++;
                }
            }

        }
        if (context.canceled && isSharingLivesToP1) 
        { 
            Debug.Log(context.phase);
            isSharingLivesToP1 = false;
        }
    }

    private void shareLives()
    {
        for (int i = 0; i < playerHealthImg.Length; i++)
        {

            int clampedIndex = Mathf.Max(0, Mathf.Min(i, maxPlayerHealth - 1)); //hitung indeks yang dibatasi dalam rentang 0 hingga maxHealth - 1

            bool shouldEnable = curPlayer2Health >= i + 1;
            playerHealthImg[clampedIndex].enabled = shouldEnable;
        }
    }


    #endregion

    #region player 2 movement function
    public void p2Move(InputAction.CallbackContext context)
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

    #region player 2 breaking function
    void player2IsBreaking()
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
                curStamina -= dashStaminaCost;
                if (curStamina < 0) { curStamina = 0; }
                staminaImg.fillAmount = curStamina / maxStamina;

                if (staminaRegen != null) { StopCoroutine(staminaRegen); }
                staminaRegen = StartCoroutine(staminaRegenerating());
            }
            
        }
    }

    public void player1Ghosting(InputAction.CallbackContext context)
    {
        if (context.performed && !isGhosting)
        {
            if (curStamina > ghostStaminaCost)
            {
                isGhosting = true;
                linkRay.player2LinkedToObstacle = false;
                curStamina -= ghostStaminaCost; //-> nanti di perbaiki
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
        if (isGhosting && ghostDuration <= 0)
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

    #region change link dan change layer player 2

    private void changeLayer()
    {
        if (!linkRay.isLinkedToPlayer)
        {
            gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else
        {
            gameObject.layer = LayerMask.NameToLayer("Obstacle");
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

    //gamepad disconnect
    public void gamepadDiconnected() 
    {
        //disconnect function
    }

    //gamepad reconnect
    public void gamepadReconnect() 
    {
        //reconnect function
    }
}
