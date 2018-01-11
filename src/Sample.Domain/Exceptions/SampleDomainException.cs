using System;

namespace Sample.Domain.Exceptions
{
    /// <summary>
    /// Exception type for domain exceptions
    /// </summary>
    public class SampleDomainException : Exception
    {
        public SampleDomainException()
        { }

        public SampleDomainException(string message) : base(message)
        { }

        public SampleDomainException(string message, Exception innerException) : base(message, innerException)
        { }
    }
}