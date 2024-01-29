using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserInterface : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuScreen;
    [SerializeField] private GameObject gameScreen;
    [SerializeField] private SequenceGameScreen sequenceGameScreen;

    [SerializeField] private Menu menu;

    [SerializeField] private RunningGameScreen gameRunningScreen;

    [SerializeField] private CustomSequenceScreen customSequenceScreen;
    [SerializeField] private GameObject settingsScreen;
    [SerializeField] private GameObject helpScreen;
    private Screen currentScreen;
    private Screen previousScreen;
    private Stack<Screen> screenStack = new Stack<Screen>();


    public delegate void ChangeScreenEvent(string screen);
    public static event ChangeScreenEvent OnChangeScreen;


    // Start is called before the first frame update
    void Start()
    {
        currentScreen = Screen.MainMenu;
        previousScreen = Screen.MainMenu;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ChangeScreen(Screen screen) 
    {
        screenStack.Push(currentScreen);
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
                OnChangeScreen?.Invoke("Mode");
                break;
            case Screen.Sequence:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                sequenceGameScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                OnChangeScreen?.Invoke("Sequence");
                break;
            case Screen.CustomSequence:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                customSequenceScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                OnChangeScreen?.Invoke("Custom Sequence");
                break;
            case Screen.Running:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                helpScreen.SetActive(false);
                gameRunningScreen.SetActive(true);
                sequenceGameScreen.SetActive(false);
                OnChangeScreen?.Invoke("Running");
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
        if (screenStack.Count > 0) 
        {
            Screen previous = screenStack.Pop();
            ChangeScreen(previous);
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

    public void DisplayCustomSequenceScreen() 
    {
        ChangeScreen(Screen.CustomSequence);
    }

    public void OpenMenu() 
    {
        menu.OpenMenu();
    }

    public void CloseMenu()
    {
        menu.CloseMenu();
    }

    public void QuitGame() 
    {
        Application.Quit();
    }

    public void PlaySequence(string sequence) 
    {
        ChangeScreen(Screen.Sequence);
        StartCoroutine(sequenceGameScreen.StartCountdown());
        StartCoroutine(sequenceGameScreen.PlaySequence(sequence));
    }

    public void UpdateGameState(int progress, bool correctInput) 
    {
        Debug.Log("Updating game state");
        gameRunningScreen.UpdateGameState(progress, correctInput);
    }

    public void HandleSequenceInput(string input) 
    {
        customSequenceScreen.HandleSequenceInput(input);
    }
    
}
