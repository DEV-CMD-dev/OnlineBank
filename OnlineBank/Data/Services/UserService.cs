using OnlineBank.Data;
using OnlineBank.Data.Entities;

public static class UserService
{
    private static BankDbContext? _db;
    public static User? CurrentUser { get; private set; }

    public static void Init(BankDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public static User? GetUser(int userId)
    {
        if (_db == null) throw new InvalidOperationException("UserService is not initialized");
        return _db.Users.Find(userId);
    }

    public static List<User> GetAllUsers()
    {
        if (_db == null) throw new InvalidOperationException("UserService is not initialized");
        return _db.Users.ToList();
    }

    public static void CreateUser(User user)
    {
        if (_db == null) throw new InvalidOperationException("UserService is not initialized");
        user.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public static List<Card> GetUserCards(int userId)
    {
        if (_db == null) throw new InvalidOperationException("UserService is not initialized");
        return _db.Cards.Where(c => c.UserId == userId).ToList();
    }

    public static bool Login(string email, string password)
    {
        if (_db == null) throw new InvalidOperationException("UserService is not initialized");
        var user = _db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user == null) return false;
        CurrentUser = user;
        return true;
    }

    public static void Logout()
    {
        CurrentUser = null;
    }
}
