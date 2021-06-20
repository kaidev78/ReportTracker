using System;
namespace WebAPI_ProductService.Models
{
    public class Product
    {
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int DeveloperId { get; set; }
        public string DeveloperName { get; set; }
    }
}
