using System;

namespace b2.Domain.Core
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}