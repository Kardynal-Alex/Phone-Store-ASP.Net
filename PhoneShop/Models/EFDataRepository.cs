using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public Product GetProductById(int id)
        {
            return context.Products.Find(id);
        }
        public IQueryable<Product> GetAllProducts()
        {
            return context.Products;
        }
        public void CreatProduct(Product newProduct, IFormFile Image)
        {
            var existingSupplier = context.Suppliers.FirstOrDefault(x => x.Name == newProduct.Supplier.Name && x.ContactDetail.Name == newProduct.Supplier.ContactDetail.Name);
            if (existingSupplier != null)
            {
                Product product = new Product();
                product.Name = newProduct.Name;
                product.Brand = newProduct.Brand;
                product.Price = newProduct.Price;
                if (Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Image.CopyTo(ms);
                        newProduct.Image = ms.ToArray();
                    }
                }
                product.Image = newProduct.Image;
                product.SupplierId = existingSupplier.Id;
                context.Products.Add(product);
            }
            else
            {
                if (Image != null)
                {
                    using (var ms = new MemoryStream())
                    {
                        Image.CopyTo(ms);
                        newProduct.Image = ms.ToArray();
                    }
                }
                context.Products.Add(newProduct);
            }
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
        public IQueryable<Product> GetFilteredProduct(string brand = null, double? minPrice = null, double? maxPrice = null)
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
        public IQueryable<Supplier> GetAllSuppliers()
        {
            return context.Suppliers; 
        }
        public IQueryable<ContactDetail> GetAllContactDetails()
        {
            return context.ContactDetails;
        }

    }
}
