using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseInput : MonoBehaviour
{
    public void pausePlayer1Input(InputAction.CallbackContext context)
    {
        Debug.Log("player 1 klik pause" + context.phase);
        if (context.started)
        {
            gamePause();
        }
    }

    public void pausePlayer2Input(InputAction.CallbackContext context)
    {
        Debug.Log("player 2 juga klik" + context.phase);
        if (context.started)
        {
            gamePause();
        }
    }

    private void gamePause()
    {
        if (!Pause.pause.isGamePaused)
        {
            Pause.pause.isGamePaused = true;
        }
        else
        {
            Pause.pause.isGamePaused = false;
        }
    }
}
