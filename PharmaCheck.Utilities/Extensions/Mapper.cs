namespace PharmaCheck.Utilities.Extensions;

public static class Mapper
{
    public static async Task<IEnumerable<TOut>> Map<T, TOut>(this Task<List<T>> asyncResult, Func<T, TOut> mapper)
    {
        List<T> result = await asyncResult;
        return result.Select(mapper);
    }

    public static async Task<TOut> Map<T, TOut>(this Task<T> asyncResult, Func<T, TOut> mapper)
    {
        T result = await asyncResult;
        return mapper(result);
    }
}