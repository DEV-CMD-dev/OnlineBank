using OnlineBank.Data;

public static class CardService
{
    private static BankDbContext? _db;

    public static void Init(BankDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public static Card? GetCard(int cardId)
    {
        if (_db == null) throw new InvalidOperationException("CardService is not initialized");
        return _db.Cards.Find(cardId);
    }

    public static List<Card> GetAllCards(int userId)
        {
        if (_db == null) throw new InvalidOperationException("CardService is not initialized");
        return _db.Cards.Where(c => c.UserId == userId).ToList();
    }


    public static List<Card> GetUserCards(int userId)
    {
        if (_db == null) throw new InvalidOperationException("CardService is not initialized");
        return _db.Cards.Where(c => c.UserId == userId).ToList();
    }

    public static void AddCard(Card card)
    {
        if (_db == null) throw new InvalidOperationException("CardService is not initialized");
        card.CreatedAt = DateOnly.FromDateTime(DateTime.Now);
        card.Balance = 0;
        card.IsBlocked = false;
        _db.Cards.Add(card);
        _db.SaveChanges();
    }

    public static void BlockCard(int cardId)
    {
        var card = GetCard(cardId);
        if (card != null)
        {
            card.IsBlocked = true;
            _db!.SaveChanges();
        }
    }

    public static void UnblockCard(int cardId)
    {
        var card = GetCard(cardId);
        if (card != null)
        {
            card.IsBlocked = false;
            _db!.SaveChanges();
        }
    }
}
