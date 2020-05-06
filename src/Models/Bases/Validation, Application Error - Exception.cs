namespace MovieWeb.WebApi.Model
{
    using System;
    using System.Globalization;

    public class ValidationException : Exception
    {
        public ValidationException(string message)
            : base(message)
        { }

        public int ErrorCode { get; }
    }

    public class ApplicationErrorException : Exception
    {
        public ApplicationErrorException() 
            : base() { }

        public ApplicationErrorException(string message) 
            : base(message) { }

        public ApplicationErrorException(string message, params object[] args)
            : base(string.Format(CultureInfo.CurrentCulture, message, args))
        { }
    }
}
