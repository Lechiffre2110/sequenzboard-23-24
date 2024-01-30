using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSequenceDropdown : MonoBehaviour
{
    //Serializefield for Dropdown
    [SerializeField] private TMPro.TMP_Dropdown dropdown;
    private Data _data;
    private List<string> _sequenceNames = new List<string>();
    // Event for loading a sequence
    //public delegate void OnLoadSequenceEvent(string name, string sequence);
    public delegate void OnLoadSequenceEvent(string sequence);
    public static event OnLoadSequenceEvent OnLoadSequence;


    void Start()
    {
        _data = new Data();
        _sequenceNames = _data.GetSequenceNames();
        dropdown.ClearOptions();
        dropdown.AddOptions(_sequenceNames);
    }

    public void LoadSequence() {
        string sequenceName = _sequenceNames[dropdown.value];
        string sequence = _data.LoadSequence(sequenceName);
        OnLoadSequence?.Invoke(sequence);
        gameObject.SetActive(false);
    }

    public void ShowLoadSequenceDropdown() {
        gameObject.SetActive(true);
    }

}
