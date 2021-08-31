﻿using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace Business.Utilities.Jwt
{
    public class AccessToken
    {
        public string Token { get; set; }

        public DateTime Expiration { get; set; }
        public IEnumerable<string> claim { get; set; }



    }
}
