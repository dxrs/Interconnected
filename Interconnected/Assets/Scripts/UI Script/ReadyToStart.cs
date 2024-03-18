using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyToStart : MonoBehaviour
{
    public static ReadyToStart readyToStart;

    public bool isGameStart;

    [SerializeField] Image imageStart;

    [SerializeField] GameObject startUI;
    [SerializeField] GameObject dialogue;

    Color alphaColor;

 
    private void Awake()
    {
        readyToStart = this;
    }

    private void Start()
    {
        startUI.SetActive(true);
        //dialogue.SetActive(false);
        alphaColor = imageStart.color;
        alphaColor.a = 1;
        imageStart.color = alphaColor;
        
    }

    private void Update()
    {

        StartCoroutine(waitToFadeOut());
        if (alphaColor.a <= 0) 
        {
            isGameStart = true;
            startUI.SetActive(false);
        }
       
       
      
    }

    IEnumerator waitToFadeOut() 
    {
        yield return new WaitForSeconds(.2f);
        alphaColor.a = Mathf.MoveTowards(alphaColor.a, 0, 1f * Time.deltaTime);
        imageStart.color = alphaColor;
       
    }
}
