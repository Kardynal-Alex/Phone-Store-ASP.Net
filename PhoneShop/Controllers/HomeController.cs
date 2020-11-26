using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authorization;
using PhoneShop.Models.DataModel;
using PhoneShop.Models.Pagination;
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
                return View("ShowAll",await GetCurrentProducts(page, brand, minPrice, maxPrice));
            }
            return Content("не аутентифицирован");
        }

        public IActionResult Create(int Page, double? minPrice = null, double? maxPrice = null)
        {
            //ViewBag.CreateMode = true;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Page = Page;
            return View("CreateProduct", new Product());
        }
        [HttpPost]
        public IActionResult Create(Product product, IFormFile Image, int Page, double? minPrice = null, double? maxPrice = null)
        {
            if (!ModelState.IsValid)
            {
                return RedirectToAction("ShowAll");
            }
            else
            {
                repository.CreatProduct(product, Image);
                return RedirectToRoute(new { Controller = "Home", Action = "ShowAll", page = Page, minPrice = minPrice, maxPrice = maxPrice });
            }
        }
        
        public IActionResult Edit(int id, int Page, double? minPrice = null, double? maxPrice = null)
        {
            ViewBag.CreateMode = false;
            ViewBag.minPrice = minPrice;
            ViewBag.maxPrice = maxPrice;
            ViewBag.Page = Page;
            return View("Editor", repository.GetProduct(id));
        }
        [HttpPost]
        public IActionResult Edit(Product product, IFormFile Image, int Page, double? minPrice = null, double? maxPrice = null)
        {
            TempData["message"] = string.Format("\"{0}\" was changed", product.Name);
            repository.UpdateProduct(product, Image);
            return RedirectToRoute(new { Controller = "Home", Action = "ShowAll", page = Page, minPrice = minPrice, maxPrice = maxPrice });
        }
        [HttpPost]
        public IActionResult Delete(int id, int Page, double? minPrice = null, double? maxPrice = null)
        {
            repository.DeleteProduct(id);
            return RedirectToRoute(new { Controller = "Home", Action = "ShowAll", page = Page, minPrice = minPrice, maxPrice = maxPrice });
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
