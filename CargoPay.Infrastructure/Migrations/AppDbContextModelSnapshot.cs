﻿// <auto-generated />
using System;
using CargoPay.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CargoPay.Infrastructure.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("CargoPay.Domain.Entities.Card", b =>
            {
                b.Property<int>("Id")
                    .ValueGeneratedOnAdd()
                    .HasColumnType("int");

                SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                b.Property<decimal>("Balance")
                    .HasColumnType("decimal(18,2)");

                b.Property<string>("CardNumber")
                    .IsRequired()
                    .HasMaxLength(16) 
                    .HasColumnType("nvarchar(16)");

                b.Property<DateTime>("CreatedAt")
                    .HasColumnType("datetime2")
                    .HasDefaultValueSql("GETUTCDATE()"); 

                b.HasKey("Id");

                b.ToTable("Cards");
            });
#pragma warning restore 612, 618
        }
    }
}
