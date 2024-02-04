﻿// <auto-generated />
using System;
using LawSearchEngine.Persistance.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace LawSearchEngine.Persistance.Migrations
{
    [DbContext(typeof(LawSearchEngineDbContext))]
    [Migration("20240204170047_Initial_setup")]
    partial class Initial_setup
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("LawSearchEngine.Domain.Entities.Government", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Governments");
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Entities.Permission", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Permissions");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Deleted = false,
                            Name = "AddRole",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            Id = 2L,
                            Deleted = false,
                            Name = "EditRole",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            Id = 3L,
                            Deleted = false,
                            Name = "DeleteRole",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            Id = 4L,
                            Deleted = false,
                            Name = "AddPaymentOptions",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            Id = 5L,
                            Deleted = false,
                            Name = "EditPaymentOptions",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            Id = 6L,
                            Deleted = false,
                            Name = "DeletePaymentOptions",
                            RowVersion = new byte[0]
                        });
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Entities.Role", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<long>("Id"));

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.HasKey("Id");

                    b.ToTable("Roles");

                    b.HasData(
                        new
                        {
                            Id = 1L,
                            Deleted = false,
                            Name = "Admin",
                            RowVersion = new byte[0]
                        },
                        new
                        {
                            Id = 2L,
                            Deleted = false,
                            Name = "Agency",
                            RowVersion = new byte[0]
                        });
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Entities.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Deleted")
                        .HasColumnType("bit");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("RowVersion")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("10000000-0000-0000-0000-000000000000"),
                            Deleted = false,
                            Password = "admin",
                            RowVersion = new byte[0],
                            Username = "admin"
                        });
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Relationships.RolePermission", b =>
                {
                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.Property<long>("PermissionId")
                        .HasColumnType("bigint");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.ToTable("RoleWithPermission", (string)null);

                    b.HasData(
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 1L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 2L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 3L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 4L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 5L
                        },
                        new
                        {
                            RoleId = 1L,
                            PermissionId = 6L
                        });
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Relationships.UserRole", b =>
                {
                    b.Property<Guid>("UserId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<long>("RoleId")
                        .HasColumnType("bigint");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UserWithRole", (string)null);

                    b.HasData(
                        new
                        {
                            UserId = new Guid("10000000-0000-0000-0000-000000000000"),
                            RoleId = 1L
                        });
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Entities.Government", b =>
                {
                    b.HasOne("LawSearchEngine.Domain.Entities.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Relationships.RolePermission", b =>
                {
                    b.HasOne("LawSearchEngine.Domain.Entities.Permission", null)
                        .WithMany()
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LawSearchEngine.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("LawSearchEngine.Domain.Relationships.UserRole", b =>
                {
                    b.HasOne("LawSearchEngine.Domain.Entities.Role", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("LawSearchEngine.Domain.Entities.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
