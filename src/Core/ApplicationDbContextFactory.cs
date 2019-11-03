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
                "Server=173.249.49.158,1433\\SQLEXPRESS;Database=PiggyBank;Trusted_Connection=false;User Id=levan; Password=Qz69fdk1Qz69fdk2");

            return new ApplicationDbContext(builder.Options);
        }
    }
}