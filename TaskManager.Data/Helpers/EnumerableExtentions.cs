namespace TaskManager.Data.Helpers;

public static class EnumerableExtentions
{
    public static IEnumerable<T> OrEmptyIfNull<T>(this IEnumerable<T> source)
        => source ?? Enumerable.Empty<T>();
}