using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBoard 
{
    void ConnectToBoard();
    void DisconnectFromBoard();
    void SendMessageToBoard(string message);
    string ReadMessageFromBoard();
}
