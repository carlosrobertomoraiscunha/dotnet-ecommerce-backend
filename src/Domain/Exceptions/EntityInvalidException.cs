using System;
using System.Collections.Generic;

namespace Domain.Exceptions
{
    public class EntityInvalidException : Exception
    {
        public ICollection<string> ErrorMessages { get; protected set; }

        public EntityInvalidException()
        {
        }

        public EntityInvalidException(string message) : base(message)
        {
        }

        public EntityInvalidException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public EntityInvalidException(ICollection<string> errors)
        {
            ErrorMessages = errors;
        }
    }
}
