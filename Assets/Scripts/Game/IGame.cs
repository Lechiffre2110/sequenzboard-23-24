public interface IGame 
{
    void StartGame(string gameMode);

    void StartGameFromSequence(string sequence);
    void EndGame();
    void PauseGame();
    void ResumeGame();
    void RestartGame();
    void QuitGame();
    //ONLY FOR TESTING
    string GetCurrentSequence();
} 
