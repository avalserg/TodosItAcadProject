﻿// <auto-generated />
using System;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20240331214334_Init")]
    partial class Init
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Auth.Domain.RefreshToken", b =>
                {
                    b.Property<Guid>("RefreshTokenId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("Expired")
                        .HasColumnType("datetime2");

                    b.HasKey("RefreshTokenId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("RefreshTokens");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUser", b =>
                {
                    b.Property<Guid>("ApplicationUserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("LastSingInDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("ApplicationUserId");

                    b.ToTable("ApplicationUsers");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserApplicationUserRole", b =>
                {
                    b.Property<int>("ApplicationUserRoleId")
                        .HasColumnType("int");

                    b.Property<Guid>("ApplicationUserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("ApplicationUserRoleId", "ApplicationUserId");

                    b.HasIndex("ApplicationUserId");

                    b.ToTable("ApplicationUserApplicationUserRole");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserRole", b =>
                {
                    b.Property<int>("ApplicationUserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ApplicationUserRoleId"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(20)
                        .HasColumnType("nvarchar(20)");

                    b.HasKey("ApplicationUserRoleId");

                    b.ToTable("ApplicationUserRoles");
                });

            modelBuilder.Entity("Todos.Domain.Todo", b =>
                {
                    b.Property<int>("TodoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TodoId"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsDone")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<Guid>("OwnerId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("UpdatedDate")
                        .HasColumnType("datetime2");

                    b.HasKey("TodoId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Todos");
                });

            modelBuilder.Entity("Auth.Domain.RefreshToken", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany()
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserApplicationUserRole", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Users.Domain.ApplicationUserRole", "Role")
                        .WithMany("Users")
                        .HasForeignKey("ApplicationUserRoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ApplicationUser");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Todos.Domain.Todo", b =>
                {
                    b.HasOne("Core.Users.Domain.ApplicationUser", "Owner")
                        .WithMany()
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUser", b =>
                {
                    b.Navigation("Roles");
                });

            modelBuilder.Entity("Core.Users.Domain.ApplicationUserRole", b =>
                {
                    b.Navigation("Users");
                });
#pragma warning restore 612, 618
        }
    }
}
