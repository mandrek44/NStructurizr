using System;
using System.Security.Cryptography;
using System.Text;

namespace NStructurizr.Client
{
    public class HashBasedMessageAuthenticationCode
    {
        private String apiSecret;

        public HashBasedMessageAuthenticationCode(String apiSecret)
        {
            this.apiSecret = apiSecret;
        }

        public string Generate(string content)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(this.apiSecret));
            var data = hmac.ComputeHash(Encoding.UTF8.GetBytes(content));

            return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
        }
    }
}