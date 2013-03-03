using System;

namespace Stikprove.Api.Security
{
    public class AuthUtils
    {
        public static string GenerateAccessToken()
        {
            return Guid.NewGuid().ToString();
        }
    }
}