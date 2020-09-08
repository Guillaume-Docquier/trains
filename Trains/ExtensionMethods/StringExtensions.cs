using System;

namespace Trains.ExtensionMethods
{
    public static class StringExtensions
    {
        public static string ReplaceFirst(this string text, string oldValue, string newValue)
        {
            var index = text.IndexOf(oldValue, StringComparison.Ordinal);
            if (index < 0)
            {
                return text;
            }

            var before = text.Substring(0, index);
            var after = text.Substring(index + oldValue.Length);

            return string.Concat(before, newValue, after);
        }
    }
}