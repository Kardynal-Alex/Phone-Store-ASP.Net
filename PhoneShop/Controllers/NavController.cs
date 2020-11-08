using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
namespace PhoneShop.Controllers
{
    public class NavController : Controller
    {
        public IDataRepository repository;
        public NavController(IDataRepository repo)
        {
            repository = repo;
        }
        string[] BrandName = { "Apple", "Google", "Samsung", "Xiomi" };
        /*public PartialViewResult Menu(string brandname)
        {
            ViewBag.BrandName = BrandName.ToArray();
            ViewBag.SelectedBrand = brandname;
            IEnumerable<string> products = repository.GetAllProducts()
                .Select(x => x.Brand).Distinct().OrderBy(x => x);
            return PartialView(products);
        }*/

    }
}
