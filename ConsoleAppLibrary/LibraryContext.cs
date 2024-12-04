using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleAppLibrary
{
    internal class LibraryContext : DbContext
    {
        public DbSet<Reader> Readers { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<ReaderBook> ReaderBooks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db"); 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReaderBook>()
                .HasKey(rb => new { rb.ReaderId, rb.BookId }); 
        }
    }
}
