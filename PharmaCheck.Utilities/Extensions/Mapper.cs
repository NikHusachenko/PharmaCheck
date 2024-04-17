namespace PharmaCheck.Utilities.Extensions;

public static class Mapper
{
    public static async Task<IEnumerable<TOut>> Map<T, TOut>(this Task<List<T>> asyncResult, Func<T, TOut> selector)
    {
        List<T> result = await asyncResult;
        return result.Select(selector);
    }
}