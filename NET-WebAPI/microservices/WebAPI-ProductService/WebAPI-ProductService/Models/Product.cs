﻿using System;
namespace WebAPI_ProductService.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public int DeveloperId { get; set; }
        public string ProductName { get; set; }
    }
}