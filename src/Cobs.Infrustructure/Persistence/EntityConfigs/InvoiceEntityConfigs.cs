namespace Cobs.Infrustructure.Persistence.EntityConfigs
{
    public class InvoiceEntityConfigs : BaseEntityConfig<Invoice>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Invoice> builder)
        {
            builder
            .Property(x => x.OrderId)
                .IsRequired();

            builder
            .Property(e => e.Amount)
                .HasDefaultValue(0)
                .HasPrecision(18, 0)
                .IsRequired();

            builder
            .Property(x => x.DueDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder
            .Property(x => x.Status)
                .HasDefaultValue(Status.Pending)
            .IsRequired();

            builder
                .HasOne(i => i.Order)
                .WithOne(o => o.Invoice)
                .HasForeignKey<Invoice>(i => i.OrderId);
        }
    }
}
