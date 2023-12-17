using System.Collections;
using System.IO.Ports;
using UnityEngine;

public class BluetoothConnector
{
    private SerialPort _serialPort;
    public BluetoothConnector()
    {
        _serialPort = null;
    }

    public string GetArduinoPort()
    {
        string arduinoPort = null;
        string[] availablePorts = SerialPort.GetPortNames();
        for (int i = 0; i < availablePorts.Length; i++)
        {
            if (availablePorts[i].Contains("HC-05"))
            {
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

    public string StartBluetoothConnection()
    {
        string portName = GetArduinoPort();
        using (_serialPort = new SerialPort(portName, 9600))
        {
            _serialPort.Open();
            _serialPort.ReadTimeout = 100;
            _serialPort.WriteTimeout = 100;

            if (_serialPort.IsOpen)
            {
                return "Connected";
            }
            else
            {
                throw new BoardConnectionFailedException("Failed to connect to board");
            }
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
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.WriteLine(data);
        } else
        {
            throw new BoardNotConnectedException("Board is not connected");
        }
    }

    public void StopBluetoothConnection()
    {
        if (_serialPort != null && _serialPort.IsOpen)
        {
            _serialPort.Close();
        }
    }

    public bool IsConnected()
    {
        Debug.Log("Serialport" + _serialPort);
        Debug.Log("Serialport open" + _serialPort.IsOpen);
        return _serialPort != null && _serialPort.IsOpen;
    }
}