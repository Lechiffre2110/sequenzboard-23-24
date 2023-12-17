public interface IGame 
{
    void StartGame(string gameMode, IBoard board); //TODO: revert back after testing

    void StartGameFromSequence(string sequence);
    void EndGame();
    void PauseGame();
    void ResumeGame();
    void RestartGame();
    void QuitGame();
    //ONLY FOR TESTING
    string GetCurrentSequence();
} 
