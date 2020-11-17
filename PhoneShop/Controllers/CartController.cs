﻿using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;

namespace PhoneShop.Controllers
{
    public class CartController : Controller
    {
        private IDataRepository repository;
        public static Cart cart=new Cart();

        public CartController(IDataRepository repo)
        {
            repository = repo;
        }
        public ViewResult Index(string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }
        public RedirectToActionResult AddToCart(int productId, string returnUrl)
        {
            Product product = repository.GetAllProducts().FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                cart.AddItem(product, 1); 
            }
            return RedirectToAction("Index", "Cart", new { returnUrl});
        }
        public IActionResult RemoveFromCart(int productId, string returnUrl)
        {
            Product product = repository.GetAllProducts().FirstOrDefault(x => x.Id == productId);
            if (product != null)
            {
                cart.RemoveLine(product);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ViewResult ToOrder(string URL)
        {
            ViewBag.URLaddress = URL;
            return View();
        }
        [HttpPost]
        public IActionResult ToOrder(ShippingDetails shippingDetails)
        {
            cart.Clear();
            ViewBag.Name = shippingDetails.Name;
            return View("Thanks");
        }
    }
}
