using OnlineBank.Data.Entities;
using OnlineBank.Source.Enums;
using OnlineBank.Source.Structs;

public class Card
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public required string CardNumber { get; set; }
    public CardType CardType { get; set; }
    public DateTime ExpirationDate { get; set; }
    public CardCVV CVV { get; set; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }
    public DateTime CreatedAt { get; set; }

    public ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
    public ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();
}
