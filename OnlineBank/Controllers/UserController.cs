using Microsoft.AspNetCore.Mvc;
using OnlineBank.Data.Classes;
using OnlineBank.Data.Entities;
using OnlineBank.Data.Interfaces;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly ICardService _cardService;

    public UserController(IUserService userService, ICardService cardService)
    {
        _userService = userService;
        _cardService = cardService;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        if (_userService.Login(email, password))
        {
            HttpContext.Session.SetInt32("UserId", _userService.CurrentUser!.Id);
            return RedirectToAction("Index", "Home");
        }

        ViewBag.Error = "Invalid email or password";
        return View();
    }

    [HttpGet]
    public IActionResult Register()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Register(string fullName, string email, string password, string phone, string address)
    {
        var user = new User
        {
            FullName = fullName,
            Email = email,
            Password = password,
            Phone = phone,
            Address = address,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            Cards = new List<Card>()
        };

        _userService.CreateUser(user);
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        _userService.Logout();
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Login");
    }

    public IActionResult Index() => UserView();

    public IActionResult Wallet()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return RedirectToAction("Login");

        var user = _userService.GetUser(userId.Value);
        var cards = _cardService.GetUserCards(userId.Value);

        var model = new WalletViewModel
        {
            User = _userService.GetUser(userId),
            Cards = cards
        };

        return View(model);

    }
    private int? GetCurrentUserId() => HttpContext.Session.GetInt32("UserId");
    public IActionResult Transactions() => UserView();

    public IActionResult Settings() => UserView();

    private IActionResult UserView()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return RedirectToAction("Login");

        var user = _userService.GetUser(userId.Value);
        return View(user);
    }
}
