using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebAPI_Authentication.Models;

namespace WebAPI_Authentication.Database
{
    public class AuthDatabaseOperation
    {
        private readonly string _authDBConn;

        public AuthDatabaseOperation(string authConn)
        {
            _authDBConn = authConn;

        }

        public void addAuthenticatedToken(AuthenticationResponse token, string username) {

            string query = @"BEGIN"
                            + @" IF NOT EXISTS("
                            + @" SELECT * FROM dbo.AuthenticatedToken WHERE UserName = '" + username + @"') "
                            + @" BEGIN "
                            + @"INSERT into dbo.AuthenticatedToken VALUES('"
                                + token.RefreshToken + @"', '"
                                + username + @"', '"
                                + DateTime.Now.AddHours(1) + @"')"
                            + @" END ELSE BEGIN "
                            + @" UPDATE dbo.AuthenticatedToken SET Expire = '" + DateTime.Now.AddHours(1) + @"' ,"
                            + @" Token = '" + token.RefreshToken + @"' "
                            + @" WHERE UserName = '" + username + @"' END END";

            Console.WriteLine("query is " + query);
            DataTable dataTable = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(_authDBConn))
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
        }

        public DataTable getUser(string username) {
            string query = @"select * from dbo.Users WHERE UserName = '"
                            + username
                            + @"'";
            Console.WriteLine("query is " + query);
            DataTable dataTable = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(_authDBConn))
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
            return dataTable;
        }

        public DataTable getRefreshTokenByUsername(string username) {
            string query = @"select * from dbo.AuthenticatedToken WHERE UserName = '"
                + username
                + @"'";
            Console.WriteLine("query is " + query);
            DataTable dataTable = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(_authDBConn))
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
            return dataTable;
        }

        public void cleanExpiredToken() {
            string query = @"DELETE FROM dbo.AuthenticatedToken WHERE Expire > GETDATE()";
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(_authDBConn))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    myReader.Close();
                    myCon.Close();
                }
            }
        }

    }
}
