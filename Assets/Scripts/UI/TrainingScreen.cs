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

    /// <summary>
    /// Set the complete sequence for the training
    /// </summary>
    public void SetCompleteSequence(string sequence)
    {
        _completeSequence = sequence;
    }

    /// <summary>
    /// Start the training by invoking the OnStartTraining event
    /// </summary>
    public void StartTraining() 
    {
        Debug.Log("Training starting");
        OnStartTraining?.Invoke();
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
    /// Indicate that the incorrect hold was pressed
    /// </summary>
    public void IndicateIncorrectHold()
    {
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
    /// Update the game state based on the user input on the board
    /// </summary>
    /// <param name="progress">The current progress</param>
    /// <param name="isCorrecr
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

    /// <summary>
    /// Play a sequence of holds on the screen based on the passed in string
    /// </summary>
    /// <param name="sequence">The sequence to play</param>
    public void PlaySequence(string sequence) {
        StartCoroutine(PlaySequenceCoroutine(sequence));
    }

    /// <summary>
    /// Play a sequence of holds based on the passed in string
    /// </summary>
    /// <param name="sequence">The sequence to play</param>
    IEnumerator PlaySequenceCoroutine(string sequence) {
        foreach (char c in sequence) {
            int index = ConvertHoldToIndex(c.ToString());
            GameObject holdObject = _holds[index];
            StartCoroutine(ShowHold(holdObject, index));
            yield return new WaitForSeconds(1f);
        }
    }

    /// <summary>
    /// Show the passed in hold on the screen
    /// </summary> 
    /// <param name="holdName">The name of the hold to show</param>
    public void ShowHold(string holdName)
    {
        int index = ConvertHoldToIndex(holdName);
        GameObject holdObject = _holds[index];
        StartCoroutine(ShowHold(holdObject, index));
    }

    /// <summary>
    /// Show a hold for a second
    /// </summary>
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

    /// <summary>
    /// Play hold specific sound
    /// </summary>
    /// <param name="index">The index of the sound to play</param>
    private void PlaySound(int index)
    {
        _audio.PlaySound(index);
    }

    /// <summary>
    /// Helper method to convert a hold name to an index
    /// </summary>
    /// <param name="hold">The name of the hold</param>
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
