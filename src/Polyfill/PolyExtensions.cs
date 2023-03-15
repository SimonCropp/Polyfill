namespace Polyfill;

public static class PolyExtensions
{
#if NET461 || NET462 || NET47 || NET471 || NET472 || NET48 || NET481 || NETSTANDARD2_0
    public static bool StartsWith(this string value, char ch)
    {
        if (value.Length == 0)
        {
            return false;
        }

        return value[0] == ch;
    }

    public static bool EndsWith(this string value, char ch)
    {
        var lastPos = value.Length - 1;
        if (lastPos >= value.Length)
        {
            return false;
        }

        return value[lastPos] == ch;
    }
#endif
}