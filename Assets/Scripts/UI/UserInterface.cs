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
    [SerializeField] private TrainingScreen trainingScreen;
    [SerializeField] private CustomSequenceScreen customSequenceScreen;
    [SerializeField] private GameObject settingsScreen;

    [SerializeField] private GameWonScreen gameWonScreen;

    private List<Screen> screenHistory = new List<Screen>();


    public delegate void ChangeScreenEvent(string screen);
    public static event ChangeScreenEvent OnChangeScreen;


    // Start is called before the first frame update
    void Start()
    {
        screenHistory.Add(Screen.MainMenu);
    }

    public void ChangeScreen(Screen screen) 
    {
        if (screenHistory[screenHistory.Count - 1] != screen) {
            screenHistory.Add(screen);
        }

        switch (screen) 
        {
            case Screen.MainMenu:
                mainMenuScreen.SetActive(true);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(false);
                break;
            case Screen.Game:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(true);
                settingsScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(false);
                OnChangeScreen?.Invoke("Mode");
                break;
            case Screen.Sequence:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                sequenceGameScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(false);
                OnChangeScreen?.Invoke("Sequence");
                break;
            case Screen.Training:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(true);
                OnChangeScreen?.Invoke("Training");
                break;
            case Screen.CustomSequence:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                customSequenceScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(false);
                OnChangeScreen?.Invoke("Custom Sequence");
                break;
            case Screen.Running:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                gameRunningScreen.SetActive(true);
                sequenceGameScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(false);
                OnChangeScreen?.Invoke("Running");
                break;
            case Screen.Settings:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(true);
                gameRunningScreen.SetActive(false);
                gameWonScreen.SetActive(false);
                trainingScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                break;
            case Screen.GameWon:
                mainMenuScreen.SetActive(false);
                gameScreen.SetActive(false);
                settingsScreen.SetActive(false);
                gameRunningScreen.SetActive(false);
                sequenceGameScreen.SetActive(false);
                trainingScreen.SetActive(false);
                gameWonScreen.SetActive(true);
                break;
        }
    }
    public void NavigateBack() 
    {
        if (screenHistory.Count > 1) 
        {
            screenHistory.RemoveAt(screenHistory.Count - 1);
            Screen previousScreen = screenHistory[screenHistory.Count - 1];
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

    public void DisplayCustomSequenceScreen() 
    {
        ChangeScreen(Screen.CustomSequence);
    }

    public void DisplayTrainingScreen() 
    {
        ChangeScreen(Screen.Training);
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
        StartCoroutine(sequenceGameScreen.PlaySequence(sequence));
    }

    public void UpdateGameState(int progress, bool correctInput) 
    {
        Debug.Log("Updating game state");
        gameRunningScreen.UpdateGameState(progress, correctInput);
    }

    public void ShowHold(string holdName) 
    {
        gameRunningScreen.ShowHold(holdName);
    }

    public void HandleSequenceInput(string input) 
    {
        customSequenceScreen.HandleSequenceInput(input);
    }

    public void HandleGameWon() 
    {
        ChangeScreen(Screen.GameWon);
    }

    public void UpdateTrainingGameState(int progress, bool correctInput) 
    {
        trainingScreen.UpdateGameState(progress, correctInput);
    }

    public void PlayTrainingSequence(string sequence) 
    {
        trainingScreen.PlaySequence(sequence);
    }

    public void ShowTrainingHold(string holdName) 
    {
        Debug.Log("Showing training hold " + holdName);
        trainingScreen.ShowHold(holdName);
    }
    
}
