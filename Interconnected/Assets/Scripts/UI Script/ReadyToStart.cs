using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ReadyToStart : MonoBehaviour
{
    public static ReadyToStart readyToStart;

    public bool isGameStart;

    [SerializeField] Image imageTransition;

    Color alphaColor;

 
    private void Awake()
    {
        readyToStart = this;
    }

    private void Start()
    {
        alphaColor = imageTransition.color;
        alphaColor.a = 1;
        imageTransition.color = alphaColor;
        
    }

    private void Update()
    {
        if (!SceneSystem.sceneSystem.isChangeScene) 
        {
            StartCoroutine(waitToFadeOut());
        }
        else 
        {
            alphaColor.a = Mathf.MoveTowards(alphaColor.a, 1, 2f * Time.unscaledDeltaTime);
            imageTransition.color = alphaColor;
        }

        if (alphaColor.a <= 0) 
        {
            isGameStart = true;
        }
       
      
    }

    IEnumerator waitToFadeOut() 
    {
        yield return new WaitForSeconds(.2f);
        alphaColor.a = Mathf.MoveTowards(alphaColor.a, 0, 1f * Time.deltaTime);
        imageTransition.color = alphaColor;
       
       
       
    }
}
