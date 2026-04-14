using System.Net;

namespace Crud_App_dotNetApplication.Wrappers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }
        public List<string>? Errors { get; set; }

        public HttpStatusCode StatusCode { get; set; }
        public ApiResponse(bool success, string message, T? data = default, List<string>? errors = null)
        {
            Success = success;
            Message = message;
            Data = data;
            Errors = errors;
        }

        public ApiResponse()
        {
        }

        // Helper methods
        public static ApiResponse<T> SuccessResponse(T data, string message = "Request successful")
        {
            return new ApiResponse<T>(true, message, data);
        }

        public static ApiResponse<T> FailureResponse(List<string> errors, string message = "Request failed")
        {
            return new ApiResponse<T>(false, message, default, errors);
        }
    }
}
