using Microsoft.AspNetCore.Mvc;
using OnlineBank.Controllers;
using OnlineBank.Source.Classes;
using OnlineBank.Data.Entities;
using OnlineBank.Source.Interfaces;

public class UserController : BaseController
{
    private readonly ICardService _cardService;
    private readonly ITransactionService _transactionService;

    public UserController(IUserService userService, ICardService cardService, ITransactionService transactionService)
        : base(userService)
    {
        _cardService = cardService;
        _transactionService = transactionService;
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
            CreatedAt = DateTime.Now,
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
        if (user == null)
            return RedirectToAction("Login");

        var cards = _cardService.GetUserCards(userId.Value);

        var model = new WalletViewModel
        {
            User = user,
            Cards = cards
        };

        return View(model);

    }

    public IActionResult UpdateSettings(string fullName, string phone, string address)
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return RedirectToAction("Login");
        var user = _userService.GetUser(userId.Value);
        if (user == null)
            return RedirectToAction("Login");
        user.FullName = fullName;
        user.Phone = phone;
        user.Address = address;

        _userService.UpdateUser(user);
        return RedirectToAction("Settings");
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

    [HttpPost]
    public IActionResult Transfer(int senderCardId, string recipientCardNumber, decimal amount)
    {
        var userId = GetCurrentUserId();
        if (userId == null) return RedirectToAction("Login");

        var senderCard = _cardService.GetCard(senderCardId);
        if (senderCard == null || senderCard.UserId != userId.Value || senderCard.IsBlocked)
        {
            TempData["Error"] = "Invalid sender card.";
            return RedirectToAction("Transfer");
        }

        var recipientCard = _cardService.GetCardByNumber(recipientCardNumber);
        if (recipientCard == null)
        {
            TempData["Error"] = "Recipient card not found.";
            return RedirectToAction("Transfer");
        }

        try
        {
            _transactionService.CreateTransaction(senderCardId, recipientCard.Id, amount, "User Transfer");
            TempData["Success"] = $"Transferred ${amount} to card {recipientCardNumber}.";
        }
        catch (Exception ex)
        {
            TempData["Error"] = ex.Message;
        }

        return RedirectToAction("Transfer");
    }

    [HttpGet]
    public IActionResult Transfer()
    {
        var userId = GetCurrentUserId();
        if (userId == null)
            return RedirectToAction("Login");

        var userCards = _cardService.GetUserCards(userId.Value)
                                    .Where(c => !c.IsBlocked)
                                    .ToList();

        var allTxs = _transactionService.GetAllTransactionsForUser(userId.Value);

        var recentTransactions = allTxs
                                 .OrderByDescending(t => t.CreatedAt)
                                 .Take(5)
                                 .ToList();

        var model = new TransferViewModel
        {
            UserCards = userCards,
            RecentTransactions = recentTransactions
        };

        return View(model);
    }




}
