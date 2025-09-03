using Microsoft.EntityFrameworkCore;
using OnlineBank.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<BankDbContext>(
    dbContextOptions => dbContextOptions.UseSqlServer(
        builder.Configuration["ConnectionStrings:LocalDb"])
    );

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
//    var context = scope.ServiceProvider.GetRequiredService<BankDbContext>();

//    if (!context.Users.Any())
//    {
//        var testUser = new User
//        {
//            FullName = "Ivan Ivanov",
//            Email = "ivan@example.com",
//            Phone = "+380501234567",
//            Password = "TestPassword123!",
//            Address = "Kyiv, Ukraine",
//            CreatedAt = DateOnly.FromDateTime(DateTime.Today)
//        };

//        context.Users.Add(testUser);
//        context.SaveChanges();

//        var testCard = new Card
//        {
//            UserId = testUser.Id,
//            CardNumber = "1234567812345678",
//            CardType = CardType.Visa,
//            ExpirationDate = new DateOnly(2026, 12, 1),
//            CVV = new CardCVV("123"),
//            Balance = 1000.00m,
//            IsBlocked = false,
//            CreatedAt = DateOnly.FromDateTime(DateTime.Today)
//        };

//        context.Cards.Add(testCard);
//        context.SaveChanges();
//    }
//}


app.Run();
