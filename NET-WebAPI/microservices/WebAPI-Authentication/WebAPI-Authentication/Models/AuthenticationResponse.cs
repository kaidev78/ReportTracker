using System;
namespace WebAPI_Authentication.Models
{
    public class AuthenticationResponse
    {
        public string JwtToken { get; set; }

        public string RefreshToken { get; set; }

    }
}
