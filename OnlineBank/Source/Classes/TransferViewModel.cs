using OnlineBank.Data.Entities;
    
namespace OnlineBank.Source.Classes
{
    public class TransferViewModel
    {
        public List<Card> UserCards { get; set; } = new List<Card>();
        public List<Transaction>? RecentTransactions { get; set; }
    }

}
