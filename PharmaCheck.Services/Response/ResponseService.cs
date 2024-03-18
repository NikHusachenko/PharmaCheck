namespace PharmaCheck.Services.Response;

public sealed class ResponseService
{
    public string ErrorMessage { get; private set; } = string.Empty;

    public static ResponseService Ok() => new ResponseService();

    public static ResponseService Error(string message) => new ResponseService() { ErrorMessage = message };
}

public sealed class ResponseService<T>
{
    public string ErrorMessage { get; private set; } = string.Empty;
    public T Value { get; private set; }

    public static ResponseService<T> Ok(T value) => new ResponseService<T>() { Value = value };
    public static ResponseService<T> Error(string message) => new ResponseService<T>() { ErrorMessage = message };
}