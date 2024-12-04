﻿// <auto-generated />
using System;
using ConsoleAppLibrarys;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ConsoleAppLibrarys.Migrations
{
    [DbContext(typeof(LibraryContext))]
    partial class LibraryContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "8.0.11");

            modelBuilder.Entity("ConsoleAppLibrarys.Books", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Author")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Pages")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Price")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Books");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Author = "George Orwell",
                            Genre = "Dystopian",
                            Name = "1984",
                            Pages = 328,
                            Price = 15
                        },
                        new
                        {
                            Id = 2,
                            Author = "Harper Lee",
                            Genre = "Fiction",
                            Name = "To Kill a Mockingbird",
                            Pages = 281,
                            Price = 10
                        },
                        new
                        {
                            Id = 3,
                            Author = "F. Scott Fitzgerald",
                            Genre = "Classic",
                            Name = "The Great Gatsby",
                            Pages = 180,
                            Price = 12
                        });
                });

            modelBuilder.Entity("ConsoleAppLibrarys.Reader", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Readers");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Birthday = new DateTime(1985, 5, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "John Doe",
                            Street = "123 Maple Street"
                        },
                        new
                        {
                            Id = 2,
                            Birthday = new DateTime(1990, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Jane Smith",
                            Street = "456 Oak Avenue"
                        },
                        new
                        {
                            Id = 3,
                            Birthday = new DateTime(1978, 3, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Name = "Alice Johnson",
                            Street = "789 Pine Road"
                        });
                });

            modelBuilder.Entity("ConsoleAppLibrarys.ReaderBook", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BookId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Data")
                        .HasColumnType("TEXT");

                    b.Property<int>("ReaderId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.HasIndex("ReaderId");

                    b.ToTable("ReaderBooks");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BookId = 1,
                            Data = new DateTime(2023, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReaderId = 1,
                            Status = 0
                        },
                        new
                        {
                            Id = 2,
                            BookId = 2,
                            Data = new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReaderId = 2,
                            Status = 1
                        },
                        new
                        {
                            Id = 3,
                            BookId = 3,
                            Data = new DateTime(2023, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            ReaderId = 3,
                            Status = 2
                        });
                });

            modelBuilder.Entity("ConsoleAppLibrarys.ReaderBook", b =>
                {
                    b.HasOne("ConsoleAppLibrarys.Books", "Book")
                        .WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ConsoleAppLibrarys.Reader", "Readers")
                        .WithMany()
                        .HasForeignKey("ReaderId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Book");

                    b.Navigation("Readers");
                });
#pragma warning restore 612, 618
        }
    }
}
