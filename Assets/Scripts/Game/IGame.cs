public interface IGame 
{
    /// <summary>
    /// Start a new game
    /// </summary>
    /// <param name="gameMode">The game mode to start</param>
    /// <param name="sequence">The sequence to start the game with</param>
    void StartGame(string gameMode, string sequence = "");

    /// <summary>
    /// Start a new training game
    /// </summary>
    void StartTrainingGame();

    /// <summary>
    /// Update the game state
    /// </summary>
    /// <param name="input">The input to update the game state with</param>
    /// <returns>True if the input is valid, false otherwise</returns>
    void UpdateTrainingGameState(string input);

    /// <summary>
    /// Get the current sequence
    /// </summary>
    /// <returns>The current sequence</returns>
    string GetCurrentSequence();
    

    /// <summary>
    /// Update the game state
    /// </summary>
    /// <param name="input">The input to update the game state with</param>
    void UpdateGameState(string input);
}
