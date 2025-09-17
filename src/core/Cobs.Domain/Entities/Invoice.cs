namespace Cobs.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime DueDate { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.Pending;

        public Order Order { get; set; } = default!;
    }
}
