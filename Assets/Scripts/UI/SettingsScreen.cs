using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingsScreen : MonoBehaviour
{
    [SerializeField] private TMP_Text _volumeText;
    [SerializeField] private TMP_Text _lengthText;

    [SerializeField] private AudioSource _audioSource;
    private int _volume = 5;
    private int _length = 5;

    /// <summary>
    /// Increase the volume by 1
    /// </summary>
    public void IncreaseVolume()
    {
        if (_volume == 10)
        {
            return;
        }
        _volume++;
        _volumeText.text = _volume.ToString();
        _audioSource.volume = _volume / 10f;
        PlayerPrefs.SetInt("volume", _volume);

    }

    /// <summary>
    /// Decrease the volume by 1
    /// </summary>
    public void DecreaseVolume()
    {
        if (_volume == 0)
        {
            return;
        }
        _volume--;
        _volumeText.text = _volume.ToString();
        _audioSource.volume = _volume / 10f;
        PlayerPrefs.SetInt("volume", _volume);
    }

    /// <summary>
    /// Increase the default generation sequence length by 1
    /// </summary>
    public void IncreaseLength()
    {
        _length++;
        _lengthText.text = _length.ToString();
        PlayerPrefs.SetInt("length", _length);
    }

    /// <summary>
    /// Decrease the default generation sequence length by 1
    /// </summary>
    public void DecreaseLength()
    {
        if (_length == 0)
        {
            return;
        }
        _length--;
        _lengthText.text = _length.ToString();
        PlayerPrefs.SetInt("length", _length);
    }


    // Set the volume and length to the saved values
    void Start()
    {
        _volume = PlayerPrefs.GetInt("volume");
        _length = PlayerPrefs.GetInt("length");
        _volumeText.text = _volume.ToString();
        _lengthText.text = _length.ToString();
    }
}
