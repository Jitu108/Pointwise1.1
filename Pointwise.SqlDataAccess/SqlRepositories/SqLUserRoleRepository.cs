using Microsoft.EntityFrameworkCore;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Repositories;
using Pointwise.SqlDataAccess.SQLContext;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pointwise.SqlDataAccess.SqlRepositories
{
    public sealed class SqlUserRoleRepository : IUserRoleRepository, IDisposable
    {
        private readonly PointwiseSqlContext context;

        public SqlUserRoleRepository(DbContextOptions<PointwiseSqlContext> options)
        {
            context = new PointwiseSqlContext(options);

        }

        public IEnumerable<IUserRole> GetUserRoles(int userId)
        {
            var userRoles = context.UserRoles.Where(x => x.UserId == userId)
                .AsEnumerable().Select(x => x.ToDomainEntity()).ToList();

            return userRoles;
        }
        
        public void Dispose()
        {
            context.Dispose();
        }
    }
}
