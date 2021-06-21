using System;
using System.Security.Claims;
using WebAPI_Authentication.Models;

namespace WebAPI_Authentication.Authentication
{
    public interface IJwtAuthenticationManager
    {
        AuthenticationResponse Authenticate(string username, string password, string redisUrl);
        AuthenticationResponse Authenticate(string username, Claim[] claims);

    }
}
