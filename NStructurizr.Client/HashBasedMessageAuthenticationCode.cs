using System;
using System.Security.Cryptography;
using System.Text;

namespace NStructurizr.Core.Client
{
    public class HashBasedMessageAuthenticationCode
    {

        private static readonly String HMAC_SHA256_ALGORITHM = "HmacSHA256";

        private String apiSecret;

        public HashBasedMessageAuthenticationCode(String apiSecret)
        {
            this.apiSecret = apiSecret;
        }

        public String generate(String content)
        {
            var hmac = new HMACSHA256(Encoding.UTF8.GetBytes(apiSecret));

            var data = hmac.ComputeHash(Encoding.UTF8.GetBytes(content));
            return BitConverter.ToString(data).Replace("-", string.Empty).ToLower();
        }

    }
}