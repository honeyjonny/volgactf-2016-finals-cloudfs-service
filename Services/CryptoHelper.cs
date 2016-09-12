

using System;
using System.Security.Cryptography;
using System.Text;

namespace CloudFd.Services
{
    public static class CryptoHelper
    {
        public static string GetHashForString(this HashAlgorithm algorithm, string input)
        {
            var hash = algorithm.ComputeHash(Encoding.UTF8.GetBytes(input));

            var output = BitConverter
                .ToString(hash)
                .Replace("-", "")
                .ToLower();

            return output;
        }
    }
}