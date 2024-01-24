using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    public delegate void OnResumeGameEventHandler();
    public static event OnResumeGameEventHandler OnResumeGame;

    public delegate void OnRestartGameEventHandler();
    public static event OnRestartGameEventHandler OnRestartGame;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CloseMenu()
    {
        gameObject.SetActive(false);
    }

    public void OpenMenu()
    {
        gameObject.SetActive(true);
    }
}
