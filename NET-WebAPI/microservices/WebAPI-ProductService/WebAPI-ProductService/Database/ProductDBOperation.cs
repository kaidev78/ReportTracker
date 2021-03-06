using System;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Mvc;
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
                           +product.DeveloperName + @"', '"
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

        public JsonResult getProducts(string developerName) {
            SqlDataReader myReader;
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Product WHERE DeveloperName = '"
                            + developerName
                            + @"'";
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
            return new JsonResult(dataTable);
        }

        public JsonResult getSearchResultsByName(string productName) {
            SqlDataReader myReader;
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Product WHERE ProductName = '"
                            + productName
                            + @"'";
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
            return new JsonResult(dataTable);
        }

        public JsonResult getProduct(int productId) {
            SqlDataReader myReader;
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Product WHERE ProductId = '"
                            + productId
                            + @"'";
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
            return new JsonResult(dataTable);
        }

        public void createIssue(Issue issue)
        {
            string query = @"INSERT into dbo.Issue VALUES("
                            + issue.ProductId + @", 1, '"
                            + issue.IssueName + @"', '"
                            + issue.IssueDescription + @"',"
                            + issue.IssueType + @", '"
                            + DateTime.Now + @"')";
            Console.WriteLine(query);
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

        public JsonResult getProductIssue(int productId)
        {
            SqlDataReader myReader;
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Issue WHERE ProductId = '"
                            + productId
                            + @"'";
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
            return new JsonResult(dataTable);
        }

        public JsonResult getIssue(int issueId)
        {
            SqlDataReader myReader;
            DataTable dataTable = new DataTable();
            string query = @"select * from dbo.Issue WHERE IssueId = "
                            + issueId;
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
            return new JsonResult(dataTable);
        }


        public void deleteIssue(int issueId)
        {
            string query = @"DELETE FROM dbo.Issue WHERE IssueId = " + issueId;
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

        public JsonResult updateIssueStatus(int issueId, int status) {
            string query = @"UPDATE dbo.Issue SET IssueStatus = " + status
                + @" WHERE IssueId = " + issueId;
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
            return new JsonResult(dataTable);
        }

    }
}
