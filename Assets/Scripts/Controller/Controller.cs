using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private IGame _game;
    [SerializeField] private Board _board;
    [SerializeField] private UserInterface _userInterface;

    void Start()
    {
        _game = new Game();
        _game.StartGame("normal");
    }

    void OnEnable()
    {
        Board.OnBoardMessageReceived += HandleBoardMessageReceived;
        Game.OnGameUpdated += _userInterface.UpdateGameState;
        Game.OnGameStarted += SendSequenceToBoard;
    }

    void OnDisable()
    {
        Board.OnBoardMessageReceived -= HandleBoardMessageReceived;
        Game.OnGameUpdated -= _userInterface.UpdateGameState;
        Game.OnGameStarted -= SendSequenceToBoard;
    }

    void SendSequenceToBoard(string name, string sequence)
    {
        _board.SendMessageToBoard("name sequence");
    }

    void HandleBoardMessageReceived(string input)
    {
        if (input.Contains("[") && input.Contains("]"))
        {
            //Handle any commands from board here
        }
        else if (input.Contains("No"))
        {
            //Ignore any "No message received" messages
        }
        else {
            _game.UpdateGameState(input);
        }
        Debug.Log("CONTROLLER: " + input);
    }

    void HandleGameUpdated(int progress, bool isCorrect)
    {
        _userInterface.UpdateGameState(progress, isCorrect);
    }

    public void StartGameWithRandomSequence()
    {
        _game.StartGame("normal");
        _userInterface.PlaySequence(_game.GetCurrentSequence());
    }
} 
