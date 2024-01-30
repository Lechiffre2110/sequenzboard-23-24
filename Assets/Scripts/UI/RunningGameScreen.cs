using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunningGameScreen : MonoBehaviour
{
    [SerializeField] private GameObject overlay;
    public GameObject[] sequenceButtons;
    public TMP_Text timerText;
    public TMP_Text currentHoldText;
    private float startTime;
    private int currentHold = 0;
    public GameObject correctFeedbackFrame;
    public GameObject incorrectFeedbackFrame;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        UpdateTimerText();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    void Update()
    {
        UpdateTimerText();
    }

    void UpdateTimerText()
    {
        // Calculate elapsed time in seconds and milliseconds
        float elapsedTime = Time.time - startTime;
        int seconds = Mathf.FloorToInt(elapsedTime);
        int milliseconds = (int)Math.Round((elapsedTime - seconds) * 1000, 1);

        // Update the timer text with milliseconds formatted to 2 digits
        timerText.text = string.Format("Zeit: {0:D2}:{1:00}", seconds, milliseconds);
    }

    public void StartTimer()
    {
        startTime = Time.time;
    }

    public void ShowHold(string holdName)
    {
        GameObject holdObject = sequenceButtons[ConvertHoldToIndex(holdName)];
        StartCoroutine(ShowHold(holdObject));
    }

    private IEnumerator ShowHold(GameObject hold)
    {
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(1000);
        yield return new WaitForSeconds(0.5f);
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
        yield return new WaitForSeconds(0.5f);

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
        yield return new WaitForSeconds(0.5f);

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

    public int ConvertHoldToIndex(string holdName) 
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