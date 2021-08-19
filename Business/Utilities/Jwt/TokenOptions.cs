using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Utilities.Jwt
{
    public class TokenOptions
    {
        public string Audience { get; set; }
        public IEnumerable<string> Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }

        public string SecurityKey { get; set; }
    }
}
