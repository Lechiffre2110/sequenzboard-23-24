using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard 
{
    /// <summary>
    /// Disconnect from the board
    /// </summary>
    void DisconnectFromBoard();

    /// <summary>
    /// Send a message to the board
    /// </summary>
    /// <param name="message">The message to send</param>
    void SendMessageToBoard(string message);

    /// <summary>
    /// Read a message from the board
    /// </summary>
    /// <returns>The message read from the board</returns>
    string ReadMessageFromBoard();
}
