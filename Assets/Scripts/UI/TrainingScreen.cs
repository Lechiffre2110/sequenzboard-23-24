using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrainingScreen : MonoBehaviour
{
    [SerializeField] private GameObject[] _holds;
    [SerializeField] private Audio _audio;

    [SerializeField] private GameObject correctFeedbackFrame;

    [SerializeField] private GameObject incorrectFeedbackFrame;
    [SerializeField] private TMPro.TMP_Text _currentHoldText;
    [SerializeField] private GameObject overlay;
    private string _completeSequence = "";
    private int sequenceLength = 0;

    public delegate void OnStartTrainingEvent();
    public static event OnStartTrainingEvent OnStartTraining;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    public void SetCompleteSequence(string sequence)
    {
        _completeSequence = sequence;
    }

    public void StartTraining() 
    {
        Debug.Log("Training starting");
        OnStartTraining?.Invoke();
    }

    public void IndicateCorrectHold()
    {
        correctFeedbackFrame.SetActive(true);
        StartCoroutine(HideCorrectFeedbackAfterDelay());
    }

    private IEnumerator HideCorrectFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        correctFeedbackFrame.SetActive(false);
    }

    public void IndicateIncorrectHold()
    {
        incorrectFeedbackFrame.SetActive(true);
        StartCoroutine(HideIncorrectFeedbackAfterDelay());
    }

    private IEnumerator HideIncorrectFeedbackAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        incorrectFeedbackFrame.SetActive(false);
    }

    public void UpdateGameState(int progress, bool isCorrect) 
    {
        sequenceLength = PlayerPrefs.GetInt("sequenceLength");
        if (isCorrect) 
        {
            IndicateCorrectHold();
        }
        else 
        {
            IndicateIncorrectHold();
        }
        _currentHoldText.text = "Griff " + progress + "/" + sequenceLength;
    }


    public void PlaySequence(string sequence) {
        StartCoroutine(PlaySequenceCoroutine(sequence));
    }

    IEnumerator PlaySequenceCoroutine(string sequence) {
        foreach (char c in sequence) {
            int index = ConvertHoldToIndex(c.ToString());
            GameObject holdObject = _holds[index];
            StartCoroutine(ShowHold(holdObject, index));
            yield return new WaitForSeconds(1f);
        }
    }

    public void ShowHold(string holdName)
    {
        int index = ConvertHoldToIndex(holdName);
        GameObject holdObject = _holds[index];
        StartCoroutine(ShowHold(holdObject, index));
    }

    private IEnumerator ShowHold(GameObject hold, int index)
    {
        Debug.Log("Showing hold " + hold.name);
        PlaySound(index);
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(1000);
        yield return new WaitForSeconds(1f);
        hold.transform.SetSiblingIndex(previousIndex);
        overlay.transform.SetSiblingIndex(100);
    }

    private void PlaySound(int index)
    {
        _audio.PlaySound(index);
    }

    private int ConvertHoldToIndex(string hold)
    {
        char[] holdArray = hold.ToCharArray();
        hold = holdArray[0].ToString();
        switch (hold)
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
