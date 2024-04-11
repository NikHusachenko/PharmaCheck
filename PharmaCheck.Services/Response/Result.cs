using System.Net;

namespace PharmaCheck.Services.Response;

public sealed class Result
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }

    public static Result Ok(HttpStatusCode statusCode) =>
        new()
        {
            StatusCode = statusCode
        };

    public static Result Error(string errorMessage, HttpStatusCode statusCode) =>
        new()
        {
            ErrorMessage = errorMessage,
            IsError = true,
            StatusCode = statusCode
        };
}

public sealed class Result<T>
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T Value { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Result<T> Ok(T value, HttpStatusCode statusCode) =>
        new()
        {
            Value = value,
            StatusCode = statusCode,
        };

    public static Result<T> Error(string errorMessage, HttpStatusCode statusCode) =>
        new()
        {
            ErrorMessage = errorMessage,
            IsError = true,
            StatusCode = statusCode
        };
}