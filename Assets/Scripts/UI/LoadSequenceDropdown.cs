using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSequenceDropdown : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Dropdown dropdown;
    private Data _data;
    private List<string> _sequenceNames = new List<string>();
    public delegate void OnLoadSequenceEvent(string sequence);
    public static event OnLoadSequenceEvent OnLoadSequence;

    // Load the sequence names from the data  and add them to the dropdown
    void Start()
    { 
        _data = new Data();
        _sequenceNames = _data.GetSequenceNames();
        dropdown.ClearOptions();
        dropdown.AddOptions(_sequenceNames);
    }

    /// <summary>
    /// Load the sequence from the data object and invoke the OnLoadSequence event
    /// </summary>
    public void LoadSequence() {
        string sequenceName = _sequenceNames[dropdown.value];
        string sequence = _data.LoadSequence(sequenceName);
        OnLoadSequence?.Invoke(sequence);
        gameObject.SetActive(false);
    }

    /// <summary>
    /// Show the load sequence dropdown
    /// </summary>
    public void ShowLoadSequenceDropdown() {
        gameObject.SetActive(true);
    }

}
