namespace Cobs.Application.DTOs
{
    public record CustomerDto
    {
        public int Id { get; init; }
        public int? WalletId { get; init; } = default;
        public string? FirstName { get; init; } = default;
        public string? LastName { get; init; } = default;
        public string? Email { get; init; } = default;
        public Role? Role { get; init; } = default;
    }
}
