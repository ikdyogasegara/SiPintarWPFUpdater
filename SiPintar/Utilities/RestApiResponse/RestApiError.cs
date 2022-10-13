using System.Diagnostics.CodeAnalysis;

namespace SiPintar.Utilities
{
    [ExcludeFromCodeCoverage]
    public class RestApiError
    {
        public RestApiError(string message, ResponseErrorType errorType)
        {
            Message = message;
            ErrorType = errorType;
        }

        public string Message { get; protected set; }
        public ResponseErrorType ErrorType { get; protected set; }
    }

    public enum ResponseErrorType
    {
        ApiNotFound,
        ServerError,
        UnknownError,
        Unauthorized,
        ConnectionError,
        RequestTimeOut,
        TooManyRequests
    }
}
