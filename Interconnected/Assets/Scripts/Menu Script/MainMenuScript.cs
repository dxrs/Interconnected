using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void survival()
    {
        SceneManager.LoadScene("Level Survival 1");
    }

    public void adventure()
    {
        SceneManager.LoadScene("Level Adv 1");
    }
}
