using System;
namespace WebAPI_ProductService.Models
{
    public class ProductCreation
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Token { get; set; }
    }
}
