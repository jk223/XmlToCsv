using System;

namespace Gentrack_JagmeetPOC
{
    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
    }
}
