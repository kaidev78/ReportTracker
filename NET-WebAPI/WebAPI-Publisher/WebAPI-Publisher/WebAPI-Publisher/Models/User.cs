using System;
namespace WebAPI_Publisher.Models
{
    public class User
    {
        public string UserEmail { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int UserType { get; set; }
    }
}
