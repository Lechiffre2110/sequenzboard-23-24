public class Board : IBoard 
{
    private BluetoothConnector bluetoothConnector = new BluetoothConnector();
    public void ConnectToBoard()
    {
        bluetoothConnector.StartBluetoothConnection();
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
