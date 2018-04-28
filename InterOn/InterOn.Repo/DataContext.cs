﻿using InterOn.Data.DbModels;
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
        public DbSet<Event> Events { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Post> Posts { get; set; }
        public DbSet<EventSubCategory> EventSubCategories { get; set; }
        public DbSet<UserEvent> UserEvents { get; set; }
        public DbSet<UserGroup> UserGroups { get; set; }
        public DbSet<Message> Messages { get; set; }
        public DbSet<Friend> Friends { get; set; }
     

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GroupCategory>().HasKey(gc => new {gc.SubCategoryId, gc.GroupId});
            modelBuilder.Entity<UserEvent>().HasKey(ug => new { ug.EventId, ug.UserId });
            modelBuilder.Entity<UserGroup>().HasKey(ug => new {ug.UserId, ug.GroupId});    
            modelBuilder.Entity<EventSubCategory>().HasKey(es => new {es.EventId, es.SubCategoryId});
            modelBuilder.Entity<Friend>()
                .HasOne(f => f.UserA)
                .WithMany()
                .HasForeignKey(f => f.UserAId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Friend>()
                .HasOne(f => f.UserB)
                .WithMany()
                .HasForeignKey(f => f.UserBId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Message>()
                .HasOne(f => f.SenderUser)
                .WithMany()
                .HasForeignKey(f => f.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Message>()
                .HasOne(f => f.ReceiverUser)
                .WithMany()
                .HasForeignKey(f => f.ReceiverId)
                .OnDelete(DeleteBehavior.Restrict);
        }

    }
   
}
