using Microsoft.EntityFrameworkCore;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.Domain.Models;
using Pointwise.SqlDataAccess.SQLContext;
using System.Linq;

namespace Pointwise.SqlDataAccess.SqlRepositories
{
    public class SqlAuthRepository : IAuthRepository
    {
        private readonly PointwiseSqlContext context;

        public SqlAuthRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);

        }

        public IAuthUser Authenticate(string userName, string password)
        {
            var user = context.Users
                .Include(x => x.SqlUserType)
                .Include(x => x.SqlUserRoles)
                .SingleOrDefault(x => x.UserName == userName && x.Password == password);
            return user != null ? user.ToAuthEntity(): null;
        }

        public bool Logout(string userName)
        {
            var userExists = context.Users.SingleOrDefault(x => x.UserName == userName);
            return userExists != null;
        }
    }
}
