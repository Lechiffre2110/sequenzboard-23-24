using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private SequenceGameScreen sequenceGameScreen;

    [SerializeField] private RunningGameScreen gameRunningScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject helpScreen;
    private Screen currentScreen;
    private Screen previousScreen;


    // Start is called before the first frame update
    void Start()
    {
        currentScreen = Screen.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScreen(Screen screen) 
    {
        previousScreen = currentScreen;
        currentScreen = screen;
        switch (screen) 
        {
            case Screen.MainMenu:
                mainMenuScreen.SetActive(true);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                break;
            case Screen.Game:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(true);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                break;
            case Screen.Sequence:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                sequenceGameScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                break;
            case Screen.Running:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                gameRunningScreen.SetActive(true);
                sequenceGameScreen.SetActive(false);
                break;
            case Screen.Settings:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(true);
                helpScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                break;
            case Screen.Help:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                break;
        }
    }
    public void NavigateBack() 
    {
        if (previousScreen != null) 
        {
            ChangeScreen(previousScreen);
        }
    }

    public void DisplayMainMenuScreen() 
    {
        ChangeScreen(Screen.MainMenu);
    }

    public void DisplayGameScreen() 
    {
        ChangeScreen(Screen.Game);
    }

    public void DisplayRunningGameScreen() 
    {
        ChangeScreen(Screen.Running);
    }

    public void DisplaySettingsScreen() 
    {
        ChangeScreen(Screen.Settings);
    }

    public void DisplayHelpScreen() 
    {
        ChangeScreen(Screen.Help);
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void PlaySequence(string sequence) 
    {
        ChangeScreen(Screen.Sequence);
        StartCoroutine(sequenceGameScreen.PlaySequence(sequence));
    }

    public void UpdateGameState() 
    {
        //gameRunningScreen.UpdateGameState();
    }
    
}
