namespace Cobs.Application.DTOs
{
    public class TransactionDto
    {
        public int Id { get; init; }
        public decimal TransactionAmount { get; init; }
        public decimal WalletBalance { get; init; }
        public DateTime TransactionDate { get; init; }
    }
}
