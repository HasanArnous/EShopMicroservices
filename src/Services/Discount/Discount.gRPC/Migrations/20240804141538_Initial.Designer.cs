﻿// <auto-generated />
using System;
using Discount.gRPC.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Discount.gRPC.Migrations
{
    [DbContext(typeof(DiscountDbContext))]
    [Migration("20240804141538_Initial")]
    partial class Initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.7");

            modelBuilder.Entity("Discount.gRPC.Models.Coupon", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ProductId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Coupons");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amount = 150.0,
                            Description = "150 Discount on the NP1!",
                            ProductId = new Guid("97daae1e-c74a-4212-af58-b727290346b8")
                        },
                        new
                        {
                            Id = 2,
                            Amount = 25.0,
                            Description = "25 Discount on the NE1!",
                            ProductId = new Guid("e1b4b20b-d29a-45bf-998d-59a59fb83e28")
                        });
                });
#pragma warning restore 612, 618
        }
    }
}