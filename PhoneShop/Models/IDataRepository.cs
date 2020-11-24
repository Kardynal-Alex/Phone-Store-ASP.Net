using System.Linq;
using Microsoft.AspNetCore.Http;
using PhoneShop.Models.DataModel;
namespace PhoneShop.Models
{
    public interface IDataRepository
    {
        Product GetProduct(int id);
        IQueryable<Product> GetAllProducts();
        void CreatProduct(Product newProduct, IFormFile Image);
        void UpdateProduct(Product updateProduct, IFormFile Image);
        void DeleteProduct(int id);
        IQueryable<Product> GetFilteredProduct(string brand = null, decimal? minPrice=null, decimal? maxPrice=null);
    }
}
