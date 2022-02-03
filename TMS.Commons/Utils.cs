using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TMS.Commons
{
    public static class Utils
    {
        public static bool IsAdmin(string Header)
        {
            var userName = GetUserName(Header);
            return userName?.ToLower() == Constants.ADMIN;
        }

        public static string GetUserName(string header)
        {
            if (string.IsNullOrEmpty(header))
            {
                return null;
            }

            var token = new JwtSecurityTokenHandler().ReadJwtToken(header.Substring(7));
            return token.Claims.First(x => x.Type == Constants.NAME).Value;
        }
    }
}
