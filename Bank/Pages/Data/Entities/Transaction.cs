using Bank.Pages.Data.Enums;

namespace Bank.Pages.Data.Entities
{
    public class Transaction
    {
        public int Id { get; set; }
        public Card FromCardId { get; set; }
        public Card ToCardId { get; set;}
        public decimal Amount { get;set; }
        public TransactionType Type { get; set; }
        public TransactionStatus Status { get; set; }
        public string Description { get; set; }
        public DateOnly CreatedAt { get; set; }
    }
}