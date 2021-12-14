namespace BA.Core.Extensions;

public static class EnumerableExtension
{
    public static bool IsEmpty<T>(this IEnumerable<T> enumeration)
    {
        if (enumeration == null) return true;

        if (!enumeration.Any()) return true;
        else return false;
    }
}