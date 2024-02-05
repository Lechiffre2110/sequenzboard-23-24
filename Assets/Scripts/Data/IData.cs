public interface IData 
{
    /// <summary>
    /// Retrieves all sequences from the database.
    /// </summary>
    /// <returns>A list of SequenceModel objects representing all sequences.</returns>
    List<SequenceModel> GetAllSequences();

    /// <summary>
    /// Retrieves all sequence names from the database.
    /// </summary>
    /// <returns>A List of strings with the names of the sequences.</returns>    
    List<string> GetSequenceNames();
    
    /// <summary>
    /// Retrieves a sequence from the database by its name.
    /// </summary>
    /// <param name="name">The name of the sequence to retrieve.</param>
    /// <returns>A string representing the sequence.</returns>
    string LoadSequence(string name);

    /// <summary>
    /// Saves a sequence to the database.
    /// </summary>
    /// <param name="name">The name of the sequence to save.</param>
    /// <param name="sequence">The sequence to save.</param>
    void SaveSequence(string name, string sequence);
}