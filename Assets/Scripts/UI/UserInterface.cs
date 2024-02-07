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

    // Add the main menu screen to the screen history on start
    void Start()
    {
        screenHistory.Add(Screen.MainMenu);
    }

    /// <summary>
    /// Change the screen to the specified screen
    /// </summary>
    /// <param name="screen">The screen to change to</param>
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

    /// <summary>
    /// Navigate back to the previous screen from the screen history
    /// </summary>
    public void NavigateBack() 
    {
        if (screenHistory.Count > 1) 
        {
            screenHistory.RemoveAt(screenHistory.Count - 1);
            Screen previousScreen = screenHistory[screenHistory.Count - 1];
            ChangeScreen(previousScreen);
        }
    }

    /// <summary>
    /// Display the main menu screen
    /// </summary>
    public void DisplayMainMenuScreen() 
    {
        ChangeScreen(Screen.MainMenu);
    }

    /// <summary>
    /// Display the game selection screen
    /// </summary>
    public void DisplayGameScreen() 
    {
        ChangeScreen(Screen.Game);
    }

    /// <summary>
    /// Display the game running screen
    /// </summary>
    public void DisplayRunningGameScreen() 
    {
        ChangeScreen(Screen.Running);
    }

    /// <summary>
    /// Display the settings screen
    /// </summary>
    public void DisplaySettingsScreen() 
    {
        ChangeScreen(Screen.Settings);
    }

    /// <summary>
    /// Display the custom sequence selection screen
    /// </summary>
    public void DisplayCustomSequenceScreen() 
    {
        ChangeScreen(Screen.CustomSequence);
    }

    /// <summary>
    /// Display the training screen
    /// </summary>
    public void DisplayTrainingScreen() 
    {
        ChangeScreen(Screen.Training);
    }

    /// <summary>
    /// Open the menu
    /// </summary>
    public void OpenMenu() 
    {
        menu.OpenMenu();
    }

    /// <summary>
    /// Close the menu
    /// </summary>
    public void CloseMenu()
    {
        menu.CloseMenu();
    }

    /// <summary>
    /// Quit the gamee
    /// </summary>
    public void QuitGame() 
    {
        Application.Quit();
    }

    /// <summary>
    /// Play a sequence on the sequence game screen
    /// </summary>
    /// <param name="sequence">The sequence to play</param>
    public void PlaySequence(string sequence) 
    {
        ChangeScreen(Screen.Sequence);
        StartCoroutine(sequenceGameScreen.PlaySequence(sequence));
    }

    /// <summary>
    /// Update the game state on the game running screen based on the user input
    /// </summary>
    /// <param name="progress">The current progress</param>
    /// <param name="correctInput">True if the input was correct, false otherwise</param>
    public void UpdateGameState(int progress, bool correctInput) 
    {
        Debug.Log("Updating game state");
        gameRunningScreen.UpdateGameState(progress, correctInput);
    }

    /// <summary>
    /// Show a hold on the game running screen
    /// </summary>
    /// <param name="holdName">The name of the hold to show</param>
    public void ShowHold(string holdName) 
    {
        gameRunningScreen.ShowHold(holdName);
    }

    /// <summary>
    /// Handle the input of a sequence on the custom sequence screen
    /// </summary>
    /// <param name="input">The input to handle</param>
    public void HandleSequenceInput(string input) 
    {
        customSequenceScreen.HandleSequenceInput(input);
    }

    /// <summary>
    /// Handle the completion of a sequence on the game running screen
    /// </summary>
    public void HandleGameWon() 
    {
        ChangeScreen(Screen.GameWon);
    }

    /// <summary>
    /// Update the game state on the training screen based on the user input
    /// </summary>
    /// <param name="progress">The current progress</param>
    /// <param name="correctInput">True if the input was correct, false otherwise</param>
    public void UpdateTrainingGameState(int progress, bool correctInput) 
    {
        trainingScreen.UpdateGameState(progress, correctInput);
    }

    /// <summary>
    /// Play a sequence on the training screen
    /// </summary>
    /// <param name="sequence">The sequence to play</param>
    public void PlayTrainingSequence(string sequence) 
    {
        trainingScreen.PlaySequence(sequence);
    }

    /// <summary>
    /// Show a hold on the training screen after its pressed on the board
    /// </summary>
    /// <param name="holdName">The name of the hold to show</param>
    public void ShowTrainingHold(string holdName) 
    {
        Debug.Log("Showing training hold " + holdName);
        trainingScreen.ShowHold(holdName);
    }
    
}
