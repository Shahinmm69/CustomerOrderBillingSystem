namespace Cobs.Infrustructure.Persistence.EntityConfigs
{
    public abstract class BaseEntityConfig<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : class, IBaseEntity, new()
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            ConfigureIdentity(builder);

            // Custom configuration
            ConfigureEntity(builder);
        }

        public abstract void ConfigureEntity(EntityTypeBuilder<TEntity> builder);

        private void ConfigureIdentity(EntityTypeBuilder<TEntity> builder)
        {
            // Config Identity column
            builder
                .HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd()
                   .IsRequired();
        }
    }
}
