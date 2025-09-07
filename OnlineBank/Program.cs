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
        builder.Configuration["ConnectionStrings:LocalDb"])
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

using (var scope = app.Services.CreateScope())
{
    var userService = scope.ServiceProvider.GetRequiredService<UserService>();
    var cardService = scope.ServiceProvider.GetRequiredService<CardService>();

    var testUser = new User
    {
        FullName = "Vasya pupkin",
        Email = "vasya@gmail.com",
        Phone = "+380678888888",
        Password = "VeryHardPass",
        Address = "Rivne, Ukraine",
        CreatedAt = DateOnly.FromDateTime(DateTime.Today)
    };

    userService.CreateUser(testUser);

    var testCard = new Card
    {
        UserId = testUser.Id,
        CardNumber = "8888567855556127",
        CardType = CardType.MasterCard,
        ExpirationDate = new DateOnly(2028, 12, 1),
        CVV = new CardCVV("555"),
        Balance = 10000.00m,
        IsBlocked = false,
        CreatedAt = DateOnly.FromDateTime(DateTime.Today)
    };

    cardService.AddCard(testCard);
}



app.Run();
