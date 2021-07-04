using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using WebAPI_Authentication.Authentication;
using WebAPI_Authentication.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI_Authentication.Controllers
{

    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IJwtAuthenticationManager _jwtAuthentication;
        private readonly ITokenRefresher _tokenRefresher;

        public UserController(
            IConfiguration configuration,
            IJwtAuthenticationManager jwtAuthenticationManager,
            ITokenRefresher tokenRefresher) {
            _configuration = configuration;
            _jwtAuthentication = jwtAuthenticationManager;
            _tokenRefresher = tokenRefresher;
        }

        // POST: api/login
        // login a user
        [HttpPost("/user/login")]
        public JsonResult Login([FromBody] UserLogin userLogin)
        {
            string query = @"select UserPassword from dbo.Users WHERE UserName = '"
                            + userLogin.UserName
                            + @"'";
            Console.WriteLine("query is " + query);
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ReportTrackerAuthDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }

            JsonResult result = new JsonResult(dataTable);
            Console.WriteLine("Try to login user " + dataTable.Rows[0]["UserPassword"]);
            return result;
        }

        // POST: api/register
        // register a user
        [HttpPost("/user/register")]
        public IActionResult Register([FromBody] User user)
        {

            string query = @"SELECT * FROM dbo.Users WHERE UserName = '" + user.UserName + "'";
            DataTable dataTable = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ReportTrackerAuthDBCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    dataTable.Load(myReader);

                    myReader.Close();
                    myCon.Close();
                }
            }
            int found = dataTable.Rows.Count;
            if (found > 0)
            {
                // There are user already existed
                return StatusCode(400, "User is already existed");
            }
            else
            {
                query = @"INSERT into dbo.Users VALUES ('" +
                user.UserName + @"', '" +
                user.UserPassword + @"', '" +
                user.UserEmail + @"', '" +
                DateTime.Now + @"', '" +
                user.UserType + @"')";
                Console.WriteLine("query is " + query);
                dataTable = new DataTable();
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    using (SqlCommand myCommand = new SqlCommand(query, myCon))
                    {
                        myReader = myCommand.ExecuteReader();
                        //dataTable.Load(myReader);

                        myReader.Close();
                        myCon.Close();
                    }
                }

                return new JsonResult("Updated Sucessfully");
            }
        }

        [HttpPost("/user/authtest")]
        public IActionResult Authenticate([FromBody] UserLogin userLogin) {
            string redisDataSource = _configuration.GetConnectionString("ReportTrackerRedisCon");
            AuthenticationResponse authenticationResponse = _jwtAuthentication.Authenticate(userLogin.UserName, userLogin.UserPassword, redisDataSource);
            if (authenticationResponse == null) return Unauthorized();
            Console.WriteLine("token is :" + authenticationResponse);
            return Ok(authenticationResponse);
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        public IActionResult Refresh([FromBody] RefreshCred refreshCred)
        {
            var token = _tokenRefresher.refresher(refreshCred);
            if (token == null) {
                return Unauthorized();
            }

            return Ok(token);
        }

    }

}
