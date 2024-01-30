using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    private IGame _game;
    private Data _data;
    private string _currentScreen = "Custom Sequence";
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
        UserInterface.OnChangeScreen += HandleScreenChange;
        CustomSequenceScreen.OnCustomSequenceComplete += StartGameWithCustomSequence;
        Game.OnGameWon += _userInterface.HandleGameWon;
        LoadSequenceDropdown.OnLoadSequence += StartGameWithCustomSequence;
        _data = new Data();
        CustomSequenceScreen.OnSequenceSave += _data.SaveSequence;
    }

    void OnDisable()
    {
        Board.OnBoardMessageReceived -= HandleBoardMessageReceived;
        Game.OnGameUpdated -= _userInterface.UpdateGameState;
        Game.OnGameStarted -= SendSequenceToBoard;
        UserInterface.OnChangeScreen -= HandleScreenChange;
        CustomSequenceScreen.OnCustomSequenceComplete -= StartGameWithCustomSequence;
        CustomSequenceScreen.OnSequenceSave -= _data.SaveSequence;
    }

    void SendSequenceToBoard(string name, string sequence)
    {
        _board.SendMessageToBoard("!"+sequence);
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

        if (_currentScreen == "Custom Sequence" && input.Length == 1)
        {
            _userInterface.HandleSequenceInput(input);
        } else if (_currentScreen == "Running" && input.Length == 1) {
            _game.UpdateGameState(input);
            _userInterface.ShowHold(input);
        }
        Debug.Log("CONTROLLER: " + input);
    }

    void HandleScreenChange(string screen)
    {
        _currentScreen = screen;
        if (screen == "Custom Sequence")
        {
            _board.SendMessageToBoard("?");
        }
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

    public void StartGameWithCustomSequence(string sequence)
    {
        _game.StartGame("custom", sequence);
        _userInterface.PlaySequence(_game.GetCurrentSequence());
    }
} 
