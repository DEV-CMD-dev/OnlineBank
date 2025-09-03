using OnlineBank.Data.Entities;
using OnlineBank.Data.Enums;
using OnlineBank.Data.Structs;

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

    public ICollection<Transaction> SentTransactions { get; set; }
    public ICollection<Transaction> ReceivedTransactions { get; set; }
}
