using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomSequenceScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> holds = new List<GameObject>();

    //Text field for sequencename
    [SerializeField] private TMPro.TMP_InputField sequenceName;
    private string sequenceString = "";

    public delegate void OnCustomSequenceCompleteEvent(string sequence);
    public static event OnCustomSequenceCompleteEvent OnCustomSequenceComplete;

    public delegate void OnSequenceSaveEvent(string name, string sequence);
    public static event OnSequenceSaveEvent OnSequenceSave;

    
    // Start is called before the first frame update
    void Start()
    {
    }

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
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(10000);
        yield return new WaitForSeconds(1.5f);
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
}
