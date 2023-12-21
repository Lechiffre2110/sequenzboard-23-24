using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private IGame _game;

    void Start()
    {
        _game = new Game();
        _game.StartGame("normal");
    }

    void FixedUpdate()
    {
        _game.StartGame("normal");
    }

    void OnApplicationQuit()
    {
        _game.QuitGame();
    }
}
