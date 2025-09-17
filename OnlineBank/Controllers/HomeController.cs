using Microsoft.AspNetCore.Mvc;
using OnlineBank.Models;
using OnlineBank.Src.Classes;
using OnlineBank.Src.Interfaces;
using System.Diagnostics;

namespace OnlineBank.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IUserService userService)
            : base(userService)
        {
            _logger = logger;
        }

        public ActionResult Index()
        {
            var cards = new List<PayCardViewModel>
            {
                new PayCardViewModel
                {
                    LogoPath = "/images/cards/visa.png",
                    Title = "Visa",
                    Description = "Wide acceptance worldwide",
                    Perks = new List<string> { "Contactless payments", "0.5% cashback on groceries" }
                },
                new PayCardViewModel
                {
                    LogoPath = "/images/cards/mastercard.png",
                    Title = "MasterCard",
                    Description = "Secure payments and premium offers",
                    Perks = new List<string> { "Airport lounge access", "1% cashback on travel" }
                },
                new PayCardViewModel
                {
                    LogoPath = "/images/cards/unionpay.png",
                    Title = "UnionPay",
                    Description = "Good rates in Asia",
                    Perks = new List<string> { "Low FX fees", "Partner discounts" }
                },
                new PayCardViewModel
                {
                    LogoPath = "/images/cards/googlepay.png",
                    Title = "GooglePay",
                    Description = "Smart payments across Europe",
                    Perks = new List<string> { "Instant cashback", "Secure NFC transactions", "Works with loyalty programs" }
                },
                new PayCardViewModel
                {
                    LogoPath = "/images/cards/applepay.png",
                    Title = "Apple Pay",
                    Description = "Pay quickly with your phone",
                    Perks = new List<string> { "Easy in-app payments", "Tokenized security" }
                }

            };

            return View(cards);
        }

        public IActionResult Wallet()
        {
            return View();
        }
        
        public IActionResult Transactions()
        {
            return View();
        }
        
        public IActionResult Settings()
        {
            return View();
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
