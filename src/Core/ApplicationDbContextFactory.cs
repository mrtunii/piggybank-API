using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Core
{
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ApplicationDbContext>();
            builder.UseSqlServer(
                "Server=127.0.0.1,1433;Database=PiggyBank;Trusted_Connection=false;User Id=sa; Password=Qz69fdk1Qz69fdk2");

            return new ApplicationDbContext(builder.Options);
        }
    }
}