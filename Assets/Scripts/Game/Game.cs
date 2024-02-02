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

    private string[] _trainingSubsequences;
    private int _trainingSubsequenceIndex = 0;

    private int _trainingProgress = 0;

    private string _previousTrainingInput = "";
    private int _length = 0;


    public delegate void OnGameStartedEventHandler(string name, string sequence);
    public static event OnGameStartedEventHandler OnGameStarted;
    public delegate void OnGameUpdatedEventHandler(int progress, bool isCorrect);
    public static event OnGameUpdatedEventHandler OnGameUpdated;
    public static event OnGameWonEventHandler OnGameWon;
    public delegate void OnGameWonEventHandler();
    public delegate void OnTrainingGameStartedEventHandler(string name, string sequence);
    public static event OnTrainingGameStartedEventHandler OnTrainingGameStarted;
    public delegate void OnTrainingGameUpdatedEventHandler(int progress, bool isCorrect);
    public static event OnTrainingGameUpdatedEventHandler OnTrainingGameUpdated;


    public Game()
    {
        _currentSequence = "";
        _currentGameMode = "";
    }

    public void StartGame(string gameMode, string sequence = "")
    {
        if (gameMode != "normal" && gameMode != "custom" && gameMode != "training")
        {
            throw new System.ArgumentException("Invalid game mode");
        }
        if (gameMode == "normal")
        {
            _length = PlayerPrefs.GetInt("length");
            _currentSequence = GenerateSequence(_length);
            _progress = 0;
            _inputSequence = new List<char>();
            OnGameStarted("Zufallssequenz", _currentSequence);
            PlayerPrefs.SetInt("currentLength", _length);
            
        } 
        else {
            _currentSequence = sequence;
            _progress = 0;
            _inputSequence = new List<char>();
            OnGameStarted("Custom", _currentSequence);
            PlayerPrefs.SetInt("currentLength", _currentSequence.Length);
        }
    }

    public void StartTrainingGame() {
        //_currentSequence = sequence;
        _trainingProgress = 0;
        _trainingSubsequenceIndex = 0;
        _inputSequence = new List<char>();
        _trainingSubsequences = SplitSequenceIntoSubsequences(_currentSequence);
        OnTrainingGameStarted("test", _trainingSubsequences[_trainingSubsequenceIndex]);
        PlayerPrefs.SetInt("currentLength", _currentSequence.Length);
    }

    public bool ValidateTrainingInput(string input) {
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

    public void UpdateTrainingGameState(string input) {
        _previousTrainingInput = input;
        _inputSequence.Add(input[0]);

        bool inputIsValid = ValidateTrainingInput(input);

        if (!inputIsValid) {
            OnTrainingGameUpdated(0, false);
            _trainingProgress = 0;
            _inputSequence = new List<char>();
            _previousTrainingInput = "";
        }

        else {
            _trainingProgress++;
            OnTrainingGameUpdated(_trainingProgress, true);
        }

        if (_trainingProgress == _trainingSubsequences[_trainingSubsequenceIndex].Length) {
            _trainingSubsequenceIndex++;
            if (_trainingSubsequenceIndex == _trainingSubsequences.Length) {
                Debug.Log("Training game won");
            }
            else {
                _trainingProgress = 0;
                _inputSequence = new List<char>();
                OnTrainingGameStarted("training", _trainingSubsequences[_trainingSubsequenceIndex]);
            }
        }
    }

    //TRAINING MODE
    private string[] SplitSequenceIntoSubsequences(string sequence) {
        string[] subsequences = new string[sequence.Length];
        for (int i = 0; i < sequence.Length; i++) {
            subsequences[i] = sequence.Substring(0, i+1);
            Debug.Log("Subsequence " + i + ": " + subsequences[i]);
        }
        return subsequences;
    }

    //END TRAINING MODE

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