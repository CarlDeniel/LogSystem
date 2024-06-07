using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LogSystem
{
    public class UserDataContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<TimeLog> TimeLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=C:\\Users\\Home\\source\\repos\\LogSystem\\LogSystem\\LogSystem\\DataFile.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<User>().HasData(new User
            {
                Id = 1,
                FullName = "Admin User",
                EmployeeId = "admin",
                Username = "admin",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123"),
                IsSuperUser = true
            });
        }
    }
}
