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
    //private IBoard _board;
    private List<char> _inputSequence = new List<char>();
    public string _previousInput = ""; //TODO: adjust logic
    private int _progress = 0;

    public delegate void OnGameStartedEventHandler(string name, string sequence);
    public static event OnGameStartedEventHandler OnGameStarted;
    public delegate void OnGameUpdatedEventHandler(int progress, bool isCorrect);
    public static event OnGameUpdatedEventHandler OnGameUpdated;

    public static event OnGameWonEventHandler OnGameWon;
    public delegate void OnGameWonEventHandler();


    public Game()
    {
        _currentSequence = "";
        _currentGameMode = "";
    }

    public void StartGame(string gameMode, string sequence = "")
    {
        if (gameMode != "normal" && gameMode != "custom")
        {
            throw new System.ArgumentException("Invalid game mode");
        }
        if (gameMode == "normal")
        {
            _currentSequence = GenerateSequence(10);
            OnGameStarted("Zufallssequenz", _currentSequence);
        } else {
            _currentSequence = sequence;
            OnGameStarted("TEST_NAME", _currentSequence);
        }
    }

    public string GetCurrentSequence()
    {
        return _currentSequence;
    }

    public void StartGameFromSequence(string sequence)
    {
        throw new System.NotImplementedException();
    }

    public void UpdateGameState(string input) {

        _previousInput = input;
        _inputSequence.Add(input[0]);

        bool inputIsValid = ValidateBoardInput();

        if (!inputIsValid) {
            Debug.Log("Wrong input");
            OnGameUpdated(0, false);
            _progress = 0;
            //reset game state and give feedback about incorrectness of user input
            _inputSequence = new List<char>();
            _previousInput = "";
            //display wrong input on game running screen
            //reset progress on game running screen
        }

        else {
            _progress++;
            OnGameUpdated(_progress, true);
        }

        if (_progress == _currentSequence.Length) {
            OnGameWon();
        }
    }

    private bool ValidateBoardInput()
    {
        if (_inputSequence.Count == 0 || _inputSequence.Count > _currentSequence.Length)
        {
            return false;
        }

        for (int i = 0; i < _inputSequence.Count; i++)
        {
            if (_inputSequence[i] != _currentSequence[i])
            {
                Debug.Log("Wrong input at " + i + " expected " + _currentSequence[i] + " got " + _inputSequence[i]);
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
            Hold hold;

            // Never have two of the same holds in a row
            do
            {
                hold = HoldExtensions.GetRandomHold();
            } while (i > 0 && sequenceBuilder[i - 1] == hold.ToHoldString()[0]);

            sequenceBuilder.Append(hold.ToHoldString());
        }

        string sequence = sequenceBuilder.ToString();
        Debug.Log(sequence);
        return sequence;
    }
}