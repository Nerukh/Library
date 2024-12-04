using Cafe.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCore_Demo2
{
    internal class CafeDbContext:DbContext
    {
        public DbSet<Waiter> Waiters { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("DataSource=C:Users/t3809/Downloads/N22-master/cafe.db");
        }
    }
}
