using Microsoft.AspNetCore.Mvc;
using OnlineBank.Data;
using OnlineBank.Data.Entities;
using OnlineBank.Data.Services;

public class UserController : Controller
{
    private readonly BankDbContext _db;

    public UserController(BankDbContext db)
    {
        _db = db;
        ServiceInitializer.InitAll(_db);
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        if (UserService.Login(email, password))
        {
            HttpContext.Session.SetInt32("UserId", UserService.CurrentUser!.Id);
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

        UserService.CreateUser(user);
        return RedirectToAction("Login");
    }

    public IActionResult Logout()
    {
        UserService.Logout();
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Login");
    }

    public IActionResult Index() => UserView();

    public IActionResult Wallet() => UserView();

    public IActionResult Transactions() => UserView();

    public IActionResult Settings() => UserView();

    private IActionResult UserView()
    {
        if (UserService.CurrentUser == null)
        {
            return RedirectToAction("Login");
        }
        return View(UserService.CurrentUser);
    }
}
