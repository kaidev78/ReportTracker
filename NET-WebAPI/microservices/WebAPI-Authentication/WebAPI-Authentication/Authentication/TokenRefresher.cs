using System;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebAPI_Authentication.Database;
using WebAPI_Authentication.Models;

namespace WebAPI_Authentication.Authentication
{
    
    public class TokenRefresher: ITokenRefresher
    {
        private readonly string key;
        private readonly AuthDatabaseOperation _authDatabaseOperation;
        private readonly IConfiguration _configuration;
        private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
        public TokenRefresher(string key, IConfiguration configuration, IJwtAuthenticationManager jwtAuthenticationManager)
        {
            this.key = key;
            _configuration = configuration;
            _authDatabaseOperation = new AuthDatabaseOperation(_configuration.GetConnectionString("ReportTrackerAuthDBCon"));
            _jwtAuthenticationManager = jwtAuthenticationManager;
        }

        public AuthenticationResponse refresher(RefreshCred refreshCred) {
            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken validatedToken;
            var principal = tokenHandler.ValidateToken(refreshCred.JwtToken,
                new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                }, out validatedToken);
            var jwtToken = validatedToken as JwtSecurityToken;
            if (jwtToken == null || !jwtToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase)) {
                throw new SecurityTokenException("Invalid token passed!");
            }

            var userName = principal.Identity.Name;

            DataTable dataTable = _authDatabaseOperation.getRefreshTokenByUsername(userName);

            if (dataTable.Rows.Count <= 0)
            {
                return null;
            }
            else {
                string refreshToken = (string)dataTable.Rows[0]["Token"];
                if (refreshCred.RefreshToken != refreshToken) {
                    throw new SecurityTokenException("Invalid token passed!");
                }
            }
            return _jwtAuthenticationManager.Authenticate(userName, principal.Claims.ToArray());
        }
    }
}
