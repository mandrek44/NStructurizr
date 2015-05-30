using System;
using System.Text;

namespace NStructurizr.Core.Client
{
    public class HmacAuthorizationHeader
    {

        private String apiKey;
        private String hmac;

        public HmacAuthorizationHeader(String apiKey, String hmac)
        {
            this.apiKey = apiKey;
            this.hmac = hmac;
        }

        public String getApiKey()
        {
            return apiKey;
        }

        public String getHmac()
        {
            return hmac;
        }

        public String format()
        {
            return apiKey + ":" + Convert.ToBase64String(Encoding.UTF8.GetBytes(hmac));
        }

        public static HmacAuthorizationHeader parse(String s)
        {
            String apiKey = s.Split(':')[0];
            String hmac = Encoding.UTF8.GetString(Convert.FromBase64String(s.Split(':')[1]));

            return new HmacAuthorizationHeader(apiKey, hmac);
        }

    }
}