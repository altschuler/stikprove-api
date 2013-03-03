﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Stikprove.Api.Security
{
	public class BasicAuthCredentials
    {
        public string Name { get; set; }
        public string Password { get; set; }

        public static BasicAuthCredentials Parse(string base64Token)
        {
            var encoding = Encoding.GetEncoding("UTF-8");
            var credentials = encoding.GetString(Convert.FromBase64String(base64Token));

            var separator = credentials.IndexOf(':');
            var name = credentials.Substring(0, separator);
            var password = credentials.Substring(separator + 1);

            return new BasicAuthCredentials()
            {
                Name = name,
                Password = password
            };
        }
	}
}