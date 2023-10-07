﻿// <auto-generated />
using System;
using LibraryIdentityProvider.EFCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace LibraryIdentityProvider.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231006171838_UserRoleTable")]
    partial class UserRoleTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("LibraryIdentityProvider.Entities.Password", b =>
                {
                    b.Property<int>("PasswordID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PasswordID"));

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<byte[]>("Salt")
                        .IsRequired()
                        .HasColumnType("bytea");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("PasswordID");

                    b.HasIndex("UserID")
                        .IsUnique();

                    b.ToTable("Password", (string)null);
                });

            modelBuilder.Entity("LibraryIdentityProvider.Entities.UserAccount", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FirstName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("UserAccount", (string)null);
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Role", b =>
                {
                    b.Property<int>("RoleID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("RoleID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid?>("UserAccountId")
                        .HasColumnType("uuid");

                    b.HasKey("RoleID");

                    b.HasIndex("UserAccountId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.Permission", b =>
                {
                    b.Property<int>("PermissionID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("PermissionID"));

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PermissionID");

                    b.ToTable("Permission");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.RolePermission", b =>
                {
                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.Property<int>("PermissionID")
                        .HasColumnType("integer");

                    b.HasKey("RoleID", "PermissionID");

                    b.HasIndex("PermissionID");

                    b.ToTable("RolePermission");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.RoleUser", b =>
                {
                    b.Property<int>("RoleID")
                        .HasColumnType("integer");

                    b.Property<Guid>("UserID")
                        .HasColumnType("uuid");

                    b.HasKey("RoleID", "UserID");

                    b.HasIndex("UserID");

                    b.ToTable("RoleUser");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Entities.Password", b =>
                {
                    b.HasOne("LibraryIdentityProvider.Entities.UserAccount", null)
                        .WithOne()
                        .HasForeignKey("LibraryIdentityProvider.Entities.Password", "UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Role", b =>
                {
                    b.HasOne("LibraryIdentityProvider.Entities.UserAccount", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserAccountId");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.RolePermission", b =>
                {
                    b.HasOne("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryIdentityProvider.Features.UserManagement.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Permission");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.RoleUser", b =>
                {
                    b.HasOne("LibraryIdentityProvider.Features.UserManagement.Role", "Role")
                        .WithMany("RoleUsers")
                        .HasForeignKey("RoleID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LibraryIdentityProvider.Entities.UserAccount", "UserAccount")
                        .WithMany("RoleUsers")
                        .HasForeignKey("UserID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Role");

                    b.Navigation("UserAccount");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Entities.UserAccount", b =>
                {
                    b.Navigation("RoleUsers");

                    b.Navigation("Roles");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Role", b =>
                {
                    b.Navigation("RolePermissions");

                    b.Navigation("RoleUsers");
                });

            modelBuilder.Entity("LibraryIdentityProvider.Features.UserManagement.Roles_and_Permissions.Permission", b =>
                {
                    b.Navigation("RolePermissions");
                });
#pragma warning restore 612, 618
        }
    }
}
