using OnlineBank.Data;
using OnlineBank.Data.Entities;

public class TransactionService
{
    private readonly BankDbContext _db;
    private readonly CardService _cardService;

    public TransactionService(BankDbContext db, CardService cardService)
    {
        _db = db;
        _cardService = cardService;
    }

    public void CreateTransaction(int fromCardId, int toCardId, decimal amount, string description = "")
    {
        var fromCard = _cardService.GetCard(fromCardId);
        var toCard = _cardService.GetCard(toCardId);

        if (fromCard == null || toCard == null)
            throw new Exception("One of the cards not found.");
        if (fromCard.IsBlocked)
            throw new Exception("Sender card is blocked.");
        if (fromCard.Balance < amount)
            throw new Exception("Insufficient funds.");

        fromCard.Balance -= amount;
        toCard.Balance += amount;

        var transaction = new Transaction
        {
            FromCardId = fromCardId,
            ToCardId = toCardId,
            Amount = amount,
            Status = OnlineBank.Data.Enums.TransactionStatus.Successful,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            Description = description
        };

        _db.Transactions.Add(transaction);
        _db.SaveChanges();
    }

    public List<Transaction> GetCardTransactions(int cardId)
    {
        return _db.Transactions
            .Where(t => t.FromCardId == cardId || t.ToCardId == cardId)
            .ToList();
    }
}
