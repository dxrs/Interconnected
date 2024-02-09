using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player1Stamina : MonoBehaviour
{
    [SerializeField] Player1Ability player1Ability;

    [SerializeField] Image staminaImg;

    public float maxStamina;
    public float curStamina;
    public float dashStaminaCost;
    public float pullUpStaminaCost;
    public float staminaRegenRate;

    Coroutine staminaRegen;

    private void Start()
    {
        maxStamina = 100;
        curStamina = maxStamina;
    }
    public void staminaFunctionCallback() 
    {
        staminaImg.fillAmount = curStamina / maxStamina;

        if (staminaRegen != null) { StopCoroutine(staminaRegen); }
        staminaRegen = StartCoroutine(staminaRegenerating());
    }

    IEnumerator staminaRegenerating()
    {
        yield return new WaitForSeconds(1f); //tunggu 1 detik untuk regenerate stamina

        do
        {
            if (!player1Ability.isPullingUp)
            {
                curStamina += staminaRegenRate / 10;
                if (curStamina > maxStamina) { curStamina = maxStamina; }
                staminaImg.fillAmount = curStamina / maxStamina;

            }

            yield return new WaitForSeconds(.1f); //rate regenate x/ms
        } while (curStamina < maxStamina);
    }

}
