using OnlineBank.Data.Entities;
using OnlineBank.Src.Enums;
using OnlineBank.Src.Structs;

public class Card
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public string CardNumber { get; set; }
    public CardType CardType { get; set; }
    public DateOnly ExpirationDate { get; set; }
    public CardCVV CVV { get; set; }
    public decimal Balance { get; set; }
    public bool IsBlocked { get; set; }
    public DateOnly CreatedAt { get; set; }

    public ICollection<Transaction> SentTransactions { get; set; } = new List<Transaction>();
    public ICollection<Transaction> ReceivedTransactions { get; set; } = new List<Transaction>();
}
