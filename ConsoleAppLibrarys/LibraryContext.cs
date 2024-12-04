using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;


namespace ConsoleAppLibrarys
{
    public class LibraryContext : DbContext
    {
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<ReaderBook> ReaderBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite(@"Data Source=C:\Users\t3809\Downloads\N22-master\Library.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Book>().HasData(new Book[]
            {
        new Book { Id = 1, Name = "1984", Author = "George Orwell", Genre = "Dystopian", Pages = 328, Price = 15 },
        new Book { Id = 2, Name = "To Kill a Mockingbird", Author = "Harper Lee", Genre = "Fiction", Pages = 281, Price = 10 },
        new Book { Id = 3, Name = "The Great Gatsby", Author = "F. Scott Fitzgerald", Genre = "Classic", Pages = 180, Price = 12 }
            });

            modelBuilder.Entity<Reader>().HasData(new Reader[]
            {
        new Reader { Id = 1, Name = "John Doe", Street = "123 Maple Street", Birthday = new DateTime(1985, 5, 12) },
        new Reader { Id = 2, Name = "Jane Smith", Street = "456 Oak Avenue", Birthday = new DateTime(1990, 11, 20) },
        new Reader { Id = 3, Name = "Alice Johnson", Street = "789 Pine Road", Birthday = new DateTime(1978, 3, 15) }
            });

            modelBuilder.Entity<ReaderBook>().HasData(new ReaderBook[]
            {
        new ReaderBook { Id = 1, ReaderId = 1, BookId = 1, Status = Option.Bought, Data = new DateTime(2023, 12, 1) },
        new ReaderBook { Id = 2, ReaderId = 2, BookId = 2, Status = Option.Brought, Data = new DateTime(2023, 12, 2) },
        new ReaderBook { Id = 3, ReaderId = 3, BookId = 3, Status = Option.Taken_Away, Data = new DateTime(2023, 12, 3) }
            });
        }
    }

   
}
