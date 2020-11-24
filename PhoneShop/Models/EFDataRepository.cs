using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using PhoneShop.Models.DataModel;
namespace PhoneShop.Models
{
    public class EFDataRepository:IDataRepository
    {
        public EFDataRepository(EFDatabaseContext ctx)
        {
            context = ctx;
        }
        private EFDatabaseContext context;

        public Product GetProduct(int id)
        {
            return context.Products.Find(id);
        }
        public IQueryable<Product> GetAllProducts()
        {
            return context.Products;
        }
        public void CreatProduct(Product newProduct, IFormFile Image)
        {
            newProduct.Id = 0;
            if (Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    newProduct.Image = ms.ToArray();
                }
            }
            context.Products.Add(newProduct);
            context.SaveChanges();
        }
        public void UpdateProduct(Product updateProduct,IFormFile Image)
        {
            Product p =context.Products.Find(updateProduct.Id);
            p.Name = updateProduct.Name;
            p.Brand = updateProduct.Brand;
            p.Price = updateProduct.Price;
            if (Image != null)
            {
                using (var ms = new MemoryStream())
                {
                    Image.CopyTo(ms);
                    p.Image = ms.ToArray();
                }
            }
            context.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            context.Products.Remove(new Product { Id = id });
            context.SaveChanges();
        }
        public IQueryable<Product> GetFilteredProduct(string brand = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> products = context.Products;
            if(brand!=null)
            {
                products = products.Where(p => p.Brand == brand);
            }
            if(minPrice!=null)
            {
                products = products.Where(p => p.Price >= minPrice);
            }
            if(maxPrice!=null)
            {
                products = products.Where(p => p.Price <= maxPrice);
            }
            return products;
        }
    }
}
