using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{
    public AudioClip[] clips;
    // Start is called before the first frame update
    void Start()
    {
    }

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

    // Update is called once per frame
    void Update()
    {
        
    }
}
