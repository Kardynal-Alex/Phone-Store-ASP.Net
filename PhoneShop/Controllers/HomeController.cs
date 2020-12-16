using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PhoneShop.Models.DataModel;
using PhoneShop.Models.Pagination;
using System.Collections.Generic;
using System;

namespace PhoneShop.Controllers
{   
    public class HomeController : Controller
    { 
        private IDataRepository repository;
       
        public HomeController(IDataRepository repo)
        {
            repository = repo;
        }
        const int pageSize = 10;
        public async Task<IQueryable<Product>> GetCurrentProducts(int page, string brand = null, double? minPrice = null, double? maxPrice = null)
        {
            IQueryable<Product> source = await Task.Run(()=> repository.GetFilteredProduct(brand, minPrice, maxPrice));
            var count =source.Count();
            var productSelected = await Task.Run(() => source.Skip((page - 1) * pageSize).Take(pageSize));
            
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            IndexViewModel viewModel = new IndexViewModel
            {
                PageViewModel = pageViewModel,
                Products = productSelected
            };
            ViewBag.Page = page;
            ViewBag.CountProducts = count;
            ViewBag.PageSize = pageSize;
            
            return viewModel.Products;
        }
        
        public async Task<IActionResult> Index(int page=1, string brand = null, double? minPrice = null, double? maxPrice = null)
        {
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Brand = brand;
            var promoCode = repository.GetPromoCodeByDate();
            if(promoCode!=null)
            {
                ViewBag.PromoCode = promoCode.PromoCode;
            }           
            ViewBag.URL = HttpContext.Request.Path.ToString() + HttpContext.Request.QueryString;
            return View(await GetCurrentProducts(page, brand, minPrice, maxPrice));
        }
        public async Task<IActionResult> ShowAll(int page=1, string brand = null, double? minPrice = null, double? maxPrice = null)
        {
            if (User.Identity.IsAuthenticated)
            {
                ViewBag.minPrice = minPrice;
                ViewBag.maxPrice = maxPrice;
                ViewBag.Page = page;
                return View("ShowAll", await GetCurrentProducts(page, brand, minPrice, maxPrice));
            }
            return Content("не аутентифицирован");
        }
        public IActionResult SuppliersList()
        {
            SupplierViewModel supplierViewModel = new SupplierViewModel
            {
                Suppliers = repository.GetAllSuppliers().ToList(),
                ContactDetails = repository.GetAllContactDetails().ToList()
            };
            return View(supplierViewModel);
        }
        public IActionResult Create(int Page, double? minPrice = null, double? maxPrice = null)
        {
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Page = Page;
            return View("CreateProduct", new Product());
        }
        [HttpPost]
        public async Task<IActionResult> Create(Product product, IFormFile Image, int Page, double? minPrice = null, double? maxPrice = null)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ShowAll");
            }
            else
            {
                await repository.CreatProduct(product, Image);
                return RedirectToRoute(new { Controller = "Home", Action = "ShowAll", Page = Page, minPrice = minPrice, maxPrice = maxPrice });
            }
        }
        public IActionResult Edit(int id, int Page, double? minPrice = null, double? maxPrice = null)
        {
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Page = Page;
            var product = repository.GetProductById(id);
            var suppliers = repository.GetSupplierById(product.SupplierId);
            var contact = repository.GetContactDetailById(suppliers.ContactDetailId);
            var productInfo = repository.GetProductInfoById(product.ProductInfoId);
            ViewBag.SupplierId = suppliers.Id;
            ViewBag.ProductInfoId = productInfo.Id;
            return View("EditProduct", repository.GetProductById(id));
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Product product, IFormFile Image, int SupplierId,int ProductInfoId, int Page, double? minPrice = null, double? maxPrice = null)
        {
            TempData["message"] = string.Format("\"{0}\" was changed", product.Name);
            await repository.UpdateProduct(product, Image, SupplierId,ProductInfoId);
            return RedirectToRoute(new { Controller = "Home", Action = "ShowAll", Page = Page, minPrice = minPrice, maxPrice = maxPrice });
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id, int Page, double? minPrice = null, double? maxPrice = null)
        {
            await repository.DeleteProduct(id);
            return RedirectToRoute(new { Controller = "Home", Action = "ShowAll", Page = Page, minPrice = minPrice, maxPrice = maxPrice });
        }
        public IActionResult EditSupplier(int id)
        {
            var supplier = repository.GetSupplierById(id);
            var contact = repository.GetContactDetailById(supplier.ContactDetailId);
            ViewBag.ContactDetailId = supplier.ContactDetailId;
            return View(supplier);
        }
        [HttpPost]
        public async Task<IActionResult> EditSupplier(Supplier supplier,int contactId)
        {
            await repository.UpdateSupplier(supplier, contactId);
            return RedirectToAction(nameof(SuppliersList));
        }
        public IActionResult PromoCodeList()
        {
            return View(repository.GetAllPromoCode());
        }
        public IActionResult CreatePromoCode()
        {
            ViewBag.CreateMode = true;
            return View("CreatePromoCode");
        }
        [HttpPost]
        public async Task<IActionResult> CreatePromoCode(PromoCodeSystem promoCodeSystem)
        {
            ViewBag.CreateMode = true;
            if (promoCodeSystem.Date1 > promoCodeSystem.Date2)
            {
                ModelState.AddModelError("Date1", "Date1 must be less equal than Date2");
            }
            else
            if (ModelState.IsValid)
            {
                await repository.CreatePromoCode(promoCodeSystem);
                return RedirectToAction(nameof(PromoCodeList));
            }
            return View();
        }
        public IActionResult EditPromoCode(int id)
        {
            ViewBag.CreateMode = false;
            return View("CreatePromoCode",repository.GetPromoCodeById(id));
        }
        [HttpPost]
        public async Task<IActionResult> EditPromoCode(PromoCodeSystem promoCodeSystem)
        {
            ViewBag.CreateMode = false;
            if (promoCodeSystem.Date1 > promoCodeSystem.Date2)
            {
                ModelState.AddModelError("Date1", "Date1 must be less equal than Date2");
            }
            else
            if (ModelState.IsValid)
            {
                await repository.UpdatePromoCode(promoCodeSystem);
                return RedirectToAction(nameof(PromoCodeList));
            }
            return View("CreatePromoCode");
        }
        [HttpPost]
        public async Task<IActionResult> DeletePromoCode(int id)
        {
            await repository.DeletePromoCode(id);
            return RedirectToAction(nameof(PromoCodeList));
        }
        public IActionResult DetailInfoAboutPhone(int productId)
        {
            var product = repository.GetProductById(productId);
            var productInfo = repository.GetProductInfoById(product.ProductInfoId);
            var promoCode = repository.GetPromoCodeByDate();
            if(promoCode!=null)
            {
                ViewBag.PromoCode = promoCode.DiscountPercentage;
                ViewBag.PriceWithPromo = (product.Price * (1 - promoCode.DiscountPercentage * 0.01));
            }
            return View(product);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
