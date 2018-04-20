﻿// <auto-generated />
using InterOn.Repo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace InterOn.Repo.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20180420144831_TestTableUserEvent")]
    partial class TestTableUserEvent
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.2-rtm-10011")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("InterOn.Data.DbModels.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EventRef");

                    b.Property<string>("Latitude")
                        .IsRequired();

                    b.Property<string>("Longitude")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("EventRef")
                        .IsUnique();

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<int>("PostId");

                    b.Property<DateTime>("UpdateDateTime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("PostId");

                    b.HasIndex("UserId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.ConfirmationKey", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Key");

                    b.Property<bool>("Revoked");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("ConfirmationKeys");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Event", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("DateTimeEvent");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<int?>("GroupId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<string>("PhotoUrl");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.EventSubCategory", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("SubCategoryId");

                    b.HasKey("EventId", "SubCategoryId");

                    b.HasIndex("SubCategoryId");

                    b.ToTable("EventSubCategories");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50);

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.GroupCategory", b =>
                {
                    b.Property<int>("SubCategoryId");

                    b.Property<int>("GroupId");

                    b.HasKey("SubCategoryId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("GroupCategories");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.GroupPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("GroupRef");

                    b.HasKey("Id");

                    b.HasIndex("GroupRef")
                        .IsUnique();

                    b.ToTable("GroupPhotos");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.MainCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("MainCategories");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.MainCategoryPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("MainCategoryRef");

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryRef")
                        .IsUnique();

                    b.ToTable("MainCategoryPhoto");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Post", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content")
                        .IsRequired();

                    b.Property<DateTime>("CreateDateTime");

                    b.Property<int?>("EventId");

                    b.Property<int?>("GroupId");

                    b.Property<DateTime>("UpdateDateTime");

                    b.Property<int?>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("EventId");

                    b.HasIndex("GroupId");

                    b.HasIndex("UserId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.SubCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("MainCategoryId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("MainCategoryId");

                    b.ToTable("SubCategories");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.SubCategoryPhoto", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileName")
                        .IsRequired()
                        .HasMaxLength(255);

                    b.Property<int>("SubCategoryRef");

                    b.HasKey("Id");

                    b.HasIndex("SubCategoryRef")
                        .IsUnique();

                    b.ToTable("SubCategoryPhoto");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Name");

                    b.Property<byte[]>("PasswordHash");

                    b.Property<byte[]>("PasswordSalt");

                    b.Property<string>("Surname");

                    b.Property<string>("Username");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserEvent", b =>
                {
                    b.Property<int>("EventId");

                    b.Property<int>("UserId");

                    b.HasKey("EventId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("UserEvents");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserGroup", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("GroupId");

                    b.HasKey("UserId", "GroupId");

                    b.HasIndex("GroupId");

                    b.ToTable("UserGroups");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserRole", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("RoleId");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserToken", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("IsStop");

                    b.Property<string>("Token");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.ToTable("UserTokens");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Address", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Event", "Event")
                        .WithOne("Address")
                        .HasForeignKey("InterOn.Data.DbModels.Address", "EventRef")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Comment", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Post", "Post")
                        .WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.ConfirmationKey", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Event", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.EventSubCategory", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Event", "Event")
                        .WithMany("SubCategories")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterOn.Data.DbModels.SubCategory", "SubCategory")
                        .WithMany("Events")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Group", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany("GroupAdmin")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.GroupCategory", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Group", "Group")
                        .WithMany("SubCategories")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterOn.Data.DbModels.SubCategory", "SubCategory")
                        .WithMany("Groups")
                        .HasForeignKey("SubCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.GroupPhoto", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Group", "Group")
                        .WithOne("GroupPhoto")
                        .HasForeignKey("InterOn.Data.DbModels.GroupPhoto", "GroupRef")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.MainCategoryPhoto", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.MainCategory", "MainCategory")
                        .WithOne("MainCategoryPhoto")
                        .HasForeignKey("InterOn.Data.DbModels.MainCategoryPhoto", "MainCategoryRef")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.Post", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Event", "Event")
                        .WithMany("Posts")
                        .HasForeignKey("EventId");

                    b.HasOne("InterOn.Data.DbModels.Group", "Group")
                        .WithMany("Posts")
                        .HasForeignKey("GroupId");

                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany("Posts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("InterOn.Data.DbModels.SubCategory", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.MainCategory", "MainCategory")
                        .WithMany("SubCategories")
                        .HasForeignKey("MainCategoryId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.SubCategoryPhoto", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.SubCategory", "SubCategory")
                        .WithOne("SubCategoryPhoto")
                        .HasForeignKey("InterOn.Data.DbModels.SubCategoryPhoto", "SubCategoryRef")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserEvent", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany("Events")
                        .HasForeignKey("EventId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterOn.Data.DbModels.Event", "Event")
                        .WithMany("Users")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserGroup", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Group", "Group")
                        .WithMany("Users")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany("Groups")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("InterOn.Data.DbModels.UserRole", b =>
                {
                    b.HasOne("InterOn.Data.DbModels.Role", "Roles")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("InterOn.Data.DbModels.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
