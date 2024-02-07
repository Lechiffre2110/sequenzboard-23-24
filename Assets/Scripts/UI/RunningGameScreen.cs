using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunningGameScreen : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    [SerializeField] private Audio audio;
    public GameObject[] sequenceButtons;
    public TMP_Text currentHoldText;
    private int currentHold = 0;
    private int sequenceLength = 0;
    public GameObject correctFeedbackFrame;
    public GameObject incorrectFeedbackFrame;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// Show a hold on screen
    /// </summary>
    /// <param name="holdName">The name of the hold to show</param>
    public void ShowHold(string holdName)
    {
        int index = ConvertHoldToIndex(holdName);
        GameObject holdObject = sequenceButtons[index];
        StartCoroutine(ShowHold(holdObject, index));
    }

    /// <summary>
    /// Show a hold for 1 second
    /// </summary>
    /// <param name="hold">The hold to show</param>
    /// <param name="index">The index of the hold</param>
    private IEnumerator ShowHold(GameObject hold, int index)
    {
        PlaySound(index);
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(1000);
        yield return new WaitForSeconds(1f);
        hold.transform.SetSiblingIndex(previousIndex);
        overlay.transform.SetSiblingIndex(100);
    }

    /// <summary>
    /// Indicate that the correct hold was pressed
    /// </summary>
    public void IndicateCorrectHold()
    {
        correctFeedbackFrame.SetActive(true);
        StartCoroutine(HideCorrectFeedbackAfterDelay());
    }

    /// <summary>
    /// Hide the correct hold frame after a delay of 1 second
    /// </summary>
    private IEnumerator HideCorrectFeedbackAfterDelay()
    {
         yield return new WaitForSeconds(1f);
        correctFeedbackFrame.SetActive(false);
    }

    /// <summary>
    /// Indicate that the pressed hold was incorrect
    /// </summary>
    public void IndicateIncorrectHold()
    {
        //show incorrect hold frame for 0.5 seconds
        incorrectFeedbackFrame.SetActive(true);
        StartCoroutine(HideIncorrectFeedbackAfterDelay());
    }

    /// <summary>
    /// Hide the incorrect hold frame after a delay of 1 second
    /// </summary>
    private IEnumerator HideIncorrectFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        incorrectFeedbackFrame.SetActive(false);
    }

    /// <summary>
    /// Update the current hold
    /// </summary>
    /// <param name="currentHold">The index of the current hold</param>
    public void UpdateCurrentHold(int currentHold) 
    {
        this.currentHold = currentHold;
    }

    /// <summary>
    /// Reset the game state
    /// </summary>
    public void ResetGameState() 
    {
        currentHold = 0;
    }

    /// <summary>
    /// Update the game state based on the user input on the board
    /// </summary>
    /// <param name="progress">The current progress</param>
    /// <param name="isCorrect">True if the input was correct, false otherwise</param>
    public void UpdateGameState(int progress, bool isCorrect) 
    {
        sequenceLength = PlayerPrefs.GetInt("currentLength");
        if (isCorrect) 
        {
            IndicateCorrectHold();
        }
        else 
        {
            IndicateIncorrectHold();
        }
        UpdateCurrentHold(progress);
        currentHoldText.text = "Griff " + currentHold + "/" + sequenceLength;
    }

    /// <summary>
    /// Play a sound for a specific hold
    /// </summary>
    /// <param name="index">The index of the hold</param>
    public void PlaySound(int index)
    {
        audio.PlaySound(index);
    }

    /// <summary>
    /// Helper method to convert a hold name to an index
    /// </summary>
    /// <param name="holdName">The name of the hold</param>
    /// <returns>The index of the hold</returns>
    private int ConvertHoldToIndex(string holdName)
    {
        char[] holdChar = holdName.ToCharArray();
        switch (holdChar[0]) 
        {
            case 'A':
                return 0;
            case 'B':
                return 1;
            case 'C':
                return 2;
            case 'D':
                return 3;
            case 'E':
                return 4;
            case 'F':
                return 5;
            case 'G':
                return 6;
            default:
                return 0;
        }
    }
}