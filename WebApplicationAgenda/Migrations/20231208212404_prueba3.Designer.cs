﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebApplicationAgenda.Data;

#nullable disable

namespace WebApplicationAgenda.Migrations
{
    [DbContext(typeof(AgendaContext))]
    [Migration("20231208212404_prueba3")]
    partial class prueba3
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("WebApplicationAgenda.Entities.Contact", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<int?>("BlockedByUserId")
                        .HasColumnType("int");

                    b.Property<int?>("CelularNumber")
                        .HasColumnType("int");

                    b.Property<bool>("IsBlocked")
                        .HasColumnType("bit")
                        .HasColumnName("IsBlocked");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("TelephoneNumber")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("BlockedByUserId");

                    b.HasIndex("UserId");

                    b.ToTable("Contacts");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CelularNumber = 341457896,
                            IsBlocked = true,
                            Name = "Jaimito",
                            UserId = 1
                        },
                        new
                        {
                            Id = 2,
                            CelularNumber = 34156978,
                            IsBlocked = false,
                            Name = "Pepe",
                            TelephoneNumber = 422568,
                            UserId = 2
                        },
                        new
                        {
                            Id = 3,
                            CelularNumber = 11425789,
                            IsBlocked = true,
                            Name = "Maria",
                            UserId = 1
                        },
                        new
                        {
                            Id = 4,
                            CelularNumber = 34156,
                            IsBlocked = true,
                            Name = "Juanfer",
                            TelephoneNumber = 42256,
                            UserId = 3
                        });
                });

            modelBuilder.Entity("WebApplicationAgenda.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"), 1L, 1);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Email = "karenbailapiola@gmail.com",
                            LastName = "Lasot",
                            Name = "Karen",
                            Password = "Pa$$w0rd",
                            UserName = "karenpiola"
                        },
                        new
                        {
                            Id = 2,
                            Email = "elluismidetotoras@gmail.com",
                            LastName = "Gonzales",
                            Name = "Luis Gonzalez",
                            Password = "lamismadesiempre",
                            UserName = "luismitoto"
                        },
                        new
                        {
                            Id = 3,
                            Email = "sr@gmail.com",
                            LastName = "Razo",
                            Name = "Seba",
                            Password = "seba",
                            UserName = "sr"
                        });
                });

            modelBuilder.Entity("WebApplicationAgenda.Entities.Contact", b =>
                {
                    b.HasOne("WebApplicationAgenda.Entities.User", null)
                        .WithMany("BlockedContacts")
                        .HasForeignKey("BlockedByUserId")
                        .OnDelete(DeleteBehavior.Restrict);

                    b.HasOne("WebApplicationAgenda.Entities.User", "User")
                        .WithMany("Contacts")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("WebApplicationAgenda.Entities.User", b =>
                {
                    b.Navigation("BlockedContacts");

                    b.Navigation("Contacts");
                });
#pragma warning restore 612, 618
        }
    }
}
