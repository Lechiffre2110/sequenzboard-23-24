using UnityEngine;
public class Board : IBoard 
{
    private BluetoothConnector _bluetoothConnector;

    public Board()
    {
        _bluetoothConnector = new BluetoothConnector();
    }
    public void ConnectToBoard()
    {
        _bluetoothConnector.StartBluetoothConnection();
    }

    public void SendMessageToBoard(string sequence)
    {
        try
        {
            Debug.Log("Sending message to board: " + sequence);
            _bluetoothConnector.WriteData(sequence);
        }
        catch (BoardNotConnectedException ex)
        {
            Debug.Log(ex.Message);
        }
    }

    public string ReadMessageFromBoard()
    {
        try
        {
            return _bluetoothConnector.ReadData();
        }
        catch (BoardNotConnectedException ex)
        {
            Debug.Log(ex.Message);
            return "";
        }
    }

    public void DisconnectFromBoard()
    {
        _bluetoothConnector.StopBluetoothConnection();
    }
}
