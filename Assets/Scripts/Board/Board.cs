using UnityEngine;
public class Board : MonoBehaviour
{
    private BluetoothConnector _bluetoothConnector;
    public delegate void BoardMessageReceivedEventHandler(string message);
    public static event BoardMessageReceivedEventHandler OnBoardMessageReceived;

    private float _readMessageTimer = 0.0f;
    private float _readMessageInterval = 0.05f;

    // Create a new BluetoothConnector and connect to the board
    void Start()
    {
        _bluetoothConnector = new BluetoothConnector();
        ConnectToBoard();
    }

    // Call ReadMessageFromBoard every _readMessageInterval seconds
    void Update()
    {
        _readMessageTimer += Time.deltaTime;

        if (_readMessageTimer >= _readMessageInterval)
        {
            try 
            {
                ReadMessageFromBoard();
            }
            catch (System.TimeoutException ex)
            {   
            }
            _readMessageTimer = 0.0f; // Reset the timer
        }
    }

    // Disconnect from the board when the application is closed
    void OnApplicationQuit()
    {
        DisconnectFromBoard();
    }

    /// <summary>
    /// Connect to the board
    /// </summary>
    private void ConnectToBoard()
    {
        _bluetoothConnector.StartBluetoothConnection();
    }

    /// <summary>
    /// Send a message to the board
    /// </summary>
    /// <param name="sequence">The message to send</param>
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

    /// <summary>
    /// Read a message from the board
    /// </summary>
    /// <returns>The message read from the board</returns>
    public string ReadMessageFromBoard()
    {
        try
        {
            string data = _bluetoothConnector.ReadData();
            if (!string.IsNullOrEmpty(data))
            {
                OnBoardMessageReceived(data);
            }
            return data;
        }
        catch (BoardNotConnectedException ex)
        {
            Debug.Log(ex.Message);
            return "";
        }
    }

    /// <summary>
    /// Disconnect from the board
    /// </summary>
    public void DisconnectFromBoard()
    {
        _bluetoothConnector.StopBluetoothConnection();
    }
}
