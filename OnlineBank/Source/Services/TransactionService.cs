using OnlineBank.Data;
using OnlineBank.Data.Entities;
using OnlineBank.Source.Enums;
using OnlineBank.Source.Interfaces;

public class TransactionService : ITransactionService
{
    private readonly BankDbContext _db;
    private readonly ICardService _cardService;
    public TransactionService(BankDbContext db, ICardService cardService)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
        _cardService = cardService;
    }

    public void CreateTransaction(int fromCardId, int toCardId, decimal amount, string description = "")
    {
        if (_db == null) throw new InvalidOperationException("TransactionService is not initialized");

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
            Status = TransactionStatus.Successful,
            CreatedAt = DateTime.Now,
            Description = description
        };

        _db.Transactions.Add(transaction);
        _db.SaveChanges();
    }

    public List<Transaction> GetCardTransactions(int cardId)
    {
        if (_db == null) throw new InvalidOperationException("TransactionService is not initialized");

        return _db.Transactions
            .Where(t => t.FromCardId == cardId || t.ToCardId == cardId)
            .ToList();
    }
}
