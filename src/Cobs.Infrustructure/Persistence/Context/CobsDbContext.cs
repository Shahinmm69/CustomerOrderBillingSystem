namespace Cobs.Infrustructure.Persistence.Context
{
    public class CobsDbContext : DbContext, ICobsDbContext
    {
        #region DbSets
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Invoice> Invoices { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Wallet> Wallets { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        #endregion

        public CobsDbContext(DbContextOptions<CobsDbContext> options) : base(options) { }

        #region Overriding Methods
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema(EntitySchema.BASE);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            #region Seed Data
            // Wallets
            modelBuilder.Entity<Wallet>().HasData(
                new Wallet
                {
                    Id = 1,
                    WalletBalance = 100m
                },
                new Wallet
                {
                    Id = 2,
                    WalletBalance = 200m
                }
            );

            // Customers
            modelBuilder.Entity<Customer>().HasData(
                new Customer
                {
                    Id = 1,
                    FirstName = "Ali",
                    LastName = "Rezaei",
                    Email = "ali@example.com",
                    WalletId = 1,
                    Role = Role.Customer
                },
                new Customer
                {
                    Id = 2,
                    FirstName = "Sara",
                    LastName = "Ahmadi",
                    Email = "sara@example.com",
                    WalletId = 2,
                    Role = Role.Manager
                }
            );

            // Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    CustomerId = 1,
                    Product = "Laptop",
                    Quantity = 1,
                    TotalAmount = 1200.75m,
                    OrderDate = new DateTime(2025, 9, 15)
                },
                new Order
                {
                    Id = 2,
                    CustomerId = 2,
                    Product = "Phone",
                    Quantity = 2,
                    TotalAmount = 1500.00m,
                    OrderDate = new DateTime(2025, 9, 15)
                }
            );

            // Invoices
            modelBuilder.Entity<Invoice>().HasData(
                new Invoice
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 1200.75m,
                    DueDate = new DateTime(2025, 9, 22),
                    Status = Status.Pending
                },
                new Invoice
                {
                    Id = 2,
                    OrderId = 2,
                    Amount = 1500.00m,
                    DueDate = new DateTime(2025, 9, 22),
                    Status = Status.Pending
                }
            );
            #endregion
        }
        #endregion
    }
}
