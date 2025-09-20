using OnlineBank.Data.Entities;

namespace OnlineBank.Source.Classes
{
    public class WalletViewModel
    {
        public User User { get; set; } = null!;
        public List<Card> Cards { get; set; } = new();
    }
}
