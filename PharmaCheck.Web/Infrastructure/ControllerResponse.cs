namespace PharmaCheck.Web.Infrastructure;

public static class ControllerResponse
{
    public static dynamic ToErrorResult(string message) => new { errorMessage = message };
}