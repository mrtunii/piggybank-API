using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configuration
{
    public class UserConfiguration : BaseEntityConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Firstname)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Lastname)
                .HasMaxLength(200)
                .IsRequired();

            builder.Property(c => c.Username)
                .HasMaxLength(400)
                .IsRequired();

            builder.Property(c => c.Password)
                .IsRequired();
            builder.Property(c => c.PasswordSalt)
                .IsRequired();

            builder.Property(c => c.PhoneNumber)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.Amount)
                .HasColumnType("decimal(10,2)")
                .HasDefaultValue(0);

            builder.Property(c => c.Point)
                .HasDefaultValue(0);

            builder.HasMany(c => c.Transactions)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId);
        }
    }
}