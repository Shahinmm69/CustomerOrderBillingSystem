namespace Cobs.Domain.Entities
{
    public class Transaction : BaseEntity
    {
        public int WalletId { get; set; }
        public DateTime TransactionDate { get; set; } = DateTime.UtcNow;
        public decimal TransactionAmount { get; set; }
        public decimal WalletBalance { get; set; }

        public Wallet Wallet { get; set; } = default!;
    }
}
