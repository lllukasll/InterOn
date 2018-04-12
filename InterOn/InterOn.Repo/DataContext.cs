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
        public DbSet<SubCategory> SubCategories { get; set; }
        public DbSet<MainCategory> MainCategories { get; set; }
        public DbSet<GroupPhoto> GroupPhotos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupCategory>().HasKey(gc =>
                new { gc.SubCategoryId, gc.GroupId });
            modelBuilder.Entity<Group>()
                .HasOne(a => a.GroupPhoto)
                .WithOne(b => b.Group)
                .HasForeignKey<GroupPhoto>(b => b.GroupRef);
            modelBuilder.Entity<UserGroup>().HasKey(ug =>
                new { ug.UserId, ug.GroupId });
        }
    }
   
}
