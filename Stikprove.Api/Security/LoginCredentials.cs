using System;
using System.Text;

namespace Stikprove.Api.Security
{
	public class LoginCredentials
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public static LoginCredentials Parse(string base64Token)
        {
            var encoding = Encoding.GetEncoding("UTF-8");
            var credentials = encoding.GetString(Convert.FromBase64String(base64Token));

            var separator = credentials.IndexOf(':');
            var name = credentials.Substring(0, separator);
            var password = credentials.Substring(separator + 1);

            return new LoginCredentials()
            {
                Name = name,
                Password = password
            };
        }
	}
}