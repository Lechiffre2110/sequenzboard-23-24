using System.Collections;
using UnityEngine;
using TMPro;

public class SequenceGameScreen : MonoBehaviour
{
    public GameObject[] sequenceButtons;
    public GameObject overlay;
    public TMP_Text countdownText;
    private string _currentSequence;

    void Start()
    {
        overlay.SetActive(false);
        for (int i = 0; i < sequenceButtons.Length; i++)
        {
            sequenceButtons[i].SetActive(false);
        }
    }

    public void SetActive(bool active)
    {
        if (!active)
        {
            for (int i = 0; i < sequenceButtons.Length; i++)
            {
                sequenceButtons[i].SetActive(false);
            }
            overlay.SetActive(false);
        }
        gameObject.SetActive(active);
    }

    public IEnumerator StartCountdown() 
    {
        countdownText.text = "3";
        yield return new WaitForSeconds(1);
        countdownText.text = "2";
        yield return new WaitForSeconds(1);
        countdownText.text = "1";
        yield return new WaitForSeconds(1);
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        countdownText.text = "";
        for (int i = 0; i < sequenceButtons.Length; i++)
        {
            sequenceButtons[i].SetActive(true);
        }
        overlay.SetActive(true);
        yield return new WaitForSeconds(2);
    }

    private IEnumerator ShowHold(GameObject hold)
    {
        int previousIndex = hold.transform.GetSiblingIndex();
        hold.transform.SetSiblingIndex(10000);
        yield return new WaitForSeconds(1.5f);
        hold.transform.SetSiblingIndex(previousIndex);
    }

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
                    yield return ShowHold(sequenceButtons[0]);
                    break;
                case 'B':
                    yield return ShowHold(sequenceButtons[1]);
                    break;
                case 'C':
                    yield return ShowHold(sequenceButtons[2]);
                    break;
                case 'D':
                    yield return ShowHold(sequenceButtons[3]);
                    break;
                case 'E':
                    yield return ShowHold(sequenceButtons[4]);
                    break;
                case 'F':
                    yield return ShowHold(sequenceButtons[5]);
                    break;
                case 'G':
                    yield return ShowHold(sequenceButtons[6]);
                    break;
                case 'H':
                    yield return ShowHold(sequenceButtons[7]);
                    break;
                case 'I':
                    yield return ShowHold(sequenceButtons[8]);
                    break;
                case 'J':
                    yield return ShowHold(sequenceButtons[9]);
                    break;
                case 'K':
                    yield return ShowHold(sequenceButtons[10]);
                    break;
                case 'L':
                    yield return ShowHold(sequenceButtons[11]);
                    break;
                case 'M':
                    yield return ShowHold(sequenceButtons[12]);
                    break;
                case 'N':
                    yield return ShowHold(sequenceButtons[13]);
                    break;
                case 'O':
                    yield return ShowHold(sequenceButtons[14]);
                    break;
            }

        }
        _currentSequence = filteredSequence;
    }

    public void ReplaySequence()
    {
        StartCoroutine(PlaySequence(_currentSequence));
    }
}