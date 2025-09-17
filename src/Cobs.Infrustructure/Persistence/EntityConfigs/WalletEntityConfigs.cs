namespace Cobs.Infrustructure.Persistence.EntityConfigs
{
    public class WalletEntityConfigs : BaseEntityConfig<Wallet>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Wallet> builder)
        {
            builder
            .Property(e => e.WalletBalance)
                .HasDefaultValue(0)
                .HasPrecision(18, 0)
                .IsRequired();

            builder
            .Property(e => e.RowVersion)
                .IsRowVersion()
                .IsConcurrencyToken();
        }
    }
}
