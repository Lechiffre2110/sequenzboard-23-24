using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;
using System.Linq;

public class Game : IGame
{
    private string _currentSequence = "";
    private string _currentGameMode = "";
    private List<char> _inputSequence = new List<char>();
    public string _previousInput = "";
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


    /// <summary>
    /// Start a new game
    /// </summary>
    /// <param name="gameMode">The game mode to start</param>
    /// <param name="sequence">The sequence to start the game with</param>
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

    /// <summary>
    /// Start a new training game
    /// </summary>
    public void StartTrainingGame() {
        //_currentSequence = sequence;
        _trainingProgress = 0;
        _trainingSubsequenceIndex = 0;
        _inputSequence = new List<char>();
        _trainingSubsequences = SplitSequenceIntoSubsequences(_currentSequence);
        OnTrainingGameStarted("test", _trainingSubsequences[_trainingSubsequenceIndex]);
        PlayerPrefs.SetInt("currentLength", _currentSequence.Length);
    }

    /// <summary>
    /// Validate the input of the training game
    /// </summary>
    /// <param name="input">The input to validate</param>
    /// <returns>True if the input is valid, false otherwise</returns>
    private bool ValidateTrainingInput(string input) {
        for (int i = 0; i < _inputSequence.Count; i++)
        {
            if (_inputSequence[i] != _currentSequence[i])
            {
                return false;
            }
        }
        return true;
    }

    /// <summary>
    /// Update the game state
    /// </summary>
    /// <param name="input">The input to update the game state with</param>
    /// <returns>True if the input is valid, false otherwise</returns>
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

    /// <summary>
    /// Split a sequence into subsequences
    /// </summary>
    /// <param name="sequence">The sequence to split</param>
    /// <returns>An array of subsequences</returns>
    private string[] SplitSequenceIntoSubsequences(string sequence) {
        string[] subsequences = new string[sequence.Length];
        for (int i = 0; i < sequence.Length; i++) {
            subsequences[i] = sequence.Substring(0, i+1);
        }
        return subsequences;
    }


    /// <summary>
    /// Get the current sequence
    /// </summary>
    /// <returns>The current sequence</returns>
    public string GetCurrentSequence()
    {
        return _currentSequence;
    }

    /// <summary>
    /// Update the game state
    /// </summary>
    /// <param name="input">The input to update the game state with</param>
    public void UpdateGameState(string input) {
        _previousInput = input;
        _inputSequence.Add(input[0]);

        bool inputIsValid = ValidateBoardInput();

        if (!inputIsValid) {
            OnGameUpdated(0, false);
            _progress = 0;
            _inputSequence = new List<char>();
            _previousInput = "";
        }

        else {
            _progress++;
            OnGameUpdated(_progress, true);
        }

        if (_progress == _currentSequence.Length) {
            OnGameWon();
        }
    }

    /// <summary>
    /// Validate the input of the game
    /// </summary>
    /// <returns>True if the input is valid, false otherwise</returns>
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
                return false;
            }
        }

        return true;
    }

    /// <summary>
    /// Generate a sequence of a given length
    /// </summary>
    /// <param name="sequenceLength">The length of the sequence to generate</param>
    /// <returns>A string representing the generated sequence</returns>
    private string GenerateSequence(int sequenceLength)
    {
        StringBuilder sequenceBuilder = new StringBuilder();

        for (int i = 0; i < sequenceLength; i++)
        {
            Hold hold;

            do
            {
                hold = HoldExtensions.GetRandomHold();
            } while (i > 0 && sequenceBuilder[i - 1] == hold.ToHoldString()[0]);

            sequenceBuilder.Append(hold.ToHoldString());
        }

        string sequence = sequenceBuilder.ToString();

        return sequence;
    }
}