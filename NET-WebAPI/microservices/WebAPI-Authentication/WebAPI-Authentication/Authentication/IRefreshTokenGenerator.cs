using System;
namespace WebAPI_Authentication.Authentication
{
    public interface IRefreshTokenGenerator
    {
        string GenerateToken();
    }
}
