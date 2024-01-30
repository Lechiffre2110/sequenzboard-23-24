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


    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
        sequenceString = "";
    }

    private void AddHoldToSequence(string hold)
    {
        if (hold.Length == 1 && hold[0] >= 'A' && hold[0] <= 'G')
        {
            sequenceString += hold;
        }
        Debug.Log("Sequence: " + GetSequence());
    }

    public string GetSequence()
    {
        return sequenceString;
    }

    public void HandleSequenceInput(string hold)
    {
        string holdName = "Hold " + hold;
        GameObject holdObject = GameObject.Find(holdName);
        StartCoroutine(ShowHold(holdObject));
        AddHoldToSequence(hold);
    }
    
    private IEnumerator ShowHold(GameObject hold)
    {
        PlaySound(ConvertHoldToIndex(hold.name));
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(10000);
        yield return new WaitForSeconds(1f);
        hold.transform.SetSiblingIndex(previousIndex);
    }

    public void HandleSequenceComplete()
    {
        OnCustomSequenceComplete?.Invoke(sequenceString);
    }

    public void SaveSequenceToDatabase()
    {
        string name = sequenceName.text;
        OnSequenceSave?.Invoke(name, sequenceString);
    }

    public void PlaySound(int index)
    {
        audio.PlaySound(index);
    }

    private int ConvertHoldToIndex(string holdName)
    {
        switch (holdName)
        {
            case "Hold A":
                return 0;
            case "Hold B":
                return 1;
            case "Hold C":
                return 2;
            case "Hold D":
                return 3;
            case "Hold E":
                return 4;
            case "Hold F":
                return 5;
            case "Hold G":
                return 6;
            default:
                return 7;
        }
    }
}
