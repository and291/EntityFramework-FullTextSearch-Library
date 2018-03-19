using System;

namespace fts_lib
{
    public static class ValueWrapper
    {
        public static string Wrap(string prefix, string value)
        {
            if (string.IsNullOrEmpty(prefix)) throw new ArgumentException(nameof(prefix));
            if (string.IsNullOrEmpty(value)) throw new ArgumentException(nameof(value));

            return $"\"{prefix}{value}\"";
        }

        public static string Unwrap(string prefix, string wrappedValue)
        {
            if (string.IsNullOrEmpty(prefix)) throw new ArgumentException(nameof(prefix));
            if (string.IsNullOrEmpty(wrappedValue)) throw new ArgumentException(nameof(wrappedValue));

            return wrappedValue.Substring(prefix.Length + 1, wrappedValue.Length - prefix.Length - 2);
        }
    }
}