using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configuration
{
    public class BaseEntityConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.DateCreated)
                .HasColumnType("datetime2(7)")
                .HasDefaultValueSql("getdate()");

            builder.Property(c => c.DateDeleted)
                .HasColumnType("datetime2(7)")
                .IsRequired(false);
        }
    }
}