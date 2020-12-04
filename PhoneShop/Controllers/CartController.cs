using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PhoneShop.Models;
using PhoneShop.Models.DataModel;
using System;
namespace PhoneShop.Controllers
{
    public class CartController : Controller
    {
        private IDataRepository repository;
        public static Cart cart = new Cart();

        public CartController(IDataRepository repo)
        {
            repository = repo;
        }
        public IActionResult Index(string returnUrl = "/", string Promo = null)
        {
            var priceWithDiscount = repository.GetPromoCodeByDate();
            double ComputeTotalValue = cart.ComputeTotalValue();
            if (priceWithDiscount != null && Promo != null && ComputeTotalValue > 0)    
            {
                ViewBag.PromoCode = priceWithDiscount;
                ViewBag.PromoCodeInput = Promo;
                cart.TotalPriceWithDiscount = (ComputeTotalValue * (1 - Convert.ToDouble(priceWithDiscount.DiscountPercentage * 0.01)));
            }
            return View("Index",new CartIndexViewModel
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
        public async Task<IActionResult> ToOrder(ShippingDetails shippingDetails)
        {
            ViewBag.Name = shippingDetails.Name;
            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(shippingDetails, cart);
            cart.Clear();
            return View("Thanks");
        }
    }
}
