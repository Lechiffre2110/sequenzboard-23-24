using System;

public class BoardNotConnectedException : Exception
    {
        public BoardNotConnectedException() { }

        public BoardNotConnectedException(string message) : base(message) { }

        public BoardNotConnectedException(string message, Exception inner) : base(message, inner) { }
    }