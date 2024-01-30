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
    public GameObject correctFeedbackFrame;
    public GameObject incorrectFeedbackFrame;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void ShowHold(string holdName)
    {
        GameObject holdObject = sequenceButtons[ConvertHoldToIndex(holdName)];
        StartCoroutine(ShowHold(holdObject));
    }

    private IEnumerator ShowHold(GameObject hold)
    {
        PlaySound(ConvertHoldToIndex(hold.name));
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(1000);
        yield return new WaitForSeconds(1f);
        hold.transform.SetSiblingIndex(previousIndex);
        overlay.transform.SetSiblingIndex(100);
    }

    public void IndicateCorrectHold()
    {
        correctFeedbackFrame.SetActive(true);
        StartCoroutine(HideCorrectFeedbackAfterDelay());
    }

    private IEnumerator HideCorrectFeedbackAfterDelay()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(1f);

        // Hide correct hold frame after the delay
        correctFeedbackFrame.SetActive(false);
    }

    public void IndicateIncorrectHold()
    {
        //show incorrect hold frame for 0.5 seconds
        incorrectFeedbackFrame.SetActive(true);
        StartCoroutine(HideIncorrectFeedbackAfterDelay());
    }

    private IEnumerator HideIncorrectFeedbackAfterDelay()
    {
        // Wait for 0.5 seconds
        yield return new WaitForSeconds(1f);

        // Hide incorrect hold frame after the delay
        incorrectFeedbackFrame.SetActive(false);
    }

    public void UpdateCurrentHold(int currentHold) 
    {
        this.currentHold = currentHold;
    }

    public void ResetGameState() 
    {
        currentHold = 0;
    }

    public void UpdateGameState(int progress, bool isCorrect) 
    {
        if (isCorrect) 
        {
            IndicateCorrectHold();
        }
        else 
        {
            IndicateIncorrectHold();
        }
        UpdateCurrentHold(progress);
        currentHoldText.text = "Griff " + currentHold + "/10";
    }

    public void PlaySound(int index)
    {
        audio.PlaySound(index);
    }

    private int ConvertHoldToIndex(string holdName) 
    {
        switch (holdName) 
        {
            case "A":
                return 0;
            case "B":
                return 1;
            case "C":
                return 2;
            case "D":
                return 3;
            case "E":
                return 4;
            case "F":
                return 5;
            case "G":
                return 6;
            default:
                return 0;
        }
    }
}