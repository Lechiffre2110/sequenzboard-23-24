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
        InvokeRepeating("UpdateGameState", 0.0f, 1.0f);
    }

    void UpdateGameState()
    {
       _game.UpdateGameState();
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
