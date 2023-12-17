using UnityEngine;
public class Board : IBoard 
{
    private BluetoothConnector _bluetoothConnector;

    public Board()
    {
        _bluetoothConnector = new BluetoothConnector();
        ConnectToBoard();
    }
    public void ConnectToBoard()
    {
        _bluetoothConnector.StartBluetoothConnection();
    }

    public void SendMessageToBoard(string sequence)
    {
        try
        {
            bluetoothConnector.WriteData(sequence);
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
            return bluetoothConnector.ReadData();
        }
        catch (BoardNotConnectedException ex)
        {
            Debug.Log(ex.Message);
            return "";
        }
    }

    public void DisconnectFromBoard()
    {
        bluetoothConnector.StopBluetoothConnection();
    }
}
