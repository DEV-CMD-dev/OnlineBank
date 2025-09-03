using OnlineBank.Data.Enums;

namespace OnlineBank.Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }

        public int FromCardId { get; set; }
        public Card FromCard { get; set; }

        public int ToCardId { get; set; }
        public Card ToCard { get; set; }

        public decimal Amount { get; set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public string Description { get; set; }
        public DateOnly CreatedAt { get; set; }
    }

}