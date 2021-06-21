using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebAPI_Authentication.Database
{
    public class AuthDatabaseOperation
    {
        private readonly string _authDBConn;

        public AuthDatabaseOperation(string authConn)
        {
            _authDBConn = authConn;

        }

        public void addAuthenticatedToken(string token) {
            string query = @"INSERT into dbo.AuthenticateToken VALUES('"
                + token + @"', '" + DateTime.Now.AddHours(1)
                + @"')";
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

        public void cleanExpiredToken() {
            string query = @"DELETE FROM dbo.AuthenticateToken WHERE Expire > GETDATE()";
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
