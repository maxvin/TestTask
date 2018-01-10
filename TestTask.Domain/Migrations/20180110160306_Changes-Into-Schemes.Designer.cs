﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using TestTask.Domain;

namespace TestTask.Domain.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20180110160306_Changes-Into-Schemes")]
    partial class ChangesIntoSchemes
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("TestTask.Domain.DbEntities.Comment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Text")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Comments");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Mail")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.Property<string>("Role")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("CustomerId");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<string>("Email")
                        .IsRequired();

                    b.Property<bool>("IsMunicipality");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<int>("NumberOfSchools");

                    b.Property<string>("Phone")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.Department", b =>
                {
                    b.Property<int>("DepartmentId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired();

                    b.Property<int?>("CustomerId");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("DepartmentId");

                    b.HasIndex("CustomerId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp");

                    b.Property<int?>("CustomerId");

                    b.Property<int>("DepartmentId");

                    b.Property<string>("Email");

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("Id");

                    b.Property<bool>("IsManager");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("Mail")
                        .IsRequired();

                    b.Property<string>("Mobile")
                        .IsRequired();

                    b.Property<string>("Name")
                        .IsRequired();

                    b.Property<string>("NormalizedEmail");

                    b.Property<string>("NormalizedUserName");

                    b.Property<string>("Password")
                        .IsRequired();

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName");

                    b.HasKey("UserId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.Comment", b =>
                {
                    b.HasOne("TestTask.Domain.DbEntities.Customer")
                        .WithMany("Comments")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.Contact", b =>
                {
                    b.HasOne("TestTask.Domain.DbEntities.Customer")
                        .WithMany("Contacts")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.Department", b =>
                {
                    b.HasOne("TestTask.Domain.DbEntities.Customer")
                        .WithMany("Departments")
                        .HasForeignKey("CustomerId");
                });

            modelBuilder.Entity("TestTask.Domain.DbEntities.User", b =>
                {
                    b.HasOne("TestTask.Domain.DbEntities.Customer")
                        .WithMany("Users")
                        .HasForeignKey("CustomerId");

                    b.HasOne("TestTask.Domain.DbEntities.Department", "Department")
                        .WithMany()
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
