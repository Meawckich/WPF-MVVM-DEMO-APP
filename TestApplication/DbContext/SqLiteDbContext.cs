using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using TestApplication.Models;

namespace TestApplication.DbContext
{
    public class SqLiteDbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbSet<User> users { get; set; }
        public DbSet<ExcelCells> cells { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            SqliteConnectionStringBuilder builder = new SqliteConnectionStringBuilder();
            builder.Password = "12345";

            optionsBuilder.UseSqlite("FileName= TestDb.db",option =>
            {
                option.MigrationsAssembly(Assembly.GetExecutingAssembly().FullName);
            });
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().ToTable("Users");
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasIndex(x => x.Login).IsUnique();
            });
            modelBuilder.Entity<ExcelCells>().ToTable("ExcelCells");
            modelBuilder.Entity<ExcelCells>(entity =>
            {
                entity.HasKey(x => x.ExcellCellsId);
            });
            base.OnModelCreating(modelBuilder);
        }

        public void CreateDb()
        {
            using (var s = this)
            {
                s.Database.EnsureCreated();
                s.users.Add(new User(
                    id: 1,
                    fullName: "Demidovich Vladislav",
                    login: "newlogin123",
                    passWord: "newpassword123"
                    ));
                s.SaveChanges();
            }
        }
    }
}
