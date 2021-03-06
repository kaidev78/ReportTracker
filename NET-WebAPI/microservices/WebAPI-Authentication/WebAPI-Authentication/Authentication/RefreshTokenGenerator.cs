using System;
using System.Security.Cryptography;

namespace WebAPI_Authentication.Authentication
{
    public class RefreshTokenGenerator: IRefreshTokenGenerator
    {
        public RefreshTokenGenerator()
        {
        }

        public string GenerateToken()
        {
            var randomNumber = new byte[32];
            using (var randomNumberGenerator = RandomNumberGenerator.Create()){
                randomNumberGenerator.GetBytes(randomNumber);
                return Convert.ToBase64String(randomNumber);
            }
        }
    }
}
