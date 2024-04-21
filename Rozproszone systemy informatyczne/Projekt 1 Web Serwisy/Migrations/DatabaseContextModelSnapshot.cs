﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Projekt_1_Web_Serwisy.Database;

#nullable disable

namespace Projekt_1_Web_Serwisy.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Projekt_1_Web_Serwisy.Models.DBMotor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RentPrice")
                        .HasColumnType("int");

                    b.Property<DateTime?>("RentTo")
                        .HasColumnType("datetime2");

                    b.Property<bool>("Reservation")
                        .HasColumnType("bit");

                    b.Property<string>("SpriteURL")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Motors");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "a",
                            RentPrice = 1000,
                            Reservation = false
                        },
                        new
                        {
                            Id = 2,
                            Name = "b",
                            RentPrice = 1000,
                            Reservation = false
                        },
                        new
                        {
                            Id = 3,
                            Name = "c",
                            RentPrice = 1000,
                            Reservation = false
                        },
                        new
                        {
                            Id = 4,
                            Name = "d",
                            RentPrice = 1000,
                            Reservation = false
                        },
                        new
                        {
                            Id = 5,
                            Name = "e",
                            RentPrice = 1000,
                            Reservation = false
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
