using Microsoft.EntityFrameworkCore;
using Pointwise.SqlDataAccess.EntityConfigurations;
using Pointwise.SqlDataAccess.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Pointwise.SqlDataAccess.SQLContext
{
    public class PointwiseSqlContext : DbContext
    {
        public PointwiseSqlContext(DbContextOptions<PointwiseSqlContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            new CategoryEntityConfiguration(modelBuilder.Entity<Category>());
            new SourceEntityConfiguration(modelBuilder.Entity<Source>());
            new TagEntityConfiguration(modelBuilder.Entity<Tag>());
            new ArticleTagEntityConfiguration(modelBuilder.Entity<ArticleTag>());
            new UserEntityConfiguration(modelBuilder.Entity<User>());
            new ImageEntityConfiguration(modelBuilder.Entity<Image>());
        }

        public virtual DbSet<Article> Articles { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<SqlUserType> UserTypes { get; set; }
        public virtual DbSet<SqlUserRole> UserRoles { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Image> Images { get; set; }
        public virtual DbSet<ArticleTag> ArticleTags { get; set; }
    }
}
