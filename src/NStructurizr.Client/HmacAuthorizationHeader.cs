using System;
using System.Text;

namespace NStructurizr.Client
{
    public class HmacAuthorizationHeader
    {
        public string ApiKey { get; private set; }
        public string Hmac { get; private set; }

        public HmacAuthorizationHeader(String apiKey, String hmac)
        {
            ApiKey = apiKey;
            Hmac = hmac;
        }

        public string Format()
        {
            return ApiKey + ":" + Convert.ToBase64String(Encoding.UTF8.GetBytes(Hmac));
        }

        public static HmacAuthorizationHeader Parse(string s)
        {
            var strings = s.Split(':');
            string apiKey = strings[0];
            string hmac = Encoding.UTF8.GetString(Convert.FromBase64String(strings[1]));

            return new HmacAuthorizationHeader(apiKey, hmac);
        }
    }
}