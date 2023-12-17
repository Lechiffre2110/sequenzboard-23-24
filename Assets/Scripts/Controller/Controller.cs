using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private IBoard _board;
    private IGame _game;

    void Start()
    {
        _board = new Board();
        _board.ConnectToBoard();
        
        _game = new Game(_board);
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
