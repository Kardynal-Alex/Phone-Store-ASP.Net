using System.Linq;
using PhoneShop.Models.DataModel;
namespace PhoneShop.Models.Pagination
{
    public class IndexViewModel
    {
        public IQueryable<Product> Products { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
