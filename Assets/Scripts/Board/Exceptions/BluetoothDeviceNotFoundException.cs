using System;

public class BluetoothDeviceNotFoundException : Exception
{
        public BluetoothDeviceNotFoundException()
        {
        }

        public BluetoothDeviceNotFoundException(string message)
                : base(message)
        {
        }

        public BluetoothDeviceNotFoundException(string message, Exception inner)
                : base(message, inner)
        {
        }
}