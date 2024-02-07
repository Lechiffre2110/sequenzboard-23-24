using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public delegate void OnResumeGameEventHandler();
    public static event OnResumeGameEventHandler OnResumeGame;

    public delegate void OnRestartGameEventHandler();
    public static event OnRestartGameEventHandler OnRestartGame;

    /// <summary>
    /// Resume the game by closing the menu
    /// </summary>
    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Pauses the game by opening the menu
    /// </summary>
    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
