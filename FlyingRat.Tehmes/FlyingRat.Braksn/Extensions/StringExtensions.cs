using FlyingRat.Braksn;
using System;

public static class StringExtensions
{
    public static string Slice(this string str, int? count)
    {
        if (string.IsNullOrWhiteSpace(str)) return str;
        if (count.HasValue == false) return str;
        var sliceCount = Math.Min(str.Length, count.Value);
        string dot = str.Length - sliceCount < 0 ? ConstString.Default_Ellipsis : default(string);
        return $"{str[0..sliceCount]}{dot}";
    }
}