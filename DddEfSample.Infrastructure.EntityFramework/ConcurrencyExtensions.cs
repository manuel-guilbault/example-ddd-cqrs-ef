using System;

namespace DddEfSample.Infrastructure.EntityFramework
{
    internal static class ConcurrencyExtensions
    {
        public static string ToETag(this byte[] rowVersion)
        {
            return BitConverter.ToString(rowVersion).Replace("-", "");
        }
    }
}
