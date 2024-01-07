using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RunningGameScreen : MonoBehaviour
{
    public GameObject[] sequenceButtons;
    public TMP_Text timerText;
    private float startTime;
    private int currentHold = 0;
    public GameObject correctFeedbackFrame;
    public GameObject incorrectFeedbackFrame;

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the start time when the game starts
        startTime = Time.time;
        UpdateTimerText();
    }

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer text every frame
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
        // Start or restart the timer when needed
        startTime = Time.time;
    }

    public void ShowHold(string holdName)
    {
        Debug.Log("Showing hold " + holdName);
    }

    public void IndicateCorrectHold()
    {
        correctFeedbackFrame.SetActive(true);
        //sleep 0.5s
        correctFeedbackFrame.SetActive(false);
    }

    public void IndicateIncorrectHold()
    {
        //show incorrect hold frame for 0.5 seconds
        incorrectFeedbackFrame.SetActive(true);
        //sleep 0.5s
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

    //TODO: rewrite into game state object

}