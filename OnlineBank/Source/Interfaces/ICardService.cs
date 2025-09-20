namespace OnlineBank.Source.Interfaces
{
    public interface ICardService
    {
        public Card? GetCard(int cardId);
        public Card? GetCardByNumber(string cardNumber);
        public List<Card> GetUserCards(int userId);
        public void AddCard(Card card);
        public void BlockCard(int cardId);
        public void UnblockCard(int cardId);
    }
}
