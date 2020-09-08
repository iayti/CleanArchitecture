namespace Application.Common.Models
{
    /// <summary>
    /// A standard response for service calls.
    /// </summary>
    /// <typeparam name="T">Return data type</typeparam>
    public class ServiceResult<T> : ServiceResult
    {
        public T Data { get; set; }

        public ServiceResult(T data)
        {
            this.Data = data;
        }

        public ServiceResult(T data, ServiceError error) : base(error)
        {
            this.Data = data;
        }

        public ServiceResult(ServiceError error) : base(error)
        {

        }
    }

    public class ServiceResult
    {
        public bool Succeeded => this.Error == null;

        public ServiceError Error { get; set; }

        public ServiceResult(ServiceError error)
        {
            if (error == null)
            {
                error = ServiceError.DefaultError;
            }

            this.Error = error;
        }

        public ServiceResult() { }

        #region Helper Methods

        public static ServiceResult Failed(ServiceError error)
        {
            return new ServiceResult(error);
        }

        public static ServiceResult<T> Failed<T>(ServiceError error)
        {
            return new ServiceResult<T>(error);
        }

        public static ServiceResult<T> Failed<T>(T data, ServiceError error)
        {
            return new ServiceResult<T>(data, error);
        }

        public static ServiceResult Success()
        {
            return new ServiceResult();
        }

        public static ServiceResult<T> Success<T>(T data)
        {
            return new ServiceResult<T>(data);
        }

        #endregion
    }
}
