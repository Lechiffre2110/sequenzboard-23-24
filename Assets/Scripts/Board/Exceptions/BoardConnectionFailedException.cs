using System;

public class BoardConnectionFailedException : Exception
{
        public BoardConnectionFailedException()
        {
        }

        public BoardConnectionFailedException(string message)
                : base(message)
        {
        }

        public BoardConnectionFailedException(string message, Exception inner)
                : base(message, inner)
        {
        }
}