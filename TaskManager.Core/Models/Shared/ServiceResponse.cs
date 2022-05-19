namespace TaskManager.Core.Models
{
    public class ServiceResponse<T> where T : class
    {
        public ServiceResponse(bool isSuccess, string? message = null, T? data = null)
        {
            IsSuccessfull = isSuccess;
            ErrorMessage = message;
            Data = data;
        }
        public T? Data { get; }
        public bool IsSuccessfull { get; }
        public string? ErrorMessage { get; }
    }
}