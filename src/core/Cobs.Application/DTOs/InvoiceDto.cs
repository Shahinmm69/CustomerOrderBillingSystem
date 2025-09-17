namespace Cobs.Application.DTOs
{
    public record InvoiceDto
    {
        public int Id { get; init; }
        public int? OrderId { get; init; } = default;
        public decimal? Amount { get; init; } = default;
        public DateTime? DueDate { get; init; } = default;
        public Status? Status { get; init; } = default;
    }
}
