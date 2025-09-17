using System.Text.Json.Serialization;

namespace Cobs.Application.UseCases.Order.Commands.CreateOrder
{
    public record CreateOrderCommand : IRequest<int>
    {
        [JsonIgnore]
        public int CustomerId { get; init; }

        public string Product { get; init; } = string.Empty;
        public int Quantity { get; init; }
        public decimal TotalAmount { get; init; }
        public DateTime OrderDate { get; init; } = DateTime.UtcNow;
    }
}
