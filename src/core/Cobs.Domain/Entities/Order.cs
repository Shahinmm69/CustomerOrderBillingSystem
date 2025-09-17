namespace Cobs.Domain.Entities
{
    public class Order : BaseEntity
    {
        public int CustomerId { get; set; }
        public string Product { get; set; } = string.Empty;
        public int Quantity { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime OrderDate { get; set; } = DateTime.UtcNow;

        public Customer Customer { get; set; } = default!;
        public Invoice Invoice { get; set; } = default!;
    }
}
