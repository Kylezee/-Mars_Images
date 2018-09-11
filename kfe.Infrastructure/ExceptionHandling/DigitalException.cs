using System;

namespace kfe.Infrastructure.ExceptionHandling
{
    public class DigitalException : Exception
    {
        public DigitalMessageCode DigitalMessageCode { get; set; }

        public DigitalException(DigitalMessageCode code)
        {
            DigitalMessageCode = code;
        }

        public DigitalException(DigitalMessageCode code, string message)
            : base(message)
        {
            DigitalMessageCode = code;
        }

        public DigitalException(DigitalMessageCode code, string message, Exception exception)
            : base(message, exception)
        {
            DigitalMessageCode = code;
        }

    }
}
