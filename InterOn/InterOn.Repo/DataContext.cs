using System;
using System.Collections.Generic;
using System.Text;
using InterOn.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace InterOn.Repo
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<UserToken> UserTokens { get; set; }
        public DbSet<ConfirmationKey> ConfirmationKeys { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupCategory>().HasKey(vf =>
                new { vf.SubCategoryId, vf.GroupId });

        }
    }
   
}
