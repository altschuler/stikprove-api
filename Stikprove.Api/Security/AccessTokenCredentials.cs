using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Stikprove.Api.Security
{
    public class AccessTokenCredentials
    {
        public int Id { get; set; }
        public string TokenValue { get; set; }

        public static AccessTokenCredentials Parse(string base64Token)
        {
            var encoding = Encoding.GetEncoding("UTF-8");
            var credentials = encoding.GetString(Convert.FromBase64String(base64Token));

            var separator = credentials.IndexOf(':');
            var id = credentials.Substring(0, separator);
            var password = credentials.Substring(separator + 1);

            return new AccessTokenCredentials()
            {
                Id = int.Parse(id),
                TokenValue = password
            };
        }

        public static bool TryParse(string base64Token)
        {
            try
            {
                Parse(base64Token);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}