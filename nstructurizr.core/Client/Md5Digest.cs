using System;
using System.Security.Cryptography;
using System.Text;

namespace NStructurizr.Core.Client
{
    public class Md5Digest {

        private static readonly String ALGORITHM = "MD5";

        public String generate(String content) {
            if (content == null) {
                content = "";
            }

            return GetMd5Hash(content);
        }

        static string GetMd5Hash(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                // Convert the input string to a byte array and compute the hash. 
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
            }
        }

    }
}