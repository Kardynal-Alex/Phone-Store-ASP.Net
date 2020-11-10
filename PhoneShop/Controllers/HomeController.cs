using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PhoneShop.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
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
        public async Task<IQueryable<Product>> GetCurrentProducts(int page, string brand = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            IQueryable<Product> source =repository.GetFilteredProduct(brand, minPrice, maxPrice);
            var count =source.Count();
            var productSelected = source.Skip((page - 1) * pageSize).Take(pageSize);
            
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

        public async Task<IActionResult> Index(int page=1, string brand = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Brand = brand;
            ViewBag.URL = HttpContext.Request.Path.ToString()+HttpContext.Request.QueryString;
            return View(await GetCurrentProducts(page, brand, minPrice, maxPrice));
        }

        public async Task<IActionResult> ShowAll(int page=1, string brand = null, decimal? minPrice = null, decimal? maxPrice = null)
        {
            ViewBag.EditDelete = true;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Brand = brand;
            return View(await GetCurrentProducts(page, brand, minPrice, maxPrice));
        }
        public IActionResult Create()
        {
            ViewBag.CreateMode = true;
            return View("Editor", new Product());
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile Image)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ShowAll");
            }
            else
            {
                repository.CreatProduct(product, Image);
                return RedirectToAction(nameof(ShowAll));
            }
        }
        public IActionResult Edit(int id)
        {
            ViewBag.CreateMode = false;
            return View("Editor", repository.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Edit(Product product, IFormFile Image)
        {
            TempData["message"] = string.Format("\"{0}\" was changed", product.Name);
            repository.UpdateProduct(product, Image);
            return RedirectToAction(nameof(ShowAll));
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            repository.DeleteProduct(id);
            return RedirectToAction(nameof(ShowAll));
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
