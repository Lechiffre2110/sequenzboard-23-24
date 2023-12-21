using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class BluetoothConnector
{
    private SerialPort _serialPort;
    private string _arduinoPort;
    private bool _connectionStatus = false;
    
    public BluetoothConnector()
    {
        _serialPort = null;
        _arduinoPort = GetArduinoPort();
        _connectionStatus = StartBluetoothConnection();
    }

    public bool GetConnectionStatus()
    {
        Debug.Log("Is connected: " + _connectionStatus);
        return _connectionStatus;
    }

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

    public string ReadData()
    {
        if (_serialPort == null || !_serialPort.IsOpen) 
        {
            throw new BoardNotConnectedException("Board is not connected");
        }
        return _serialPort.ReadLine();
    }

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

    public void StopBluetoothConnection()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            Debug.Log("Disconnected from board.");
            _serialPort.Close();
        }
    }
}