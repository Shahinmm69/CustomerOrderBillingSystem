namespace Cobs.Application.DTOs
{
    public record OrderDto
    {
        public int Id { get; init; }
        public int? CustomerId { get; init; } = default;
        public string? Product { get; init; } = default;
        public int? Quantity { get; init; } = default;
        public decimal? TotalAmount { get; init; } = default;
        public DateTime? OrderDate { get; init; } = default;
    }
}
