public interface IGame 
{
    void StartGame(string gameMode, string sequence = "");

    void StartGameFromSequence(string sequence);

    void StartTrainingGame();
    void UpdateTrainingGameState(string input);
    void EndGame();

    void UpdateGameState(string input);
    void PauseGame();
    void ResumeGame();
    void RestartGame();
    //ONLY FOR TESTING
    string GetCurrentSequence();
} 
