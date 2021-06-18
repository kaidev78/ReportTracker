using System;
namespace WebAPI_Authentication.Authentication
{
    public interface IJwtAuthenticationManager
    {
        string Authenticate(string username, string password, string redisUrl);

    }
}
