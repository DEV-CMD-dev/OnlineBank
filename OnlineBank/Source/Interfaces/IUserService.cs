using OnlineBank.Data.Entities;

namespace OnlineBank.Src.Interfaces
{
    public interface IUserService
    {   
        User? GetUser(int? userId);
        List<User> GetAllUsers();
        void CreateUser(User user);
        List<Card> GetUserCards(int userId);
        void UpdateUser(User user);
        bool Login(string email, string password);
        void Logout();
        User? CurrentUser { get; }
    }
}
