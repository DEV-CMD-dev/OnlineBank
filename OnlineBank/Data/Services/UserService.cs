using OnlineBank.Data;
using OnlineBank.Data.Entities;
using OnlineBank.Data.Interfaces;

public class UserService : IUserService
{
    private readonly BankDbContext _db;
    public User? CurrentUser { get; private set; }

    public UserService(BankDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public User? GetUser(int? userId)
    {
        return _db.Users.Find(userId);
    }

    public List<User> GetAllUsers()
    {
        return _db.Users.ToList();
    }

    public void CreateUser(User user)
    {
        user.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        _db.Users.Add(user);
        _db.SaveChanges();
    }

    public List<Card> GetUserCards(int userId)
    {
        return _db.Cards.Where(c => c.UserId == userId).ToList();
    }

    public bool Login(string email, string password)
    {
        var user = _db.Users.FirstOrDefault(u => u.Email == email && u.Password == password);
        if (user == null) return false;
        CurrentUser = user;
        return true;
    }

    public void Logout()
    {
        CurrentUser = null;
    }
}
