﻿using System.ComponentModel.DataAnnotations;

namespace StudentApi.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public required string ProductName { get; set; }
        public required int Price { get; set; }
        public required string Description { get; set; }
        public int? Rating { get; set; }
        public bool Status { get; set; }
    }
}
