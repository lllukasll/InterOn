using System.Linq;
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
        public DbSet<Event> Events { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<EventSubCategory> EventSubCategories { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupCategory>().HasKey(gc => new {gc.SubCategoryId, gc.GroupId});
            modelBuilder.Entity<Group>()
                .HasOne(a => a.GroupPhoto)
                .WithOne(b => b.Group)
                .HasForeignKey<GroupPhoto>(b => b.GroupRef)
                .OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<UserEvent>().HasKey(ug => new { ug.EventId, ug.UserId });
            modelBuilder.Entity<UserEvent>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Events)
                .HasForeignKey(bc => bc.EventId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserEvent>()
                .HasOne(bc => bc.Event)
                .WithMany(c => c.Users)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<UserGroup>().HasKey(ug => new {ug.UserId, ug.GroupId});
            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.User)
                .WithMany(b => b.Groups)
                .HasForeignKey(bc => bc.GroupId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<UserGroup>()
                .HasOne(bc => bc.Group)
                .WithMany(c => c.Users)
                .HasForeignKey(bc => bc.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Event>()
                .HasOne(a => a.Address)
                .WithOne(b => b.Event)
                .HasForeignKey<Address>(b => b.EventRef);         
            modelBuilder.Entity<EventSubCategory>().HasKey(es => new {es.EventId, es.SubCategoryId});
          
        }

    }
   
}
