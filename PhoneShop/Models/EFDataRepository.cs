﻿using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using PhoneShop.Models.DataModel;
using System;
namespace PhoneShop.Models
{
    public class EFDataRepository:IDataRepository
    {
        public EFDataRepository(EFDatabaseContext ctx)
        {
            context = ctx;
        }
        private EFDatabaseContext context;

        public Product GetProductById(int id) => context.Products.Find(id);
        public IQueryable<Product> GetAllProducts() => context.Products;
        public async Task CreatProduct(Product newProduct, IFormFile Image)
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

                ProductInfo productInfo = new ProductInfo();
                productInfo.ScreenResolution = newProduct.ProductInfo.ScreenResolution;
                productInfo.Camera = newProduct.ProductInfo.Camera;
                productInfo.CPU = newProduct.ProductInfo.CPU;
                productInfo.NumberOfCores = newProduct.ProductInfo.NumberOfCores;
                productInfo.RAMSize = newProduct.ProductInfo.RAMSize;
                productInfo.BuiltInMemory = newProduct.ProductInfo.BuiltInMemory;
                productInfo.OS = newProduct.ProductInfo.OS;
                productInfo.BatteryCapacity = newProduct.ProductInfo.BatteryCapacity;
                product.ProductInfo = productInfo;

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
            await context.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product updateProduct, IFormFile Image, int SupplierId, int ProductInfoId)
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

            Supplier s = context.Suppliers.Find(SupplierId);
            s.Name = updateProduct.Supplier.Name;
            s.Country = updateProduct.Supplier.Country;

            ContactDetail c = context.ContactDetails.Find(s.ContactDetailId);
            c.Name = updateProduct.Supplier.ContactDetail.Name;
            c.Phone = updateProduct.Supplier.ContactDetail.Phone;

            ProductInfo productInfo = context.ProductInfos.Find(ProductInfoId);
            productInfo.ScreenResolution = updateProduct.ProductInfo.ScreenResolution;
            productInfo.Camera = updateProduct.ProductInfo.Camera;
            productInfo.CPU = updateProduct.ProductInfo.CPU;
            productInfo.NumberOfCores = updateProduct.ProductInfo.NumberOfCores;
            productInfo.RAMSize = updateProduct.ProductInfo.RAMSize;
            productInfo.BuiltInMemory = updateProduct.ProductInfo.BuiltInMemory;
            productInfo.OS = updateProduct.ProductInfo.OS;
            productInfo.BatteryCapacity = updateProduct.ProductInfo.BatteryCapacity;

            context.Products.Update(p);
            context.Suppliers.Update(s);
            context.ContactDetails.Update(c);
            context.ProductInfos.Update(productInfo);
            await context.SaveChangesAsync();
        }
        public async Task DeleteProduct(int id)
        {
            var product = context.Products.Find(id);
            var productInfo = context.ProductInfos.Find(product.ProductInfoId);
            context.Products.Remove(product);
            context.ProductInfos.Remove(productInfo);
            await context.SaveChangesAsync();
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
        public IQueryable<Supplier> GetAllSuppliers() => context.Suppliers;
        public IQueryable<ContactDetail> GetAllContactDetails() => context.ContactDetails;
        public Supplier GetSupplierById(int id) => context.Suppliers.Find(id);
        public ContactDetail GetContactDetailById(int id) => context.ContactDetails.Find(id);
        public async Task UpdateSupplier(Supplier supplier, int contactDetailId)
        {
            Supplier s = context.Suppliers.Find(supplier.Id);
            s.Name = supplier.Name;
            s.Country = supplier.Country;
            ContactDetail c = context.ContactDetails.Find(contactDetailId);
            c.Name = supplier.ContactDetail.Name;
            c.Phone = supplier.ContactDetail.Phone;
            context.Suppliers.Update(s);
            context.ContactDetails.Update(c);
            await context.SaveChangesAsync();
        }
        public async Task CreatePromoCode(PromoCodeSystem promoCodeSystem)
        {
            context.PromoCodeSystems.Add(promoCodeSystem);
            await context.SaveChangesAsync();
        }
        public IQueryable<PromoCodeSystem> GetAllPromoCode() => context.PromoCodeSystems;
        public PromoCodeSystem GetPromoCodeById(int id) => context.PromoCodeSystems.Find(id);
        public async Task UpdatePromoCode(PromoCodeSystem promoCodeSystem)
        {
            PromoCodeSystem p = context.PromoCodeSystems.Find(promoCodeSystem.Id);
            p.PromoCode = promoCodeSystem.PromoCode;
            p.Date1 = promoCodeSystem.Date1;
            p.Date2 = promoCodeSystem.Date2;
            p.DiscountPercentage = promoCodeSystem.DiscountPercentage;
            context.PromoCodeSystems.Update(p);
            await context.SaveChangesAsync();
        }
        public PromoCodeSystem GetPromoCodeByDate()
        {
            return context.PromoCodeSystems.Where(x => DateTime.Now.Date >= x.Date1 && DateTime.Now.Date <= x.Date2).Select(x => x).FirstOrDefault();
        }
        public async Task DeletePromoCode(int id)
        {
            context.PromoCodeSystems.Remove(new PromoCodeSystem { Id = id });
            await context.SaveChangesAsync();
        }
        public ProductInfo GetProductInfoById(int id) => context.ProductInfos.Find(id);
        public IQueryable<ProductInfo> GetAllProductInfos() => context.ProductInfos;

    }
}
