using Microsoft.AspNetCore.Mvc;
using OnlineBank.Data;
using OnlineBank.Data.Entities;

public class UserController : Controller
{
    private readonly BankDbContext _db;

    public UserController(BankDbContext db)
    {
        _db = db;
        if (UserService.CurrentUser == null)
        {
            UserService.Init(_db);
        }
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
}
