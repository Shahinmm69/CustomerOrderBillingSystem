namespace Cobs.Infrustructure.Persistence.EntityConfigs
{
    public class TransactionEntityConfigs : BaseEntityConfig<Transaction>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Transaction> builder)
        {
            builder
            .Property(x => x.WalletId)
                .IsRequired();
            builder
            .Property(x => x.TransactionDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder
            .Property(e => e.TransactionAmount)
                .HasPrecision(18, 0)
                .IsRequired();

            builder
            .Property(e => e.WalletBalance)
                .HasDefaultValue(0)
                .HasPrecision(18, 0)
                .IsRequired();

            builder
                .HasOne(t => t.Wallet)
                .WithMany(w => w.Transactions)
                .HasForeignKey(t => t.WalletId);
        }
    }
}
