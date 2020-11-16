using FlyingRat.Braksn;
using System;
using System.Text;

public static class StringExtensions
{
    public static string EllipsizeUtf8(this string str, int count, string ellipsis="...")
    {
        if (string.IsNullOrEmpty(str) || str.Length <= count) return str;
        var index = 0;
        var step = 0;
        var backCount = count;
        var spanString = str.AsSpan();
        while (index < count && index < spanString.Length)
        {
            var num = UTF8Encoding.UTF8.GetByteCount(spanString.Slice(index++, 1));
            if (num == 1) step++;
        }
        while (step > 0 && index < spanString.Length)
        {
            var num = UTF8Encoding.UTF8.GetByteCount(spanString.Slice(index++, 1));
            if (num == 1) step--;
            else step -= 2;
            if (step >= 0) count++;
        }
        return $"{spanString.Slice(0, count).ToString()}{(count > backCount ? "..." : default)}";
    }
}