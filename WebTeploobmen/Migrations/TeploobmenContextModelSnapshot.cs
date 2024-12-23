﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebTeploobmen.Data;

#nullable disable

namespace WebTeploobmen.Migrations
{
    [DbContext(typeof(TeploobmenContext))]
    partial class TeploobmenContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("WebTeploobmen.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("WebTeploobmen.Data.Variant", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Av")
                        .HasColumnType("REAL");

                    b.Property<double>("Cg")
                        .HasColumnType("REAL");

                    b.Property<double>("Cm")
                        .HasColumnType("REAL");

                    b.Property<double>("Da")
                        .HasColumnType("REAL");

                    b.Property<double>("Gm")
                        .HasColumnType("REAL");

                    b.Property<int>("H0")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tbol")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tmal")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("UserId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Wg")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.ToTable("Variants");
                });
#pragma warning restore 612, 618
        }
    }
}
