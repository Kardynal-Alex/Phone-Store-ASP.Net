using System.Linq;
using System.Threading.Tasks;
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
        Task CreatProduct(Product newProduct, IFormFile Image);
        Task UpdateProduct(Product updateProduct, IFormFile Image, int SupplierId, int ProductInfoId);
        Task DeleteProduct(int id);
        IQueryable<Product> GetFilteredProduct(string brand = null, double? minPrice=null, double? maxPrice=null);
        Task UpdateSupplier(Supplier supplier, int contactDetailId);
        IQueryable<PromoCodeSystem> GetAllPromoCode();
        PromoCodeSystem GetPromoCodeById(int id);
        PromoCodeSystem GetPromoCodeByDate();
        Task CreatePromoCode(PromoCodeSystem promoCodeSystem);
        Task UpdatePromoCode(PromoCodeSystem promoCodeSystem);
        Task DeletePromoCode(int id);
        ProductInfo GetProductInfoById(int id);
        IQueryable<ProductInfo> GetAllProductInfos();
    }
}
