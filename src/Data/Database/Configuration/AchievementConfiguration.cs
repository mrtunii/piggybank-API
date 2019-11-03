using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Database.Configuration
{
    public class AchievementConfiguration : BaseEntityConfiguration<Achievement>
    {
        public override void Configure(EntityTypeBuilder<Achievement> builder)
        {
            base.Configure(builder);

            builder.Property(c => c.Name)
                .HasMaxLength(400)
                .IsRequired();
            
            builder.Property(c => c.PointsToReach)
                .IsRequired();
            
            builder.HasMany(c => c.Users)
                .WithOne(c => c.Achievement)
                .HasForeignKey(c => c.AchievementId);
        }
    }
}