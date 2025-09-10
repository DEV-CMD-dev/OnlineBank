using OnlineBank.Data.Entities;

namespace OnlineBank.Data.Interfaces
{
    public interface ITransactionService
    {
        public void CreateTransaction(int fromCardId, int toCardId, decimal amount, string description = "");
        public List<Transaction> GetCardTransactions(int cardId);
    }
}
