using System.Net;

namespace PharmaCheck.Services.Response;

public sealed class Result
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public HttpStatusCode StatusCode { get; set; }

    public static Result Ok(ResultSuccessStatusCode statusCode) =>
        new()
        {
            StatusCode = (HttpStatusCode)statusCode
        };

    public static Result Error(string errorMessage, ResultErrorStatusCode statusCode) =>
        new()
        {
            ErrorMessage = errorMessage,
            IsError = true,
            StatusCode = (HttpStatusCode)statusCode
        };
}

public sealed class Result<T>
{
    public bool IsError { get; set; }
    public string ErrorMessage { get; set; } = string.Empty;
    public T Value { get; set; }
    public HttpStatusCode StatusCode { get; set; }

    public static Result<T> Ok(T value, ResultSuccessStatusCode statusCode) =>
        new()
        {
            Value = value,
            StatusCode = (HttpStatusCode)statusCode,
        };

    public static Result<T> Error(string errorMessage, ResultErrorStatusCode statusCode) =>
        new()
        {
            ErrorMessage = errorMessage,
            IsError = true,
            StatusCode = (HttpStatusCode)statusCode
        };
}