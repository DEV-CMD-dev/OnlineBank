using Microsoft.AspNetCore.Mvc;
using OnlineBank.Data.Entities;
using OnlineBank.Source.Enums;
using OnlineBank.Source.Interfaces;
using OnlineBank.Source.Structs;

namespace OnlineBank.Controllers
{
    public class ManagementController : BaseController
    {
        private readonly ICardService _cardService;

        public ManagementController(IUserService userService, ICardService cardService)
            : base(userService)
        {
            _cardService = cardService;
        }

        public IActionResult Users(string search)
        {
            var users = _userService.GetAllUsers();

            if (!string.IsNullOrEmpty(search))
            {
                users = users
                    .Where(u => u.FullName.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                u.Email.Contains(search, StringComparison.OrdinalIgnoreCase) ||
                                u.Phone.Contains(search, StringComparison.OrdinalIgnoreCase))
                    .ToList();
            }

            return View(users);
        }

        [HttpGet]
        public IActionResult EditUser(int id)
        {
            var user = _userService.GetUser(id);
            if (user == null) return NotFound();

            user.Cards = _cardService.GetUserCards(id);
            return View(user);
        }

        [HttpPost]
        public IActionResult EditUser(User model)
        {
            var user = _userService.GetUser(model.Id);
            if (user == null) return NotFound();

            user.FullName = model.FullName;
            user.Phone = model.Phone;
            user.Address = model.Address;

            _userService.UpdateUser(user);

            return RedirectToAction("Users");
        }

        [HttpPost]
        public IActionResult AddCard(int UserId, string CardNumber, CardType CardType, DateTime ExpirationDate, string CVV)
        {
            try
            {
                var card = new Card
                {
                    UserId = UserId,
                    CardNumber = CardNumber,
                    CardType = CardType,
                    ExpirationDate = ExpirationDate,
                    CVV = new CardCVV(CVV),
                    Balance = 0,
                    IsBlocked = false,
                    CreatedAt = DateTime.Now
                };

                _cardService.AddCard(card);
                return RedirectToAction("EditUser", new { id = UserId });
            }
            catch (Exception ex)
            {
                TempData["CardError"] = ex.Message;
                return RedirectToAction("EditUser", new { id = UserId });
            }
        }

        public IActionResult BlockCard(int id)
        {
            _cardService.BlockCard(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public IActionResult UnblockCard(int id)
        {
            _cardService.UnblockCard(id);
            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
