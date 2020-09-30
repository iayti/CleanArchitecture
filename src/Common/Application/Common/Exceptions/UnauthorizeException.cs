namespace Application.Common.Exceptions
{
    using System;

    public class UnauthorizeException : Exception
    {
        public UnauthorizeException() : base("User was not found!")
        {
            
        }
    }
}
