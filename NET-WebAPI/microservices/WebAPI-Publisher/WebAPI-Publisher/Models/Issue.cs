using System;
namespace WebAPI_ProductService.Models
{
    public class Issue
    {
        public int ProductId { get; set; }
        public string IssueName { get; set; }
        public string IssueDescription { get; set; }
        public int IssueType { get; set; }
    }
}
