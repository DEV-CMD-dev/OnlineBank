using Microsoft.EntityFrameworkCore;
using OnlineBank.Data;
using OnlineBank.Data.Entities;
using OnlineBank.Data.Enums;
using OnlineBank.Data.Structs;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BankDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:RemoteDb"])
    );

builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<CardService>();
builder.Services.AddScoped<TransactionService>();


var app = builder.Build();
    
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//using (var scope = app.Services.CreateScope())
//{
//    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
//    var cardService = scope.ServiceProvider.GetRequiredService<CardService>();

//    var testUser = new User
//    {
//        FullName = "Oleg Melnik",
//        Email = "ol23@gmail.com",
//        Phone = "+380995523485",
//        Password = "TestPasg@5ac",
//        Address = "Kyiv, Ukraine",
//        CreatedAt = DateOnly.FromDateTime(DateTime.Today)
//    };

//    userService.CreateUser(testUser);

//    var testCard = new Card
//    {
//        UserId = testUser.Id,
//        CardNumber = "8558645265896127",
//        CardType = CardType.UnionPay,
//        ExpirationDate = new DateOnly(2028, 12, 1),
//        CVV = new CardCVV("113"),
//        Balance = 10.50m,
//        IsBlocked = false,
//        CreatedAt = DateOnly.FromDateTime(DateTime.Today)
//    };

//    cardService.AddCard(testCard);
//}



app.Run();
