using System;
using NgTemplate.API.DTOs.Enums;

namespace NgTemplate.API.Exceptions
{
    public class NgResponseException : Exception
    {
        public ExceptionType ExceptionType { get; set; }
        public NgResponseException(ExceptionType exceptionType = ExceptionType.InternalErrorException)
                    : base()
        {
            ExceptionType = exceptionType;
        }

        public NgResponseException(string message, ExceptionType exceptionType = ExceptionType.InternalErrorException)
                    : base(message)
        {
            ExceptionType = exceptionType;
        }

        public NgResponseException(string message, Exception innerException, ExceptionType exceptionType = ExceptionType.InternalErrorException)
                    : base(message, innerException)
        {
            ExceptionType = exceptionType;
        }
    }
}