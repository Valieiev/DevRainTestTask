﻿// <auto-generated />
using System;
using DevRainAPI.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DevRainAPI.Migrations
{
    [DbContext(typeof(DevRainDBContext))]
    [Migration("20240213021508_InitialCreate")]
    partial class InitialCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DevRainAPI.Models.Feedback", b =>
                {
                    b.Property<Guid>("Id")
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Company")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("company");

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("createdDate");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("email");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("fullName");

                    b.Property<decimal?>("NegativeScore")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("negativeScore");

                    b.Property<decimal?>("NeutralScore")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("neutralScore");

                    b.Property<decimal?>("PositiveScore")
                        .HasColumnType("decimal(5, 2)")
                        .HasColumnName("positiveScore");

                    b.Property<string>("Sentiment")
                        .HasMaxLength(255)
                        .HasColumnType("nvarchar(255)")
                        .HasColumnName("sentiment");

                    b.Property<string>("Text")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("text");

                    b.HasKey("Id")
                        .HasName("PK__Feedback__3214EC07F31E4D0A");

                    b.ToTable("Feedback");
                });
#pragma warning restore 612, 618
        }
    }
}
