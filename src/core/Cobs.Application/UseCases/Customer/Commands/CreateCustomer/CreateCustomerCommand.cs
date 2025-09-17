using System.Text.Json.Serialization;

namespace Cobs.Application.UseCases.Customer.Commands.CreateCustomer
{
    public record CreateCustomerCommand : IRequest<int>
    {
        [JsonIgnore]
        public int WalletId { get; init; }

        public string FirstName { get; init; } = string.Empty;
        public string LastName { get; init; } = string.Empty;
        public string Email { get; init; } = string.Empty;
        public Role Role { get; init; } = Role.Customer;
    }
}
