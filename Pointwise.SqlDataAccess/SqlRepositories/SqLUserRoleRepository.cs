using Microsoft.EntityFrameworkCore;
using Pointwise.Domain.Interfaces;
using Pointwise.Domain.Models;
using Pointwise.Domain.Repositories;
using Pointwise.SqlDataAccess.ModelExtensions;
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
            var userRoles = context.UserRoles
                .Include(x => x.SqlUser).AsNoTracking()
                .Where(x => x.UserId == userId)
                .AsEnumerable()
                .Select(x => x.ToDomainEntity())
                .ToList();

            return userRoles;
        }

        public IUserRole AddUserRole(Domain.Models.UserRole entity)
        {
            var sEntity = entity.ToPersistentEntity();

            var insertedRow = context.UserRoles.Add(sEntity);
            context.SaveChanges();

            return insertedRow.Entity.ToDomainEntity();
        }

        public bool AddUserRole(IEnumerable<UserRole> entities)
        {
            
            try
            {
                //var userId = entities.Select(x => x.User.Id).FirstOrDefault();

                //var existingRoles = context.UserRoles.Where(x => x.UserId == userId).ToList();
                //var entitiesToInsert = entities.ToList();
                //foreach(var entity in entitiesToInsert.ToList())
                //{
                //    var t = entity;
                //    if(existingRoles.Any(x =>
                //    x.UserId == t.User.Id
                //    && x.EntityType == t.EntityType 
                //    && x.AccessType == t.AccessType))
                //    {
                //        entitiesToInsert.Remove(t);
                //    }
                //}

                //var sEntities = entitiesToInsert.Select(x => x.ToPersistentEntity()).ToList();
                var sEntities = entities.Select(x => x.ToPersistentEntity()).ToList();

                context.UserRoles.AddRange(sEntities);
                context.SaveChanges();

                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool RemoveUserRole(Domain.Models.UserRole entity)
        {
            try
            {
                var sEntity = entity.ToPersistentEntity();

                var insertedRow = context.UserRoles.Remove(sEntity);
                context.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool RemoveUserRole(IEnumerable<UserRole> entities)
        {

            try
            {
                var sEntities = entities.Select(x => x.ToPersistentEntity()).ToList();

                context.UserRoles.RemoveRange(sEntities);
                context.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public void Dispose()
        {
            context.Dispose();
        }

        
    }
}
