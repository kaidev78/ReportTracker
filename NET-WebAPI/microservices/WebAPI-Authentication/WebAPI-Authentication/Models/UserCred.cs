﻿using System;
namespace WebAPI_Authentication.Models
{
    public class UserCred
    {
        public string token { get; set; }
        public int type { get; set; }
    }

    public class RefreshCred {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }
    }
}
