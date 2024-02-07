using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSequenceScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> holds = new List<GameObject>();
    [SerializeField] private Audio audio;

    //Text field for sequencename
    [SerializeField] private TMPro.TMP_InputField sequenceName;
    private string sequenceString = "";

    public delegate void OnCustomSequenceCompleteEvent(string sequence);
    public static event OnCustomSequenceCompleteEvent OnCustomSequenceComplete;

    public delegate void OnSequenceSaveEvent(string name, string sequence);
    public static event OnSequenceSaveEvent OnSequenceSave;

    // Custom SetActive method to reset sequenceString
    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        sequenceString = "";
    }

    /// <summary>
    /// Add a hold to the sequence
    /// </summary>
    /// <param name="hold">The hold to add as a string (e.g. "A")</param>
    private void AddHoldToSequence(string hold)
    {
        char[] holdAsChar = hold.ToCharArray();
        sequenceString += holdAsChar[0].ToString();
        Debug.Log("Sequence: " + GetSequence());
    }

    /// <summary>
    /// Getter for sequenceString
    /// </summary>
    public string GetSequence()
    {
        return sequenceString;
    }

    /// <summary>
    /// Handle game input from the board
    /// </summary>
    /// <param name="hold">The hold that was pressed</param>
    public void HandleSequenceInput(string hold)
    {
        int index = ConvertHoldToIndex(hold);
        GameObject holdObject = holds[index];
        StartCoroutine(ShowHold(holdObject, index));
        AddHoldToSequence(hold);
    }
    
    /// <summary>
    /// Show a hold for a short time
    /// </summary>
    /// <param name="hold">The hold to show</param>
    /// <param name="index">The index of the hold in the hold List</param>
    private IEnumerator ShowHold(GameObject hold, int index)
    {
        PlaySound(index);
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(10000);
        yield return new WaitForSeconds(1f);
        hold.transform.SetSiblingIndex(previousIndex);
    }

    /// <summary>
    /// Handle the completion of a sequence
    /// </summary>
    public void HandleSequenceComplete()
    {
        OnCustomSequenceComplete?.Invoke(sequenceString);
    }

    /// <summary>
    /// Save the current sequence to the database
    /// </summary>
    public void SaveSequenceToDatabase()
    {
        string name = sequenceName.text;
        OnSequenceSave?.Invoke(name, sequenceString);
    }

    /// <summary>
    /// Play hold specific sound
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
        char[] holdNameArray = holdName.ToCharArray();
        switch (holdNameArray[0].ToString())
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
