using System.Collections;
using UnityEngine;
using TMPro;

public class SequenceGameScreen : MonoBehaviour
{
    [SerializeField] private Audio audio;
    public GameObject[] sequenceButtons;
    public GameObject overlay;
    private string _currentSequence;

    public void SetActive(bool active)
    {
        gameObject.SetActive(active);
    }

    /// <summary>
    /// Show the passed in hold on the screen
    /// </summary>
    /// <param name="holdName">The name of the hold to show</param>
    /// <param name="index">The index of the hold</param>
    private IEnumerator ShowHold(GameObject hold, int index = 6)
    {
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(10000);
        PlaySound(index);
        yield return new WaitForSeconds(1.5f); 
        hold.transform.SetSiblingIndex(previousIndex);
    }

    /// <summary>
    /// Play a sequence of holds based on the passed in string
    /// </summary>
    /// <param name="sequence">The sequence to play</param>
    public IEnumerator PlaySequence(string sequence)
    {
        string filteredSequence = "";
        foreach (char c in sequence)
        {
            if (char.IsUpper(c))
            {
                filteredSequence += c;
            }
        }

        foreach (char c in filteredSequence)
        {
            switch (c)
            {
                case 'A':
                    yield return ShowHold(sequenceButtons[0], 0);
                    break;
                case 'B':
                    yield return ShowHold(sequenceButtons[1], 1);
                    break;
                case 'C':
                    yield return ShowHold(sequenceButtons[2], 2);
                    break;
                case 'D':
                    yield return ShowHold(sequenceButtons[3], 3);
                    break;
                case 'E':
                    yield return ShowHold(sequenceButtons[4], 4);
                    break;
                case 'F':
                    yield return ShowHold(sequenceButtons[5], 5);
                    break;
                case 'G':
                    yield return ShowHold(sequenceButtons[6], 6);
                    break;
            }

        }
        _currentSequence = filteredSequence;
    }

    /// <summary>
    /// Replay the current sequence
    /// </summary>
    public void ReplaySequence()
    {
        StartCoroutine(PlaySequence(_currentSequence));
    }
    
    /// <summary>
    /// Play hold specific sound
    /// </summary>
    /// <param name="index">The index of the hold</param>
    private void PlaySound(int index)
    {
        audio.PlaySound(index);
    }
}