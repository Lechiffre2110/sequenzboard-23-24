using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip[] clips;

    /// <summary>
    /// Play a sound from the clips array
    /// </summary>
    /// <param name="index">index of the clip</param>
    public void PlaySound(int index)
    {
        Debug.Log("Playing sound " + index);
        if (index < 0 || index >= clips.Length)
        {
            return;
        }
        GetComponent<AudioSource>().clip = clips[index];
        GetComponent<AudioSource>().Play();
    }
}
