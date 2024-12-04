using Cafe.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Reflection.Metadata.BlobBuilder;

namespace WpfAppLibrary
{
    internal class LibraryDbContext : DbContext
    {
        public DbSet<UserLibrary> Reader { get; set; }
        public DbSet<Books> Book { get; set; }
        public DbSet<UserBook> UserBooks { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("DataSource=C:/Users/t3809/Downloads/N22-master/cafe.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserLibrary>().HasData(new UserLibrary[] {
        new UserLibrary { Id = 1, Name = "John Doe", Street = "123 Main St", Birthday = new DateTime(1980, 1, 1) },
        new UserLibrary { Id = 2, Name = "Jane Smith", Street = "456 Elm St", Birthday = new DateTime(1990, 5, 15) }
            });
            modelBuilder.Entity<Books>().HasData(new Books[] {
        new Books { Name = "Book1", Author = "Author1", Genre = "Fiction", Pages = 200, Price = 10 },
        new Books { Name = "Book2", Author = "Author2", Genre = "Non-fiction", Pages = 300, Price = 15 }
    });
        }

    }
}
