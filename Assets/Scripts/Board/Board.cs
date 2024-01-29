using UnityEngine;
public class Board : MonoBehaviour
{
    private BluetoothConnector _bluetoothConnector;
    public delegate void BoardMessageReceivedEventHandler(string message);
    public static event BoardMessageReceivedEventHandler OnBoardMessageReceived;

    private float readMessageTimer = 0.0f;
    private float readMessageInterval = 0.05f;


    void Start()
    {
        _bluetoothConnector = new BluetoothConnector();
        ConnectToBoard();
    }

    void Update()
    {
        // Update the timer
        readMessageTimer += Time.deltaTime;

        // Check if it's time to read a message
        if (readMessageTimer >= readMessageInterval)
        {
            try 
            {
                ReadMessageFromBoard();
            }
            catch (System.TimeoutException ex)
            {
                Debug.Log(ex.Message);
            }
            readMessageTimer = 0.0f; // Reset the timer
        }
    }

    void OnApplicationQuit()
    {
        DisconnectFromBoard();
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

    public void DisconnectFromBoard()
    {
        _bluetoothConnector.StopBluetoothConnection();
    }
}
