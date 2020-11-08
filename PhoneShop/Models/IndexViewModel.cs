using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneShop.Models
{
    public class IndexViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
