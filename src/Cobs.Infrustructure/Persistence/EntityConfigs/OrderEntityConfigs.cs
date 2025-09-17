namespace Cobs.Infrustructure.Persistence.EntityConfigs
{
    public class OrderEntityConfigs : BaseEntityConfig<Order>
    {
        public override void ConfigureEntity(EntityTypeBuilder<Order> builder)
        {
            builder
            .Property(x => x.CustomerId)
                .IsRequired();

            builder
            .Property(x => x.Product)
                .HasColumnType(SqlTypes.NVARCHAR)
                .HasMaxLength(100)
                .IsRequired();

            builder
            .Property(x => x.Quantity)
                .IsRequired();

            builder
            .Property(e => e.TotalAmount)
                .HasDefaultValue(0)
                .HasPrecision(18, 0)
                .IsRequired();

            builder
            .Property(x => x.OrderDate)
                .HasDefaultValueSql("GETUTCDATE()")
                .IsRequired();

            builder
                .HasOne(o => o.Customer)
                .WithMany(c => c.Orders)
                .HasForeignKey(o => o.CustomerId);
        }
    }
}
