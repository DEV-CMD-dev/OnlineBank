using OnlineBank.Data;
using OnlineBank.Data.Entities;
using OnlineBank.Data.Enums;

public static class TransactionService
{
    private static BankDbContext? _db;

    public static void Init(BankDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public static void CreateTransaction(int fromCardId, int toCardId, decimal amount, string description = "")
    {
        if (_db == null) throw new InvalidOperationException("TransactionService is not initialized");

        var fromCard = CardService.GetCard(fromCardId);
        var toCard = CardService.GetCard(toCardId);

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
            Status = TransactionStatus.Successful,
            CreatedAt = DateOnly.FromDateTime(DateTime.Now),
            Description = description
        };

        _db.Transactions.Add(transaction);
        _db.SaveChanges();
    }

    public static List<Transaction> GetCardTransactions(int cardId)
    {
        if (_db == null) throw new InvalidOperationException("TransactionService is not initialized");

        return _db.Transactions
            .Where(t => t.FromCardId == cardId || t.ToCardId == cardId)
            .ToList();
    }
}
