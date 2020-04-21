using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Pointwise.SqlDataAccess.SQLContext
{
    public class PointwiseSqlContextFactory : IDesignTimeDbContextFactory<PointwiseSqlContext>
    {
        public PointwiseSqlContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PointwiseSqlContext>();
            //optionsBuilder.UseSqlServer("server=(localdb)\\MSSQLLocalDB;database=Pointwise;Trusted_Connection=true;MultipleActiveResultSets=true");
            optionsBuilder.UseSqlServer("data source=localhost;initial catalog=PointwiseNew;Trusted_Connection=true;MultipleActiveResultSets=true");
            return new PointwiseSqlContext(optionsBuilder.Options);
        }
    }
}
