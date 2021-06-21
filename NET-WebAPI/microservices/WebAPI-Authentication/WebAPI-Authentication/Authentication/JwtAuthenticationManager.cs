using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;
using WebAPI_Authentication.Database;
using WebAPI_Authentication.Models;

namespace WebAPI_Authentication.Authentication
{
    public class JwtAuthenticationManager : IJwtAuthenticationManager
    {

        private readonly IDictionary<string, string> users = new Dictionary<string, string> {
            { "test", "password" }
        };
        private readonly string key;
        private readonly AuthDatabaseOperation _authDatabaseOperation;
        private readonly IConfiguration _configuration;

        public JwtAuthenticationManager(IConfiguration configuration) {
            this.key = "ThisIsMySimpleJwtKey";
            _configuration = configuration;
            _authDatabaseOperation = new AuthDatabaseOperation(_configuration.GetConnectionString("ReportTrackerAuthDBCon"));
        }

        public string Authenticate(string username, string password, string redisUrl)
        {
            //will change later
            //if (!users.Any(u => u.Key == username && u.Value == password)) {
            //    return null;
            //}

            DataTable dataTable = _authDatabaseOperation.getUser(username);
            JsonResult result = new JsonResult(dataTable);
            if (dataTable.Rows.Count <= 0)
            {
                return null;
            }
            else {
                string passwordMatch = (string)dataTable.Rows[0]["UserPassword"];
                int accountType = (int)dataTable.Rows[0]["AccountType"];
                string userEmail = (string)dataTable.Rows[0]["UserEmail"];
                string userName = (string)dataTable.Rows[0]["UserName"];
                if (password == (string)passwordMatch)
                {
                    DateTime expireTime = DateTime.UtcNow.AddHours(1);
                    var tokenHandler = new JwtSecurityTokenHandler();
                    var tokenKey = Encoding.ASCII.GetBytes(key);
                    var tokenDescriptor = new SecurityTokenDescriptor
                    {
                        Subject = new ClaimsIdentity(new Claim[]
                        {
                            new Claim(ClaimTypes.Name, username),
                            new Claim("AccountType", accountType.ToString())
                        }),
                        Expires = expireTime,
                        SigningCredentials = new SigningCredentials(
                                new SymmetricSecurityKey(tokenKey),
                                SecurityAlgorithms.HmacSha256Signature
                            )
                    };

                    var token = tokenHandler.CreateToken(tokenDescriptor);
                    string tokenStr = tokenHandler.WriteToken(token);
                    //cache to redis
                    ConnectionMultiplexer muxer = ConnectionMultiplexer.Connect(redisUrl);
                    IDatabase conn = muxer.GetDatabase();
                    string redisRecord = userName + "," + userEmail + "," + accountType;
                    conn.StringSet(tokenStr, redisRecord, expireTime - DateTime.UtcNow);
                    _authDatabaseOperation.addAuthenticatedToken(tokenStr);
                    muxer.Close();
                    return tokenStr;
                }
                else {
                    return null;
                }
            }

        }
    }
}
