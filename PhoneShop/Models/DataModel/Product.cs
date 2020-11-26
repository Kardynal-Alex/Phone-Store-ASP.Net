﻿using System;
using System.ComponentModel.DataAnnotations;

namespace PhoneShop.Models.DataModel
{
    public class Product
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Enter name for product")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Choose brand")]
        public string Brand { get; set; }
        [Required(ErrorMessage = "Enter Price")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Out of range")]
        public double Price { get; set; }
        public byte[] Image { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }
    }
}
