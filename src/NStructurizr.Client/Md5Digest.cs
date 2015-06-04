using System;
using System.Security.Cryptography;
using System.Text;

namespace NStructurizr.Client
{
    public class Md5Digest 
    {
        public string Generate(string content) 
        {
            if (content == null)
                content = "";

            return GetMd5Hash(content);
        }

        static string GetMd5Hash(string input)
        {
            using (var md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));
                return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
            }
        }
    }
}