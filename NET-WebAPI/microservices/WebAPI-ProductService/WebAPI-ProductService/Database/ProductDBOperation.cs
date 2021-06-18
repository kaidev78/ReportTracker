using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using WebAPI_ProductService.Models;

namespace WebAPI_ProductService.Database
{
    public class ProductDBOperation
    {
        private readonly string _productDBConn;
        private readonly string _authDBConn;

        public ProductDBOperation(string productDBConn, string authDBConn)
        {
            _productDBConn = productDBConn;
            _authDBConn = authDBConn;
        }


        public DataTable getUserInfo(string userName) {
            SqlDataReader myReader;
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Users WHERE UserName = '"
                            + userName
                            + @"'";
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

        public void createProduct(Product product) {
            string query = @"INSERT into dbo.Product VALUES("
                           +product.DeveloperId + @", '"
                           +product.ProductName + @"', '"
                           +product.ProductDescription + @"', '"
                           +DateTime.Now + @"')";
            DataTable dataTable = new DataTable();
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(_productDBConn))
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

    }
}
