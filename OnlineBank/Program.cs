using Microsoft.EntityFrameworkCore;
using OnlineBank.Data;
using OnlineBank.Source.Enums;
using OnlineBank.Source.Interfaces;
using OnlineBank.Source.Structs;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

// Services
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<ICardService, CardService>();
builder.Services.AddScoped<ITransactionService, TransactionService>();

// DB
builder.Services.AddDbContext<BankDbContext>(
    options => options.UseSqlServer(
        builder.Configuration["ConnectionStrings:RemoteDb"]));

// Session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Session
app.UseSession();

app.UseAuthorization();

app.UseStatusCodePagesWithReExecute("/Error/{0}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//using (var scope = app.Services.CreateScope())
//{
//    var cardService = scope.ServiceProvider.GetRequiredService<ICardService>();

//    var cards = new[]
//    {
//        new Card
//        {
//            UserId = 1,
//            CardNumber = "4000123412341234",
//            CardType = CardType.UnionPay,
//            ExpirationDate = DateTime.UtcNow.AddYears(5),
//            CVV = new CardCVV("123"),
//            Balance = 1000m,
//            IsBlocked = false,
//            CreatedAt = DateTime.UtcNow
//        },
//        new Card
//        {
//            UserId = 1,
//            CardNumber = "4000567812345678",
//            CardType = CardType.MasterCard,
//            ExpirationDate = DateTime.UtcNow.AddYears(3),
//            CVV = new CardCVV("456"),
//            Balance = 5000m,
//            IsBlocked = false,
//            CreatedAt = DateTime.UtcNow
//        },
//        new Card
//        {
//            UserId = 1,
//            CardNumber = "4000789012347890",
//            CardType = CardType.Visa,
//            ExpirationDate = DateTime.UtcNow.AddYears(2),
//            CVV = new CardCVV("789"),
//            Balance = 200m,
//            IsBlocked = false,
//            CreatedAt = DateTime.UtcNow
//        }
//    };

//    foreach (var card in cards)
//    {
//        cardService.AddCard(card);
//    }
//}

app.Run();
