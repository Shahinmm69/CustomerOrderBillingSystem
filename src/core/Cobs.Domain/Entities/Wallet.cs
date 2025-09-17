namespace Cobs.Domain.Entities
{
    public class Wallet : BaseEntity
    {
        public decimal WalletBalance { get; set; }
        public byte[] RowVersion { get; set; } = default!;

        public Customer Customer { get; set; } = default!;
        public ICollection<Transaction> Transactions { get; set; } = [];
    }
}
