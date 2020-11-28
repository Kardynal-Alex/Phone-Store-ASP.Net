using System.Linq;
using Microsoft.AspNetCore.Http;
using PhoneShop.Models.DataModel;
namespace PhoneShop.Models
{
    public interface IDataRepository
    {
        Product GetProductById(int id);
        Supplier GetSupplierById(int id);
        ContactDetail GetContactDetailById(int id);
        IQueryable<Product> GetAllProducts();
        IQueryable<Supplier> GetAllSuppliers();
        IQueryable<ContactDetail> GetAllContactDetails();
        void CreatProduct(Product newProduct, IFormFile Image);
        void UpdateProduct(Product updateProduct, IFormFile Image);
        void DeleteProduct(int id);
        IQueryable<Product> GetFilteredProduct(string brand = null, double? minPrice=null, double? maxPrice=null);
        void UpdateSupplier(Supplier supplier, int contactDetailId);
    }
}
