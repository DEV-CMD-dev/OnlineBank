using OnlineBank.Data;
using OnlineBank.Data.Entities;

public class UserService
{
    private readonly BankDbContext _db;

    public UserService(BankDbContext db)
    {
        _db = db;
    }

    public User? GetUser(int userId) => _db.Users.Find(userId);

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
}
