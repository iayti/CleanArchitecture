using System;

namespace CleanArchitecture.Application.Common.Models
{
    /// <summary>
    /// All errors contained in ServiceResult objects must return an error of this type
    /// Error codes allow the caller to easily identify the received error and take action.
    /// Error messages allow the caller to easily show error messages to the end user.
    /// </summary>
    [Serializable]
    public class ServiceError
    {
        /// <summary>
        /// CTOR
        /// </summary>
        public ServiceError(string message, int code)
        {
            this.Message = message;
            this.Code = code;
        }

        public ServiceError() { }

        /// <summary>
        /// Human readable error message
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Machine readable error code
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Default error for when we receive an exception
        /// </summary>
        public static ServiceError DefaultError => new ServiceError("An exception occured.", 999);

        /// <summary>
        /// Default validation error. Use this for invalid parameters in controller actions and service methods.
        /// </summary>
        public static ServiceError ModelStateError(string validationError)
        {
            return new ServiceError(validationError, 998);
        }

        /// <summary>
        /// Use this for unauthorized responses.
        /// </summary>
        public static ServiceError ForbiddenError => new ServiceError("You are not authorized to call this action.", 998);

        /// <summary>
        /// Use this to send a custom error message
        /// </summary>
        public static ServiceError CustomMessage(string errorMessage)
        {
            return new ServiceError(errorMessage, 997);
        }

        public static ServiceError UserNotFound => new ServiceError("User with this id does not exist", 996);

        public static ServiceError UserFailedToCreate => new ServiceError("Failed to create User.", 995);

        public static ServiceError Canceled => new ServiceError("The request canceled successfully!", 994);

        public static ServiceError NotFound => new ServiceError("The specified resource was not found.", 990);

        public static ServiceError ValidationFormat => new ServiceError("Request object format is not true.", 901);
        
        public static ServiceError Validation => new ServiceError("One or more validation errors occurred.", 900);

        public static ServiceError SearchAtLeastOneCharacter => new ServiceError("Search parameter must have at least one character!", 898);

        /// <summary>
        /// Default error for when we receive an exception
        /// </summary>
        public static ServiceError ServiceProviderNotFound => new ServiceError("Service Provider with this name does not exist.", 700);

        public static ServiceError ServiceProvider => new ServiceError("Service Provider failed to return as expected.", 600);

        public static ServiceError DateTimeFormatError => new ServiceError("Date format is not true. Date format must be like yyyy-MM-dd (2019-07-19)", 500);

        #region Override Equals Operator

        /// <summary>
        /// Use this to compare if two errors are equal
        /// Ref: https://msdn.microsoft.com/ru-ru/library/ms173147(v=vs.80).aspx
        /// </summary>
        public override bool Equals(object obj)
        {
            // If parameter cannot be cast to ServiceError or is null return false.
            var error = obj as ServiceError;

            // Return true if the error codes match. False if the object we're comparing to is nul
            // or if it has a different code.
            return Code == error?.Code;
        }

        public bool Equals(ServiceError error)
        {
            // Return true if the error codes match. False if the object we're comparing to is nul
            // or if it has a different code.
            return Code == error?.Code;
        }

        public override int GetHashCode()
        {
            return Code;
        }

        public static bool operator ==(ServiceError a, ServiceError b)
        {
            // If both are null, or both are same instance, return true.
            if (ReferenceEquals(a, b))
            {
                return true;
            }

            // If one is null, but not both, return false.
            if ((object)a == null || (object)b == null)
            {
                return false;
            }

            // Return true if the fields match:
            return a.Equals(b);
        }

        public static bool operator !=(ServiceError a, ServiceError b)
        {
            return !(a == b);
        }

        #endregion
    }

}
