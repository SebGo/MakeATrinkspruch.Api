﻿// <auto-generated />
using System;
using MakeATrinkspruch.Api.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MakeATrinkspruch.Api.Migrations
{
    [DbContext(typeof(AppDBContext))]
    partial class AppDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MakeATrinkspruch.Api.Data.Entities.Tag", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("TagName")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("TagName")
                        .IsUnique();

                    b.ToTable("Tags");
                });

            modelBuilder.Entity("MakeATrinkspruch.Api.Data.Entities.Toast", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("ToastText")
                        .IsRequired()
                        .HasColumnType("varchar(255) CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("ToastText")
                        .IsUnique();

                    b.ToTable("Toasts");
                });

            modelBuilder.Entity("MakeATrinkspruch.Api.Data.Entities.ToastTag", b =>
                {
                    b.Property<Guid>("ToastId")
                        .HasColumnType("char(36)");

                    b.Property<Guid>("TagId")
                        .HasColumnType("char(36)");

                    b.HasKey("ToastId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("ToastTag");
                });

            modelBuilder.Entity("MakeATrinkspruch.Api.Data.Entities.ToastTag", b =>
                {
                    b.HasOne("MakeATrinkspruch.Api.Data.Entities.Tag", "Tag")
                        .WithMany("ToastTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("MakeATrinkspruch.Api.Data.Entities.Toast", "Toast")
                        .WithMany("ToastTags")
                        .HasForeignKey("ToastId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
