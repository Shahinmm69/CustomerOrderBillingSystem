namespace Cobs.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public int WalletId { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; } = Role.Customer;

        public Wallet Wallet { get; set; } = default!;
        public ICollection<Order> Orders { get; set; } = [];
    }
}
