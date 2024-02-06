using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class BluetoothConnector
{
    private SerialPort _serialPort;
    private string _arduinoPort;
    private bool _connectionStatus = false;
    
    /// <summary>
    /// Constructor for the BluetoothConnector class.
    /// </summary>
    public BluetoothConnector()
    {
        _serialPort = null;
        _arduinoPort = GetArduinoPort();
        _connectionStatus = StartBluetoothConnection();
    }

    /// <summary>
    /// Gets the connection status.
    /// </summary>
    /// <returns>True if the board is connected, false otherwise.</returns>
    public bool GetConnectionStatus()
    {
        Debug.Log("Is connected: " + _connectionStatus);
        return _connectionStatus;
    }

    /// <summary>
    /// Gets the Arduino port.
    /// </summary>
    /// <returns>The Arduino port.</returns>
    public string GetArduinoPort()
    {
        string arduinoPort = null;
        string[] availablePorts = SerialPort.GetPortNames();
        for (int i = 0; i < availablePorts.Length; i++)
        {
            if (availablePorts[i].Contains("HC-05"))
            {
                Debug.Log("Arduino port: " + availablePorts[i]);
                arduinoPort = availablePorts[i];
                break;
            }
        }
        if (arduinoPort == null)
        {
            throw new BluetoothDeviceNotFoundException("Bluetooth device not found");
        }
        return arduinoPort;
    }

    /// <summary>
    /// Establishes the Bluetooth connection to the board.
    /// </summary>
    /// <returns>True if the connection was successful, false otherwise.</returns>
    public bool StartBluetoothConnection()
    {
        _serialPort = new SerialPort(_arduinoPort, 9600);
        _serialPort.Open();
        _serialPort.ReadTimeout = 100;
        _serialPort.WriteTimeout = 100;
        if (_serialPort.IsOpen)
        {
            Debug.Log("Connected to board. Port name: " + _arduinoPort + "");
            return true;            
        }
        else
        {
            throw new BoardConnectionFailedException("Failed to connect to board");            
        }
    }

    /// <summary>
    /// Reads data from the board via the serial port.
    /// </summary>
    /// <returns>The data read from the board.</returns>
    public string ReadData()
    {
        if (_serialPort == null || !_serialPort.IsOpen) 
        {
            throw new BoardNotConnectedException("Board is not connected");
        }
        return _serialPort.ReadLine();
    }

    /// <summary>
    /// Writes data to the board via the serial port.
    /// </summary>
    /// <param name="data">The data to write to the board.</param>
    public void WriteData(string data)
    {
        if (_serialPort == null)
        {
            throw new BoardNotConnectedException("Board is not connected");
        }
        if (!_serialPort.IsOpen)
        {
            Debug.Log("Reconnecting to board...");
            _serialPort.Open();
        }
        _serialPort.WriteLine(data);
        Debug.Log("Sent data to board: " + data);
    }

    /// <summary>
    /// Stops the Bluetooth connection to the board.
    /// </summary>
    public void StopBluetoothConnection()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            Debug.Log("Disconnected from board.");
            _serialPort.Close();
        }
    }

    /// <summary>
    /// Gets the serial port.
    /// </summary>
    /// <returns>The serial port.</returns>
    public SerialPort GetSerialPort()
    {
        return _serialPort;
    }
}