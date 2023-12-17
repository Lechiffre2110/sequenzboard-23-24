using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text;

//TODO: remove magic numbers
public class Game : IGame
{
    private string _currentSequence;
    private string _currentGameMode;
    private IBoard _board;

    public Game(IBoard board)
    {
        _currentSequence = "";
        _currentGameMode = "";
        _board = board;
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

    public void EndGame()
    {
        throw new System.NotImplementedException();
    }

    public void PauseGame()
    {
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

    public void QuitGame()
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
}