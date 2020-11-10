using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhoneShop.Models;
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
            return context.Productss.Find(id);
        }
        public IQueryable<Product> GetAllProducts()
        {
            return context.Productss;
        }
        public void CreatProduct(Product newProduct, IFormFile Image)
        {
            newProduct.Id = 0;
            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = Image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    newProduct.Image = p1;
                }
            }
            context.Productss.Add(newProduct);
            context.SaveChanges();
        }
        public void UpdateProduct(Product updateProduct,IFormFile Image)
        {
            Product p =context.Productss.Find(updateProduct.Id);
            p.Name = updateProduct.Name;
            p.Brand = updateProduct.Brand;
            p.Price = updateProduct.Price;
            if (Image != null)
            {
                if (Image.Length > 0)
                {
                    byte[] p1 = null;
                    using (var fs1 = Image.OpenReadStream())
                    using (var ms1 = new MemoryStream())
                    {
                        fs1.CopyTo(ms1);
                        p1 = ms1.ToArray();
                    }
                    p.Image = p1;
                }
            }
            context.SaveChanges();
        }
        public void DeleteProduct(int id)
        {
            context.Productss.Remove(new Product { Id = id });
            context.SaveChanges();
        }
        public IQueryable<Product> GetFilteredProduct(string brand = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> products = context.Productss;
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
