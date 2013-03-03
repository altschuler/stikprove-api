using System;
using System.Text;
using System.Text.RegularExpressions;

namespace Stikprove.Api.Security
{
    public class AccessTokenCredentials
    {
        public string Name { get; set; }
        public string TokenValue { get; set; }

        public static AccessTokenCredentials Parse(string base64Token)
        {
            var encoding = Encoding.GetEncoding("UTF-8");
            var credentials = encoding.GetString(Convert.FromBase64String(base64Token));

            var separator = credentials.IndexOf(':');
            var name = credentials.Substring(0, separator);
            var password = credentials.Substring(separator + 1);

            return new AccessTokenCredentials()
            {
                Name = name,
                TokenValue = password
            };
        }

        public static bool TryParse(string base64Token)
        {
            try
            {
                var encoding = Encoding.GetEncoding("UTF-8");
                var decoded = encoding.GetString(Convert.FromBase64String(base64Token));

                var regex = new Regex(".*:.*");
                return regex.IsMatch(decoded);
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}