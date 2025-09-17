namespace Cobs.Infrustructure.Persistence.EntityConfigs
{
    public class CustomerEntityConfigs : BaseEntityConfig<Customer>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Customer> builder)
        {
            builder
            .Property(x => x.WalletId)
                .IsRequired();

            builder
                .HasIndex(c => c.Email)
                .IsUnique();

            builder
            .Property(x => x.FirstName)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();

            builder
            .Property(x => x.LastName)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();

            builder
            .Property(x => x.Email)
                .HasColumnType(SqlTypes.VARCHAR)
                .HasMaxLength(256)
                .IsRequired();

            builder
            .Property(x => x.Role)
                .HasDefaultValue(Role.Customer)
                .IsRequired();

            builder
                .HasOne(c => c.Wallet)
                .WithOne(w => w.Customer)
                .HasForeignKey<Customer>(c => c.WalletId);
        }
    }
}
