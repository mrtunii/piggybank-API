using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configuration
{
    public class TransactionConfiguration : BaseEntityConfiguration<Transaction>
    {
        public override void Configure(EntityTypeBuilder<Transaction> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Amount)
                .HasColumnType("decimal(10,2)")
                .IsRequired();
            builder.Property(c => c.Description)
                .HasMaxLength(400)
                .IsRequired(false);

            builder.Property(c => c.AccountNumber)
                .HasMaxLength(400);

            builder.Property(c => c.MerchantName)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(c => c.ProcessedAmount)
                .HasDefaultValue(0)
                .HasColumnType("decimal(10,2)");

            builder.Property(c => c.ProcessedPoint)
                .HasDefaultValue(0)
                .IsRequired();

            builder.Property(c => c.HasProcessed)
                .HasDefaultValue(false);

            builder.Property(c => c.TransactionDate)
                .HasColumnType("datetime2(7)")
                .IsRequired();

            builder.HasOne(c => c.User)
                .WithMany(c => c.Transactions)
                .HasForeignKey(c => c.UserId);

        }
    }
}