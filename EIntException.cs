using System;

namespace External
{
    public class EIntException : Exception
    {
        public override string Message { get; }
        public EIntException(string message)
        {
            Message = message;
        }
    }
}