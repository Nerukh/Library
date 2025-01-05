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
using Cafe.Model;

namespace WpfAppFluentAp
{
    internal class CafeDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Role { get; set; }
        public DbSet<UserRole> UserRole { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlite("DataSource=C:\\Users\\t3809\\Downloads\\N22-master\\cafe.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            

            modelBuilder.Entity<Role>().HasData(new Role[] {
               new Role { Id=Cafe.Model.Role.Admin.Id, Name=Cafe.Model.Role.Admin.Name },
               new Role { Id=Cafe.Model.Role.Manager.Id, Name=Cafe.Model.Role.Manager.Name },
               new Role { Id=Cafe.Model.Role.User.Id, Name=Cafe.Model.Role.User.Name }
            });
            modelBuilder.Entity<User>().HasData(new User[] { new User { Id = User.Admin.Id, Name = User.Admin.Name, Password = User.Admin.Password } });
            modelBuilder.Entity<UserRole>().HasData(new UserRole[] { new UserRole { Id = 1, RoleId = Cafe.Model.Role.Admin.Id, WaiterId = Cafe.Model.User.Admin.Id } });
        }
    }
}
