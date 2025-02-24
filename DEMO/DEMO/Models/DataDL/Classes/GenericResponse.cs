namespace DEMO.Models.DTO
{
    public class ApiResponse<T>
    {
        public string Status { get; set; } = "SUCCESS"; // Default success
        public string Message { get; set; } = "Request processed successfully.";
        public T Data { get; set; }
        public ErrorDetails Error { get; set; }

        // Success response
        public static ApiResponse<T> Success(T data, string message = "Request processed successfully.")
        {
            return new ApiResponse<T>
            {
                Status = "SUCCESS",
                Message = message,
                Data = data,
                Error = null
            };
        }

        // Failure response
        public static ApiResponse<T> Fail(string errorCode, string errorMessage, string errorDetails = null)
        {
            return new ApiResponse<T>
            {
                Status = "FAIL",
                Message = "An error occurred.",
                Data = default,
                Error = new ErrorDetails
                {
                    Code = errorCode,
                    Message = errorMessage,
                    Details = errorDetails
                }
            };
        }
    }

    public class ErrorDetails
    {
        public string Code { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
