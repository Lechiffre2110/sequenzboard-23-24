using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private IGame _game;
    [SerializeField] private UserInterface _userInterface;


    void Start()
    {
        _game = new Game();
        _game.StartGame("normal");
    }

    void FixedUpdate()
    {
        //_game.StartGame("normal");
    }

    void OnApplicationQuit()
    {
        _game.QuitGame();
    }

    public void StartGameWithRandomSequence()
    {
        _game.StartGame("normal");
        _userInterface.PlaySequence(_game.GetCurrentSequence());
    }
}
