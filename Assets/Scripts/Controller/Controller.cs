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

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnApplicationQuit()
    {
        _board.DisconnectFromBoard();
    }
}
