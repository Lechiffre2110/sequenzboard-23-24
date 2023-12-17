public class Board : IBoard 
{
    private BluetoothConnector bluetoothConnector = new BluetoothConnector();
    public void ConnectToBoard()
    {
        bluetoothConnector.StartBluetoothConnection();
    }

    public void SendMessageToBoard(string sequence)
    {
        if (bluetoothConnector.IsConnected())
        {
            bluetoothConnector.WriteData(sequence);
        }
        else
        {
            throw new BoardConnectionFailedException("Error sending message: Board is not connected");
        }
    }

    public string ReadMessageFromBoard()
    {
        if (bluetoothConnector.IsConnected())
        {
            return bluetoothConnector.ReadData();
        }
        else
        {
            throw new BoardConnectionFailedException("Error reading message: Board is not connected");
        }
    }

    public void DisconnectFromBoard()
    {
        bluetoothConnector.StopBluetoothConnection();
    }
}
