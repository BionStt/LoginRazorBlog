using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LoginRazorBlog.Utils
{
    public static class Helper
    {
        public static string HashPassword(string plainMessage)
        {
            if (string.IsNullOrWhiteSpace(plainMessage)) return string.Empty;

            var data = Encoding.UTF8.GetBytes(plainMessage);
            using HashAlgorithm sha = new SHA256Managed();
            sha.TransformFinalBlock(data, 0, data.Length);
            return Convert.ToBase64String(sha.Hash ?? throw new InvalidOperationException());

        }
    }
}
