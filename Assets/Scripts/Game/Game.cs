using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

//TODO: remove magic numbers
public class Game : IGame
{
    private string _currentSequence;
    private string _currentGameMode;
    private IBoard _board;
    private List<char> _inputSequence = new List<char>();
    public string _previousInput = ""; //TODO: adjust logic


    public Game()
    {
        _currentSequence = "";
        _currentGameMode = "";
        _board = new Board();
    }

    public void StartGame(string gameMode)
    {
        _currentGameMode = gameMode;
        _currentSequence = GenerateSequence(10);
        _board.SendMessageToBoard(_currentSequence);
    }

    public string GetCurrentSequence()
    {
        return _currentSequence;
    }

    public void StartGameFromSequence(string sequence)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateGameState() {
        GetBoardInput();
        bool inputIsValid = ValidateBoardInput();
        if (!inputIsValid) {
            //reset game state and give feedback about incorrectness of user input
            _inputSequence = new List<char>();
            _previousInput = "";
            //display wrong input on game running screen
            //reset progress on game running screen
        }
    }

    public void GetBoardInput()
    {
        string input = _board.ReadMessageFromBoard();
        if (input != "" && input != _previousInput)
        {
            _inputSequence.Add(input[0]);
            _previousInput = input;
            Debug.Log("Input: " + input);
        }
    }

    private bool ValidateBoardInput()
    {
        if (_inputSequence.Count == 0)
        {
            return true;
        }
        for (int i = 0; i < _inputSequence.Count; i++)
        {
            if (_inputSequence[i] != _currentSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    public void EndGame()
    {
        // Game end screen with score 
        // Go back to main menu
        throw new System.NotImplementedException();
    }

    public void PauseGame()
    {
        //pause game and show pause screen with resume and restart buttons
        throw new System.NotImplementedException();
    }

    public void ResumeGame()
    {
        throw new System.NotImplementedException();
    }

    public void RestartGame()
    {
        throw new System.NotImplementedException();
    }

    private string GenerateSequence(int sequenceLength)
    {
        StringBuilder sequenceBuilder = new StringBuilder();
        for (int i = 0; i < sequenceLength; i++)
        {
            if (i == 0 || i == 1)
            {
                sequenceBuilder.Append(HoldExtensions.GetRandomHold());
                sequenceBuilder.Append(HoldInstructionExtensions.GetStartHoldInstruction());
            }
            else if (i == sequenceLength - 1 || i == sequenceLength - 2)
            {
                sequenceBuilder.Append(HoldExtensions.GetRandomHold());
                sequenceBuilder.Append(HoldInstructionExtensions.GetEndHoldInstruction());
            }
            else
            {
                sequenceBuilder.Append(HoldExtensions.GetRandomHold());
                sequenceBuilder.Append(HoldInstructionExtensions.GetRandomHoldInstruction());
            }
        }
        string sequence = sequenceBuilder.ToString();
        Debug.Log(sequence);
        return sequence;
    }

    public void QuitGame()
    {
        _board.DisconnectFromBoard();
    }
}