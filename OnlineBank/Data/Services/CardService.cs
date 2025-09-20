using OnlineBank.Data;
using OnlineBank.Data.Interfaces;

public class CardService : ICardService
{
    private readonly BankDbContext _db;
    public CardService(BankDbContext db)
    {
        _db = db ?? throw new ArgumentNullException(nameof(db));
    }

    public Card? GetCard(int cardId) => _db.Cards.Find(cardId);

    public List<Card> GetUserCards(int userId) => _db.Cards.Where(c => c.UserId == userId).ToList();

    public void AddCard(Card card)
    {
        card.CreatedAt = DateTime.Now;
        card.Balance = 0;
        card.IsBlocked = false;
        _db.Cards.Add(card);
        _db.SaveChanges();
    }

    public void BlockCard(int cardId)
    {
        var card = GetCard(cardId);
        if (card != null)
        {
            card.IsBlocked = true;
            _db.SaveChanges();
        }
    }

    public void UnblockCard(int cardId)
    {
        var card = GetCard(cardId);
        if (card != null)
        {
            card.IsBlocked = false;
            _db.SaveChanges();
        }
    }
}
