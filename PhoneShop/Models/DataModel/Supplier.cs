﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models.DataModel
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Country { get; set; }

        public int ContactId { get; set; }
        public ContactDetail ContactDetail { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}